using NightOwl.Xamarin.Components;
using NightOwl.Xamarin.Services;
using PCLAppConfig;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.DataGrid;
using Xamarin.Forms.Xaml;

namespace NightOwl.Xamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PictureRecognition : ContentPage
	{
        private IFaceRecognitionService _faceRecognitionService;
        private IImageResizerService _imageResizerService;

        private Func<Stream> imageStream;

        public PictureRecognition ()
		{
			InitializeComponent ();
            _faceRecognitionService = new FaceRecognitionService();
            _imageResizerService = DependencyService.Get<IImageResizerService>();

            pickPhoto.Clicked += OnSelectedPhotoAsync;
            PersonsData.ItemSelected += OnPersonSelected;
        }

        void OnPersonSelected(object sender, EventArgs e)
        {
            DisplayAlert("asd", PersonsData.SelectedItem.ToString(), "asd");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           // _personSelected = null;
            PersonsData.SelectedItem = null;
        }

        async void OnSelectedPhotoAsync(object sender, EventArgs e)
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

            byte[] photo = await _imageResizerService.ResizeImageAsync(GetByteArrayFromStream(imageStream));
            image.Source = ImageSource.FromStream(() => new MemoryStream(photo));

            try
            {
                IEnumerable<Person> recognizedPersons = await RecognizePersonsFromPhtAsync(photo);

                if (recognizedPersons == null)
                {
                    await DisplayAlert(ConfigurationManager.AppSettings["NotRecognizedPersonTitle"], ConfigurationManager.AppSettings["NotRecognizedPersonText"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                    PersonsData.ItemsSource = null;
                    BindingContext = null;
                    return;
                }

                PersonsData.ItemsSource = recognizedPersons;
            }
            catch (Exception ex)
            {
                ErrorLogger.Instance.LogException(ex);
                await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["SystemErrorMessage"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
            }
        }


        public ImageSource GetImageFromFile(MediaFile file)
        {
            imageStream = (() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            return ImageSource.FromStream(imageStream);
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

        public async Task<IEnumerable<Person>> RecognizePersonsFromPhtAsync(byte[] photo)
        {
            var recognition = await _faceRecognitionService.RecognizeFacesAsync(photo);

           if (recognition.Success)
           {
                if (recognition.Message == null || recognition.Message.Count() <= 0)
                    return null;

                return recognition.Message;
           }
           else
           {
                ErrorLogger.Instance.LogError(recognition.Error);
                throw new Exception(recognition.Error);
            }
        }
    }

}