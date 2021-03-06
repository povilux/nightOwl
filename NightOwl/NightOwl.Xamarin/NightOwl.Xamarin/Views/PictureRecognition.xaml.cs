﻿using NightOwl.Xamarin.Services;
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
        private IImageResizerService _imageResizerService;

        private Func<Stream> imageStream;

        public PictureRecognition ()
		{
			InitializeComponent ();
            _faceRecognitionService = new FaceRecognitionService();
            _imageResizerService = DependencyService.Get<IImageResizerService>();

            pickPhoto.Clicked += OnSelectedPhotoAsync;
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

            imageStream = (() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            byte[] photo = await _imageResizerService.ResizeImageAsync(GetByteArrayFromStream(imageStream));
            

            image.Source = ImageSource.FromStream(() => new MemoryStream(photo));

            try
            {
                string recognizedPersons = await RecognizePersonsFromPhtAsync(photo);

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