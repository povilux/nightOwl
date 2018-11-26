using System;
using System.Collections.Generic;
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
        private Func<Stream> imageStream;
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

            addPerson.Clicked += OnAddPersonClicked;
            addPersonPhoto.Clicked += OnAddPersonPhotoClicked;
            trainRecognizer.Clicked += OnTrainRecognizerClicked;

            trainingData.Clear();
        }

        async void OnTrainRecognizerClicked(object sender, EventArgs e)
        {
            if(trainingData.Count == 0)
            {
                await DisplayAlert("Error", "Nothing to save", "Close");
                return;
            }

            Trainer trainer = new Trainer
            {
                Data = trainingData,
                Threshold = 4000,
                NumOfComponents = trainingData.Count
            };
            var trainRecognizer = await _faceRecognitionService.TrainFacesAsync(trainer);

            if (trainRecognizer.Success)
                await DisplayAlert("Saved", "Information saved successfully", "Close");
            else
            {
                trainingData.Clear();
                await DisplayAlert("Error", "Error: " + trainRecognizer.Error, "Close");
                ErrorLogger.Instance.LogError(trainRecognizer.Error);
            }
        }

        async void OnAddPersonClicked(object sender, EventArgs e)
        {
            PersonVM.Username = PersonName.Text;
            PersonVM.BirthDate = BirthDate.Date;
            PersonVM.AdditionalInfo = AdditionalInfo.Text;
            PersonVM.MissingDate = MissingDate.Date;
            
            try
            {
                Person person = new Person
                {
                    Name = PersonVM.Username,
                    BirthDate = PersonVM.BirthDate.ToString(),
                    MissingDate = PersonVM.MissingDate.ToString(),
                    AdditionalInfo = PersonVM.AdditionalInfo.ToString()
                 //   Creator = App.CurrentUser
                };

                var addPerson = await _personsService.AddNewPersonAsync(person);
               
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
                    PersonVM.Faces.Clear();
                    PersonVM.Username = PersonName.Text = "";
                    PersonVM.AdditionalInfo = AdditionalInfo.Text = "";

                    await DisplayAlert("Person added", "Person successfully created.", "Close");
                   
                }
                else
                {
                    await DisplayAlert("Error", "Error: "+  addPerson.Error, "Close");
                    ErrorLogger.Instance.LogError(addPerson.Error);
                }
            }            
            catch (Exception ex)
            {
                ErrorLogger.Instance.LogException(ex);
                await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["SystemErrorMessage"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
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

            imageStream = (() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            
            PersonVM.Faces.Add(await _imageResizerService.ResizeImageAsync(GetByteArrayFromStream(imageStream), 400, 400));

           
            await DisplayAlert("Success", "Photo was added", "Close");
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

        async void OnSelectPersonButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PeopleList());
        }
    }
}