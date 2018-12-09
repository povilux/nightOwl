using System;
using System.ComponentModel;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(NightOwl.Xamarin.VideoPlayer),
                          typeof(NightOwl.Xamarin.UWP.VideoPlayerRenderer))]

namespace NightOwl.Xamarin.UWP
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, MediaPlayerElement>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            base.OnElementChanged(args);

            if (args.NewElement != null)
            {
                if (Control == null)
                {
                    MediaPlayerElement mediaElement = new MediaPlayerElement();

                   
                    SetNativeControl(mediaElement);


                    // mediaElement.MediaOpened += OnMediaElementMediaOpened;
                    //mediaElement.CurrentStateChanged += OnMediaElementCurrentStateChanged;
                }

                SetAreTransportControlsEnabled();
                SetSource();
                SetAutoPlay();
            }
        }



        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == VideoPlayer.AreTransportControlsEnabledProperty.PropertyName)
            {
                SetAreTransportControlsEnabled();
            }
            else if(args.PropertyName == VideoPlayer.SourceProperty.PropertyName)
            {
                SetSource();
            }
            else if (args.PropertyName == VideoPlayer.PlayProperty.PropertyName)
            {
                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Source = (MediaSource.CreateFromUri(new Uri("file:///C:/users/ugnes/Desktop/output.wmv")));
                mediaPlayer.Play();

                Control.SetMediaPlayer(mediaPlayer);
            }

        }

        void SetAreTransportControlsEnabled()
        {
            Control.AreTransportControlsEnabled = Element.AreTransportControlsEnabled;
        }

        void SetSource()
        {
            if(!string.IsNullOrEmpty(Element.Source))
                 Control.Source = MediaSource.CreateFromUri(new Uri("file:///" + Element.Source));
             
        }

        void SetAutoPlay()
        {
            Control.AutoPlay = true; //Element.AutoPlay;
        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {

                //Control.MediaOpened -= OnMediaElementMediaOpened;
                // Control.CurrentStateChanged -= OnMediaElementCurrentStateChanged;
            }

            base.Dispose(disposing);
        }
    }
}
