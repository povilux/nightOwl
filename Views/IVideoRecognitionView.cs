using System;
using System.Drawing;

namespace nightOwl
{
    public interface IVideoRecognitionView
    {
        event EventHandler BackButtonClicked;
        event EventHandler CloseButtonClicked;
        event EventHandler<SelectedFileEventArgs> SelectedFile;

        Bitmap CurrentVideoFrame { set; } 

    }
}