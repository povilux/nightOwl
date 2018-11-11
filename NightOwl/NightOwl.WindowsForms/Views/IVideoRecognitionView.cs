using System;
using System.Drawing;

namespace NightOwl.WindowsForms.Views
{
    public interface IVideoRecognitionView
    {
        event EventHandler BackButtonClicked;
        event EventHandler CloseButtonClicked;
        event EventHandler<SelectedFileEventArgs> SelectedFile;

        Bitmap CurrentVideoFrame { set; } 

    }
}