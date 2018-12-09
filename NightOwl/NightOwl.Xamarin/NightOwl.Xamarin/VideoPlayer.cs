using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NightOwl.Xamarin
{
    public class VideoPlayer : View, IVideoPlayerController
    {
        // Source property
        public static readonly BindableProperty PlayProperty =
            BindableProperty.Create(nameof(PlayProperty), typeof(bool), typeof(VideoPlayer), true);

        public bool Play
        {
            set { SetValue(PlayProperty, value); }
            get { return (bool)GetValue(PlayProperty); }
        }

   


        // AreTransportControlsEnabled property
        public static readonly BindableProperty AreTransportControlsEnabledProperty =
            BindableProperty.Create(nameof(AreTransportControlsEnabled), typeof(bool), typeof(VideoPlayer), true);

        // Source property
         public static readonly BindableProperty SourceProperty =
             BindableProperty.Create(nameof(SourceProperty), typeof(string), typeof(VideoPlayer), string.Empty);

         public string Source
         {
             set { SetValue(SourceProperty, value); }
             get { return (string)GetValue(SourceProperty); }
         }

        public bool AreTransportControlsEnabled
        {
            set { SetValue(AreTransportControlsEnabledProperty, value); }
            get { return (bool)GetValue(AreTransportControlsEnabledProperty); }
        }

        private static readonly BindablePropertyKey DurationPropertyKey =
            BindableProperty.CreateReadOnly(nameof(Duration), typeof(TimeSpan), typeof(VideoPlayer), new TimeSpan(),
                propertyChanged: (bindable, oldValue, newValue) => ((VideoPlayer)bindable).SetTimeToEnd());

        public static readonly BindableProperty DurationProperty = DurationPropertyKey.BindableProperty;

        public TimeSpan Duration
        {
            set { SetValue(DurationPropertyKey, value); }
            get { return Duration; }
        }

        public static readonly BindableProperty PositionProperty =
            BindableProperty.Create(nameof(Position), typeof(TimeSpan), typeof(VideoPlayer), new TimeSpan(),
                propertyChanged: (bindable, oldValue, newValue) => ((VideoPlayer)bindable).SetTimeToEnd());

        public TimeSpan Position
        {
            set { SetValue(PositionProperty, value); }
            get { return (TimeSpan)GetValue(PositionProperty); }
        }


        // TimeToEnd property
        private static readonly BindablePropertyKey TimeToEndPropertyKey =
            BindableProperty.CreateReadOnly(nameof(TimeToEnd), typeof(TimeSpan), typeof(VideoPlayer), new TimeSpan());

        public static readonly BindableProperty TimeToEndProperty = TimeToEndPropertyKey.BindableProperty;

        public TimeSpan TimeToEnd
        {
            private set { SetValue(TimeToEndPropertyKey, value); }
            get { return (TimeSpan)GetValue(TimeToEndProperty); }
        }


        void SetTimeToEnd()
        {
            TimeToEnd = Duration - Position;
        }

    }

}
