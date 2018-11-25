using NightOwl.Xamarin.Services;
using PCLAppConfig;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NightOwl.Xamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PictureRecognition : ContentPage
	{
        private IFaceRecognitionService _faceRecognitionService;

        private Func<Stream> imageStream;

        public PictureRecognition (IFaceRecognitionService faceRecognitionService)
		{
			InitializeComponent ();
            _faceRecognitionService = faceRecognitionService;

            pickPhoto.Clicked += OnSelectedPhotoAsync;

            /*takePhoto.Clicked += async (sender, args) =>
            {

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Test",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Front
                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            };*/
        }

        public async void OnSelectedPhotoAsync(object sender, EventArgs e)
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

            image.Source = GetImageFromFile(file);

            try
            {
                byte[] photoByteArray = GetByteArrayFromStream(imageStream);
                string recognizedPersons = await RecognizePersonsFromPhtAsync(photoByteArray);

                if(recognizedPersons == null)
                {
                    await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["SystemErrorMessage"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                    return;
                }

                if (!string.IsNullOrEmpty(recognizedPersons))
                    await DisplayAlert(ConfigurationManager.AppSettings["RecognizedPersonTitle"], string.Join(Environment.NewLine, recognizedPersons), ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                else
                    await DisplayAlert(ConfigurationManager.AppSettings["NotRecognizedPersonTitle"], ConfigurationManager.AppSettings["NotRecognizedPersonText"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);           
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

        public async Task<string> RecognizePersonsFromPhtAsync(byte[] photo)
        {
            var recognition = await _faceRecognitionService.RecognizeFacesAsync(photo);

           if (recognition.Success)
           {
                return recognition.Message;
           }
           else
           {
                ErrorLogger.Instance.LogError(recognition.Error);
                return null;
            }
        }
    }
}