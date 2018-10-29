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
using Emgu.CV.Structure;
using nightOwl.Views;

namespace nightOwl
{
    public partial class PictureForm : Form
    {
        VideoCapture capture;
        public static Image<Bgr, Byte> image;
        private RecognitionHandler rh;

        public PictureForm(Image<Bgr, Byte> img)
        {
            InitializeComponent();
            image = img;
            capture = new VideoCapture();
            if(capture == null)
            {
                return;
            } else
            {
                rh = new RecognitionHandler(imageBox1, capture);
                rh.ProcessFrameForPicture();
            }
        }

        private void BackToCamButton_Click(object sender, EventArgs e)
        {
            capture.Dispose();
            Close();
            WebcamForm camForm = new WebcamForm();
            camForm.Show();
        }

        private void BackToMainButton_Click(object sender, EventArgs e)
        {
            capture.Dispose();
            Close();
            FirstPageView.self.Show();
        }

        private void PictureForm_Load(object sender, EventArgs e)
        {

        }
    }
}
