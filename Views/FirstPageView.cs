using System;
using System.Windows.Forms;
using nightOwl.Models;

namespace nightOwl.Views
{
    public partial class FirstPageView : Form
    {
        public static FirstPageView self;

        public FirstPageView()
        {
            InitializeComponent();
            self = this;
        }

        public static void CloseMainForm()
        {
            Application.Exit();
        }

        private void SelectVideoButton_Click(object sender, EventArgs e)
        {
            VideoRecognitionView videoRecognition = new VideoRecognitionView
            {
                StartPosition = FormStartPosition.Manual,
                Location = Location
            };
            videoRecognition.Show();
            Hide();
        }

        private void WatchCameraButton_Click(object sender, EventArgs e)
        {
            LiveView webCamForm = new LiveView
            {
                StartPosition = FormStartPosition.Manual,
                Location = Location
            };
            webCamForm.Show();
            Hide();
        }

        private void ShowMapButton_Click(object sender, EventArgs e)
        {
            MapView lsmForm = new MapView(new PersonModel())
            {
                StartPosition = FormStartPosition.Manual,
                Location = Location
            };
            lsmForm.Show();
            Hide();
        }

        private void AddPersonButton_Click(object sender, EventArgs e)
        {
            AddPersonView AddPersonForm = new AddPersonView(new PersonModel())
            {
                StartPosition = FormStartPosition.Manual,
                Location = Location
            };
            AddPersonForm.Show();
            Hide();
        }
    }
}
