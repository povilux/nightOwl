using nightOwl.BusinessLogic;
using nightOwl.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nightOwl.Presenters
{
    class RecognitionPresenter
    {
        private ILiveView _view;

        public RecognitionPresenter(ILiveView view)
        {
            _view = view;
            _view.VideoStreamStarting += new EventHandler<RecognitionEventArgs>(OnVideoStreamStarting);
            _view.CloseButtonClicked += new EventHandler(OnProgramClosing);
            _view.BackButtonClicked += new EventHandler(OnBackButtonClicked);
        }

        public void OnVideoStreamStarting(object sender, RecognitionEventArgs e)
        {
            PersonRecognizer.Instance.LoadTrainedFaces();
            PersonRecognizer.Instance.StartCapture((frame) => _view.CurrentFrame = frame, e.FromVideo);
        }

        public void OnBackButtonClicked(object sender, EventArgs e)
        {
            PersonRecognizer.Instance.CloseCapture();
        }

        public void OnProgramClosing(object sender, EventArgs e)
        {
            PersonRecognizer.Instance.CloseCapture();
            Application.Exit();
        }

    }
}
