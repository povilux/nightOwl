using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using nightOwl.Views;
using nightOwl.BusinessLogic;
using nightOwl.Presenters;

namespace nightOwl
{
    public partial class LiveView : Form, ILiveView
    {
        private readonly RecognitionPresenter _presenter;

        public LiveView()
        {
            InitializeComponent();
            _presenter = new RecognitionPresenter(this);
        }

        public Bitmap CurrentFrame {
            set { imgCamUser.Image = value; }
        }
    
        private void LiveView_Load(object sender, EventArgs e)
        {
            RecognitionEventArgs args = new RecognitionEventArgs
            {
                FromVideo = false
            };

            VideoStreamStarting(sender, args);
        }

        private void AnalyzeButton_Click(object sender, EventArgs e)
        {
         /*   Application.Idle -= rh.ProcessFrameForWebcam;
            imageToAnalyze = capture.QueryFrame().ToImage<Bgr, Byte>();
            capture.Dispose();
            PictureForm picForm = new PictureForm(imageToAnalyze);
            picForm.Show();
            Close();*/
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            CloseButtonClicked(sender, e);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            BackButtonClicked(sender, e);
            Close();
            FirstPageView.self.Show();
        }

        public event EventHandler<RecognitionEventArgs> VideoStreamStarting;
        public event EventHandler CloseButtonClicked;
        public event EventHandler BackButtonClicked;

    }

    public class RecognitionEventArgs : EventArgs
    {
        public bool FromVideo { get; set; }
    }

}
