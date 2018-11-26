using Plugin.Media;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NightOwl.Xamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VideoRecognition : ContentPage
	{

        public VideoRecognition ()
		{
			InitializeComponent ();

            string FilePath = "";
            pickVideo.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsPickVideoSupported)
                {
                    await DisplayAlert("Videos Not Supported", ":( Permission not granted to videos.", "OK");
                    return;
                }
                var file = await CrossMedia.Current.PickVideoAsync();
                FilePath = file.Path;

                await CrossMediaManager.Current.Play(file.Path, MediaFileType.Video);
                playStopButton.Text = "Stop";

                if (file == null)
                    return;

                //await DisplayAlert("Video Selected", "Location: " + file.Path, "OK");
                file.Dispose();
            };

            playStopButton.Clicked += async (sender, args) =>
            {
                if(FilePath == "")
                {
                    playStopButton.IsVisible = false;
                }
                else playStopButton.IsVisible = true;
                if (playStopButton.Text == "Play")
                {
                    await CrossMediaManager.Current.Play(FilePath, MediaFileType.Video);
                    playStopButton.Text = "Stop";
                }

                else if (playStopButton.Text == "Stop")
                {
                    await CrossMediaManager.Current.Pause();
                    playStopButton.Text = "Play";
                }
            };
        }

        public async void OnRecognizedPeopleListClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecognizedPeopleList());
        }
    }
}