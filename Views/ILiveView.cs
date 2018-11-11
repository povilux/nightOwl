using System;
using System.Drawing;

namespace nightOwl
{
    public interface ILiveView
    {
        Bitmap CurrentFrame { set; }
        event EventHandler<RecognitionEventArgs> VideoStreamStarting;
        event EventHandler BackButtonClicked;
        event EventHandler CloseButtonClicked;
    }
}