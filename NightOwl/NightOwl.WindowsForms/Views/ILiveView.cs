using System;
using System.Drawing;

namespace NightOwl.WindowsForms.Views
{
    public interface ILiveView
    {
        Bitmap CurrentFrame { set; }
        event EventHandler<RecognitionEventArgs> VideoStreamStarting;
        event EventHandler BackButtonClicked;
        event EventHandler CloseButtonClicked;
    }
}