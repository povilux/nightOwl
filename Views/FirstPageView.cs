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
            VideoPlayerForm videoForm = new VideoPlayerForm();
            videoForm.StartPosition = FormStartPosition.Manual;
            videoForm.Location = Location;
            videoForm.Show();
            this.Hide();
        }

        private void WatchCameraButton_Click(object sender, EventArgs e)
        {
            WebcamForm webCamForm = new WebcamForm();
            webCamForm.StartPosition = FormStartPosition.Manual;
            webCamForm.Location = Location;
            webCamForm.Show();
            this.Hide();
        }

        private void ShowMapButton_Click(object sender, EventArgs e)
        {
            MapView lsmForm = new MapView(new PersonModel());
            lsmForm.StartPosition = FormStartPosition.Manual;
            lsmForm.Location = Location;
            lsmForm.Show();
            this.Hide();
        }

        private void AddPersonButton_Click(object sender, EventArgs e)
        {
            AddPersonView AddPersonForm = new AddPersonView(new PersonModel());
            AddPersonForm.StartPosition = FormStartPosition.Manual;
            AddPersonForm.Location = Location;
            AddPersonForm.Show();
            this.Hide();
        }
    }
}
