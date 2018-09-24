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

namespace nightOwl
{

    public partial class Form3 : Form
    {
        private VideoCapture capture;

        private void ProcessFrame(object sender, EventArgs e)
        {
            /*
            Image<Bgr, Byte> ImageFrame = capture.QueryFrame().ToImage<Bgr, Byte>();
            imgCamUser.Image = ImageFrame;
            */
            CascadeClassifier _cascadeClassifier;
            _cascadeClassifier = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_alt_tree.xml");
            using (var imageFrame = capture.QueryFrame().ToImage<Bgr, Byte>())
            {
                if (imageFrame != null)
                {
                    var grayframe = imageFrame.Convert<Gray, Byte>();
                    var faces = _cascadeClassifier.DetectMultiScale(grayframe, 1.1, 10, Size.Empty); //the actual face detection happens here
                    foreach (var face in faces)
                    {
                        imageFrame.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them

                    }
                }
                imgCamUser.Image = imageFrame;
            }
        }

        public Form3()
        {
            InitializeComponent();
        }
        
        private void imageBox1_Click(object sender, EventArgs e)
        {
            /*
            VideoCapture capture = new VideoCapture();
            imgCamUser.Image = capture.QueryFrame();
            */
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
                Application.Idle += ProcessFrame;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
            MainForm.self.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm.self.Close();
        }
    }
}
