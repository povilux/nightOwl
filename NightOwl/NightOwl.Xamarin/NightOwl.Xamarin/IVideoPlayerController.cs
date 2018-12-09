using System;

namespace NightOwl.Xamarin
{
    public interface IVideoPlayerController
    {
        TimeSpan Duration { set; get; }
    }
}