using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using NightOwl.WindowsForms.Presenters;
using NightOwl.WindowsForms.Views;

namespace NightOwl.WindowsForms.Views
{ 
    public partial class VideoRecognitionView : Form, IVideoRecognitionView
    {
        private VideoRecognitionPresenter _presenter;

        public VideoRecognitionView()
        {
            InitializeComponent();

            _presenter = new VideoRecognitionPresenter(this);
        }

        private void VideoRecognitionView_Load(object sender, EventArgs e)
        {
            ImgVideoBox.SizeMode = PictureBoxSizeMode.StretchImage;   
            VideoTrackBar.Hide();
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

        private void ImgVideoBox_Click(object sender, EventArgs e)
        {

        }

        private void VideoTrackBar_Scroll(object sender, EventArgs e)
        {
 
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog SelectVideoFileDialog = new OpenFileDialog
            {
                Title = ConfigurationManager.AppSettings["VideoSelectingTitle"],
                Filter = ConfigurationManager.AppSettings["BrowserFilterVideo"],
                Multiselect = false
            };

            if (SelectVideoFileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedFileEventArgs args = new SelectedFileEventArgs
                {
                    SelectedFileName = SelectVideoFileDialog.FileName
                };

                SelectedFile(sender, args);
            }
        }

        public Bitmap CurrentVideoFrame { set { ImgVideoBox.Image = value;  } }

        public event EventHandler CloseButtonClicked;
        public event EventHandler BackButtonClicked;
        public event EventHandler<SelectedFileEventArgs> SelectedFile;
    }

    public class SelectedFileEventArgs : EventArgs
    {
        public string SelectedFileName
        {
            get; set;
        }
    }
}
