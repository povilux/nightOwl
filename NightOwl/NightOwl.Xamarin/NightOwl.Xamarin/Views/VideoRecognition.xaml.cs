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
        private IVideoRecognitionService _VideoRecognitionService;

        public VideoRecognition ()
        {
            InitializeComponent();
       /*     _VideoRecognitionService = DependencyService.Get<IVideoRecognitionService>();

            var layout = new StackLayout();
            layout.Children.Add(_VideoRecognitionService.CreateMediaPlayerElement());
            Content = layout;
            */
        }


    }
}