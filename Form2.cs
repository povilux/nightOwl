using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;


namespace nightOwl
{
    public partial class Form2 : Form
    {
        private FilterInfoCollection VideoCaptureDevices;
        private VideoCaptureDevice FinalVideo;

        public Form2()
        {
            InitializeComponent();
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in VideoCaptureDevices)
            {
                comboBoxAttachedCameras.Items.Add(VideoCaptureDevice.Name);
            }

            comboBoxAttachedCameras.SelectedIndex = 0;

            FinalVideo = new VideoCaptureDevice(VideoCaptureDevices[comboBoxAttachedCameras.SelectedIndex].MonikerString);
            foreach (var capability in FinalVideo.VideoCapabilities)
            {
                comboBoxSupportedModes.Items.Add(capability.FrameSize.ToString());
            }

            comboBoxSupportedModes.SelectedIndex = 0;

            FinalVideo.NewFrame += new NewFrameEventHandler(FinalVideo_NewFrame);
            pictureBoxVideo.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            FinalVideo.VideoResolution = FinalVideo.VideoCapabilities[comboBoxSupportedModes.SelectedIndex];
            FinalVideo.Start();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            FinalVideo.Stop();
        }

        private void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (pictureBoxVideo.Image != null) { pictureBoxVideo.Image.Dispose(); }
            Bitmap tempBitmap = (Bitmap)eventArgs.Frame.Clone();
            pictureBoxVideo.Image = tempBitmap;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FinalVideo != null) FinalVideo.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
