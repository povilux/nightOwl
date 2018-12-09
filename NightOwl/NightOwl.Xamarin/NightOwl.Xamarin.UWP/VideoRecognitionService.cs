using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Xamarin.Forms;

namespace NightOwl.Xamarin.UWP
{
    public class VideoRecognitionService
    {
        public VideoRecognitionService() { }

        public View CreateMediaPlayerElement()
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            return (View)mediaPlayer;
        }
    }
}
