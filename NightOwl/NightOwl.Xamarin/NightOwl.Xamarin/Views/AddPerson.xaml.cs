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
        private List<Face> trainingData = new List<Face>();
        private FaceRecognitionService _faceRecognitionService;

        public AddPerson()
        {
            InitializeComponent();

            PersonVM = new PersonViewModel();
            _personsService = new PersonsService();
            _faceRecognitionService = new FaceRecognitionService();
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

            byte[] photo = await _imageResizerService.ResizeImageAsync(GetByteArrayFromStream(imageStream), 400, 400);
            image.Source = ImageSource.FromStream(() => new MemoryStream(photo));

            PersonVM.Faces.Add(photo);
        }

        async void OnSelectPersonButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<PeopleList, Person>(this, "PersonPicked", (personSender, personObject) =>
            {
                ClearData();
                SetValues(personObject);
            });
            //PeopleList peopleList = new PeopleList();
            //peopleList.PersonSelected += OnPersonSelectedFromList;
            await Navigation.PushAsync(new PeopleList());
        }

        async void OnAddPersonsDataButtonClicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                await DisplayAlert("Error", "reik irayti varda", "error");
                return;
            }

            if(PersonVM.Faces.Count < 1)
            {
                await DisplayAlert("Error", "reik bent vienos nuotraukos", "error");
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
                var addPerson = await _personsService.AddNewPersonAsync(
                                                                            new Person
                                                                            {
                                                                                Name = PersonVM.Username,
                                                                                BirthDate = PersonVM.BirthDate.ToString(),
                                                                                MissingDate = PersonVM.MissingDate.ToString(),
                                                                                AdditionalInfo = PersonVM.AdditionalInfo.ToString()
                                                                            }
                                                                        );

                if (addPerson.Success)
                {
                    foreach (byte[] face in PersonVM.Faces)
                    {
                        trainingData.Add(new Face
                        {
                            Photo = face,
                            PersonName = PersonVM.Username
                        });
                    }

                    bool trainSuccess = await TrainRecognizer();

                    if (trainSuccess)
                    {
                        ClearData();
                        await DisplayAlert("Person added", "Person successfully created.", "Close");
                    }
                    else
                    {
                        PersonVM.Faces.Clear();
                        await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["SystemErrorMessage"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                    }
                }
                else
                {
                    await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], addPerson.Error, ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                    ErrorLogger.Instance.LogError(addPerson.Error);
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

          /*  if (!string.IsNullOrEmpty(person.BirthDate))
            {
                var birthDate = DateTime.ParseExact(person.BirthDate,
                                      "yyyyMMdd",
                                       CultureInfo.InvariantCulture);
                birthdatePicker.Date = birthDate;
            }

            if (!string.IsNullOrEmpty(person.MissingDate))
            {
                var missingDate = DateTime.ParseExact(person.MissingDate,
                                  "yyyyMMdd",
                                   CultureInfo.InvariantCulture);
                missingdatePicker.Date = missingDate;
            */

            if(!string.IsNullOrEmpty(person.AdditionalInfo))
                addInfoTextBox.Text = person.AdditionalInfo;
        }

        private async Task<bool> TrainRecognizer()
        {
            if (trainingData.Count == 0)
            {
                ErrorLogger.Instance.LogError("No training data while adding person..");
                return false;
            }
            Trainer trainer = new Trainer
            {
                Data = trainingData,
                Threshold = 4000,
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