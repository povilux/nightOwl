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

        private int _PersonSelected = -1;

        public AddPerson()
        {
            InitializeComponent();

            PersonVM = new PersonViewModel();
            _personsService = new PersonsService();
            _faceRecognitionService = new FaceRecognitionService();
            _faceDetectionService = new FaceDetectionService();
            _imageResizerService = DependencyService.Get<IImageResizerService>();

        }

        /*   public void OnPersonSelectedFromList(object sender, PersonSelectedEventArgs e)
           {
               ClearPhotoList();
               SetValues(e.SelectedPerson);
           }*/

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
                    await DisplayAlert("Error", facePhoto.Error, "Close");
                    return;
                }

                image.Source = ImageSource.FromStream(() => new MemoryStream(facePhoto.Message));
                PersonVM.Faces.Add(facePhoto.Message);
            }
            catch(Exception ex)
            {
                await DisplayAlert("Exception", ex.Message, "Close");
            }
        }

        async void OnSelectPersonButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<PeopleList, Person>(this, "PersonPicked", (personSender, personObject) =>
            {
                ClearData();
                SetValues(personObject);
                _PersonSelected = personObject.Id;
            });
            //PeopleList peopleList = new PeopleList();
            //peopleList.PersonSelected += OnPersonSelectedFromList;
            await Navigation.PushAsync(new PeopleList());
        }

        async void OnAddPersonsDataButtonClicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["AddPersonInvalidNoName"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                return;
            }

            if(_PersonSelected == -1 && PersonVM.Faces.Count < 1)
            {
                await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["AddPersonInvalidNo"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                return;
            }

            addPerson.IsEnabled = false;

            PersonVM.Username = nameTextBox.Text;
            PersonVM.BirthDate = birthdatePicker.Date;
            PersonVM.MissingDate = missingdatePicker.Date;
            PersonVM.AdditionalInfo = addInfoTextBox.Text;

            if (string.IsNullOrEmpty(PersonVM.AdditionalInfo))
                PersonVM.AdditionalInfo = "";

            try
            {
                if (_PersonSelected == -1)
                {
                    var addPerson = await _personsService.AddNewPersonAsync(
                        new Person
                        {
                            Name = PersonVM.Username,
                            BirthDate = PersonVM.BirthDate.ToString(),
                            MissingDate = PersonVM.MissingDate.ToString(),
                            AdditionalInfo = PersonVM.AdditionalInfo,
                            CreatorId = App.CurrentUser.ToString(),
                            Photos = PersonVM.Faces
                        }
                    );

                    if (addPerson.Success)
                    {
                        await DisplayAlert("Success", "Person added", "Close");
                    }
                    else
                    {
                        await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], addPerson.Error, ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
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
                                              CreatorId = App.CurrentUser.ToString(),
                                              Photos = PersonVM.Faces
                                          },
                                          _PersonSelected
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
                addPerson.IsEnabled = true;
            }
        }

        private void SetValues(Person person)
        {
            if(!string.IsNullOrEmpty(person.Name))
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
        }

        private async Task<bool> TrainRecognizer()
        {
            if (trainingData.Count == 0)
                return false;
            
            Trainer trainer = new Trainer
            {
                Data = trainingData,
                Threshold = int.Parse(ConfigurationManager.AppSettings["RecognizerThreshold"]),
                NumOfComponents = trainingData.Count
            };

            var trainRecognizer = await _faceRecognitionService.TrainFacesAsync(trainer);

            if (trainRecognizer.Success)
                return true;
  
            trainingData.Clear();
            ErrorLogger.Instance.LogError(trainRecognizer.Error);
            return false;
        }

        public void ClearData()
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