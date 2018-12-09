using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NightOwl.Xamarin.Components;
using NightOwl.Xamarin.Services;
using NightOwl.Xamarin.ViewModel;
using NightOwl.Xamarin.Views;
using PCLAppConfig;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NightOwl.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPerson : ContentPage
    {
        private PersonViewModel PersonVM;
        private IImageResizerService _imageResizerService;
        private PersonsService _personsService;
        private IList<Face> trainingData = new List<Face>();
        private FaceRecognitionService _faceRecognitionService;
        private IFaceDetectionService _faceDetectionService;

        private Person _PersonSelected = null;
        private ICollection<Image> _ImageSlots = new List<Image>();
        private int _ImagesPage = 0;

        public AddPerson()
        {
            InitializeComponent();

            PersonVM = new PersonViewModel();
            _personsService = new PersonsService();
            _faceRecognitionService = new FaceRecognitionService();
            _faceDetectionService = new FaceDetectionService();
            _imageResizerService = DependencyService.Get<IImageResizerService>();

            _ImageSlots.Add(image1);
            _ImageSlots.Add(image2);
            _ImageSlots.Add(image3);

            NextPhotos.IsVisible = false;
            PrevPhotos.IsVisible = false;
        }

        void OnNextImgPageClicked(object sender, EventArgs e)
        {
            _ImagesPage++;

            if (_ImagesPage > 0)
                PrevPhotos.IsVisible = true;

            if ((_ImagesPage + 1) * _ImageSlots.Count() >= PersonVM.Faces.Count())
                NextPhotos.IsVisible = false;
            else
                NextPhotos.IsVisible = true;

            SetPhotos();
        }

        void OnPreviousImgPageClicked(object sender, EventArgs e)
        {
            _ImagesPage--;

            if (_ImagesPage > 0)
                PrevPhotos.IsVisible = true;
            else
            {
                _ImagesPage = 0;
                PrevPhotos.IsVisible = false;
            }

            if ((_ImagesPage + 1) * _ImageSlots.Count() >= PersonVM.Faces.Count())
                NextPhotos.IsVisible = false;
            else
                NextPhotos.IsVisible = true;

            SetPhotos();
        }

        void SetPhotos()
        {
            foreach (var img in _ImageSlots)
                img.Source = null;

            var filteredPersons = PersonVM.Faces.Skip(_ImageSlots.Count() * _ImagesPage).Take(_ImageSlots.Count());
            var facePhotoSource = filteredPersons.Zip(_ImageSlots, (n, w) => new { Photo = n, Slot = w });

            foreach (var face in facePhotoSource)
            {
                if(face.Photo.PhotoByteArr != null)
                    face.Slot.Source = ImageSource.FromStream(() => new MemoryStream(face.Photo.PhotoByteArr));
                else if(!string.IsNullOrEmpty(face.Photo.BlobURI))
                    face.Slot.Source = ImageSource.FromUri(new Uri(face.Photo.BlobURI));
            }
        }

        async void OnAddPersonPhotoClicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["SystemErrorNoPermissionForPhot"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium
            });

            if (file == null)
                return;

            Func<Stream> imageStream = (() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            try
            {
                byte[] photo = await _imageResizerService.ResizeImageAsync(GetByteArrayFromStream(imageStream));
                var facePhoto = await _faceDetectionService.DetectFacesAsync(photo);

                if (!facePhoto.Success)
                {
                    await DisplayAlert("Photo error", facePhoto.Error, "Close");
                    return;
                }

                PersonVM.Faces.Insert(0, new Face { PhotoByteArr = facePhoto.Message, BlobURI = string.Empty } );
                SetPhotos();

                if (PersonVM.Faces.Count() > _ImageSlots.Count() && _ImagesPage == 0)
                    NextPhotos.IsVisible = true;
            }
            catch(Exception ex)
            {
                await DisplayAlert("Exception", ex.Message.ToString(), "Close");
            }
        }

        async void OnSelectPersonButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<PeopleList, Person>(this, "PersonPicked", (personSender, personObject) =>
            {
                ClearData();
                SetValues(personObject);
                _PersonSelected = personObject;
    
                if(_PersonSelected.FacePhotos.Count() > _ImageSlots.Count())
                    NextPhotos.IsVisible = true;

                PrevPhotos.IsVisible = false;
            });
            await Navigation.PushAsync(new PeopleList());
        }

        async void OnDeletePersonClicked(object sender, EventArgs e)
        {
            if(_PersonSelected == null)
            {
                await DisplayAlert("Error", "You need to chose person!", "Close");
                return;
            }

            addPerson.IsEnabled = false;
            deletePerson.IsEnabled = false;
            editPerson.IsEnabled = false;

            var actionDeletePerson = await _personsService.DeletePersonAsync(_PersonSelected.Id);
                  
            if (actionDeletePerson.Success)
            {
                await DisplayAlert("Success", "Person deleted", "Close");

                addPerson.IsEnabled = true;
                deletePerson.IsEnabled = true;
                editPerson.IsEnabled = true;
                ClearData();
            }
            else
            {
                await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], actionDeletePerson.Error, ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                ErrorLogger.Instance.LogError(actionDeletePerson.Error);
            }
        }

        async void OnAddPersonsDataButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["AddPersonInvalidNoName"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                return;
            }

            if(_PersonSelected == null && PersonVM.Faces.Count < 1)
            {
                await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["AddPersonInvalidNo"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                return;
            }

            addPerson.IsEnabled = false;
            editPerson.IsEnabled = false;
            deletePerson.IsEnabled = false;

            PersonVM.Username = nameTextBox.Text;
            PersonVM.BirthDate = birthdatePicker.Date;
            PersonVM.MissingDate = missingdatePicker.Date;
            PersonVM.AdditionalInfo = addInfoTextBox.Text;

            if (string.IsNullOrEmpty(PersonVM.AdditionalInfo))
                PersonVM.AdditionalInfo = "";

            try
            {
                IList<byte[]> newPhotos = new List<byte[]>();

                foreach (Face face in PersonVM.Faces)
                {
                    if (face.PhotoByteArr != null && string.IsNullOrEmpty(face.BlobURI))
                        newPhotos.Add(face.PhotoByteArr);
                }

                if (_PersonSelected == null)
                {
                    var addPerson = await _personsService.AddNewPersonAsync(
                        new Person
                        {
                            Name = PersonVM.Username,
                            BirthDate = PersonVM.BirthDate.ToString(),
                            MissingDate = PersonVM.MissingDate.ToString(),
                            AdditionalInfo = PersonVM.AdditionalInfo,
                            CreatorId = App.CurrentUser,
                            Photos = newPhotos
                        }
                    );

                    if (addPerson.Success)
                    {
                        await DisplayAlert("Success", "Person added", "Close");
                        ClearData();
                    }
                    else
                    {
                        await DisplayAlert("Adding error" + ConfigurationManager.AppSettings["SystemErrorTitle"], addPerson.Error, ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                        ErrorLogger.Instance.LogError(addPerson.Error);
                    }
                }
                else
                {
                    var updatePerson = await _personsService.UpdatePersonAsync(
                                          new Person
                                          {
                                              Id = PersonVM.Id,
                                              Name = PersonVM.Username,
                                              BirthDate = PersonVM.BirthDate.ToString(),
                                              MissingDate = PersonVM.MissingDate.ToString(),
                                              AdditionalInfo = PersonVM.AdditionalInfo,
                                              CreatorId = App.CurrentUser,
                                              Photos = newPhotos
                                          },
                                          _PersonSelected.Id
                                      );

                    if (updatePerson.Success)
                    {
                        await DisplayAlert("Success", "Person updated", "Close");
                    }
                    else
                    {
                        await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], updatePerson.Error, ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                        ErrorLogger.Instance.LogError(updatePerson.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Instance.LogException(ex);
                await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["SystemErrorMessage"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
            }
            finally
            {
                if (await TrainRecognizer())
                    await DisplayAlert("Viksasg erai", "Viskas gerai", "Viskas geria");

                addPerson.IsEnabled = true;
                editPerson.IsEnabled = true;
                deletePerson.IsEnabled = true;
            }
        }

        private void SetValues(Person person)
        {
            bool sameCreator = string.Equals(person.CreatorId, App.CurrentUser);

            addPerson.IsVisible = sameCreator;
            addPhotoButton.IsVisible = sameCreator;
            deletePerson.IsVisible = sameCreator;

            if (!sameCreator)
            {
                creatorNameLabel.IsVisible = true;
                creatorNameValue.IsVisible = true;
                creatorNameValue.Text = person.CreatorName;

                creatorEmailLabel.IsVisible = true;
                creatorEmailValue.IsVisible = true;
                creatorEmailValue.Text = person.CreatorEmail;

                creatorPhoneLabel.IsVisible = true;
                creatorPhoneValue.IsVisible = true;
                creatorPhoneValue.Text = person.CreatorPhone;
            }

            if (!string.IsNullOrEmpty(person.Name))
                nameTextBox.Text = person.Name;

            if (!string.IsNullOrEmpty(person.BirthDate))
            {
                   /*    var birthDate = DateTime.ParseExact(person.BirthDate,
                                       "dd / m/ yyyy hh:mm:ss tt",
                                       CultureInfo.InvariantCulture);
                birthdatePicker.Date = birthDate;*/
            }

            if (!string.IsNullOrEmpty(person.MissingDate))
            {
              /*  var missingDate = DateTime.ParseExact(person.MissingDate,
                                       "dd / m/ yyyy hh:mm:ss tt",
                                   CultureInfo.InvariantCulture);
                missingdatePicker.Date = missingDate;*/
            }

            if(!string.IsNullOrEmpty(person.AdditionalInfo))
                addInfoTextBox.Text = person.AdditionalInfo;

            PersonVM.Id = person.Id;
            PersonVM.Faces = person.FacePhotos.ToList();
            SetPhotos();
        }

        private void ClearData()
        {
            PersonVM.Faces.Clear();
            PersonVM.Username = "";
            PersonVM.MissingDate = DateTime.Now;
            PersonVM.BirthDate = DateTime.Now;
            PersonVM.AdditionalInfo = "";

            nameTextBox.Text = "";
            addInfoTextBox.Text = "";
            birthdatePicker.Date = birthdatePicker.MinimumDate;
            missingdatePicker.Date = missingdatePicker.MaximumDate;

            _ImagesPage = 0;
            NextPhotos.IsVisible = false;
            PrevPhotos.IsVisible = false;

            addPerson.IsEnabled = true;
            editPerson.IsEnabled = true;
            deletePerson.IsEnabled = true;

            creatorNameLabel.IsVisible = false;
            creatorNameValue.IsVisible = false;
            creatorNameValue.Text = "";

            creatorEmailLabel.IsVisible = false;
            creatorEmailValue.IsVisible = false;
            creatorEmailValue.Text = "";

            creatorPhoneLabel.IsVisible = false;
            creatorPhoneValue.IsVisible = false;
            creatorPhoneValue.Text = "";

            foreach (var img in _ImageSlots)
                img.Source = null;
        }
        private async Task<bool> TrainRecognizer()
        {
            var trainRecognizer = await _faceRecognitionService.TrainFacesAsync();

            if (trainRecognizer.Success)
                return true;

            trainingData.Clear();
            ErrorLogger.Instance.LogError(trainRecognizer.Error);
            return false;
        }


        public byte[] GetByteArrayFromStream(Func<Stream> stream)
        {
            var memoryStream = stream.Invoke();

            if (!(memoryStream is MemoryStream ms))
            {
                ms = new MemoryStream();
                memoryStream.CopyTo(ms);
            }
            byte[] face = ms.ToArray();
            ms.Dispose();

            return face;
        }
    }
}