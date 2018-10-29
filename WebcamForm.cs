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

namespace nightOwl
{

    public partial class WebcamForm : Form
    {
        private VideoCapture capture;
        private Image<Bgr, Byte> imageToAnalyze;
        private RecognitionHandler rh;

        public WebcamForm()
        {
            InitializeComponent();
        }
        
        private void imageBox1_Click(object sender, EventArgs e)
        {
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if(capture == null)
            {
                try
                {
                    capture = new VideoCapture();
                }
                catch(NullReferenceException exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            if(capture != null)
            {
                rh = new RecognitionHandler(imgCamUser,capture);
                Application.Idle -= rh.ProcessFrameForWebcam;
                Application.Idle += rh.ProcessFrameForWebcam;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
            FirstPageView.self.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FirstPageView.CloseMainForm();
        }

        private void AnalyzeButton_Click(object sender, EventArgs e)
        {
            Application.Idle -= rh.ProcessFrameForWebcam;
            imageToAnalyze = capture.QueryFrame().ToImage<Bgr, Byte>();
            capture.Dispose();
            PictureForm picForm = new PictureForm(imageToAnalyze);
            picForm.Show();
            Close();
        }
    }
}
