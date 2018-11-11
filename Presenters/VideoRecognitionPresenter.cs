using nightOwl.BusinessLogic;
using System;
using System.Windows.Forms;

namespace nightOwl.Presenters
{
    class VideoRecognitionPresenter
    {
        private IVideoRecognitionView _view;

        public VideoRecognitionPresenter(IVideoRecognitionView view)
        {
            _view = view;

            _view.SelectedFile += new EventHandler<SelectedFileEventArgs>(OnUserSelectedFile);
            _view.BackButtonClicked += new EventHandler(OnBackButtonClicked);
            _view.CloseButtonClicked += new EventHandler(OnCloseButtonClicked);
        }

        public void OnBackButtonClicked(object sender, EventArgs e)
        {
            PersonRecognizer.Instance.CloseCapture();
        }

        public void OnCloseButtonClicked(object sender, EventArgs e)
        {
            PersonRecognizer.Instance.CloseCapture();

            Application.Exit();
        }

        public void OnUserSelectedFile(object sender, SelectedFileEventArgs e)
        {
            PersonRecognizer.Instance.LoadTrainedFaces();
            PersonRecognizer.Instance.StartCapture((frame) => _view.CurrentVideoFrame = frame, fromVideo: true, CurrentVideoFile: e.SelectedFileName);
        }
    }
}