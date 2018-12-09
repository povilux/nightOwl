using NightOwl.Xamarin.Services;
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
        private IVideoPicker _videoPicker;

        public VideoRecognition ()
        {
            InitializeComponent();
            _videoPicker = DependencyService.Get<IVideoPicker>();


        }

        async void OnPickVideoClicked(object sender, EventArgs e)
        {
            string url = await _videoPicker.GetVideoFileAsync();
            mediaPlayer.Source = url;
            await DisplayAlert("as0", mediaPlayer.Source, "asd");

            mediaPlayer.Play = true;
        }
    }
}