using NightOwl.Xamarin.Services;
using NightOwl.Xamarin.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NightOwl.Xamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnSelectVideoButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VideoRecognition());
        }

        async void OnSelectPictureButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PictureRecognition(new FaceRecognitionService()));
        }

        async void OnWatchCameraButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraRecognition());
        }

        async void OnAddNewPersonButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPerson());
        }

        async void OnMapButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Map());
        }
    }
}
