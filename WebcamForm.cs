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

    public partial class WebcamForm : Form
    {
        private VideoCapture capture;

        private void ProcessFrame(object sender, EventArgs e)
        {
            /*
            Image<Bgr, Byte> ImageFrame = capture.QueryFrame().ToImage<Bgr, Byte>();
            imgCamUser.Image = ImageFrame;
            */
            CascadeClassifier _cascadeClassifier;
            _cascadeClassifier = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");
            using (var imageFrame = capture.QueryFrame().ToImage<Bgr, Byte>())
            {
                if (imageFrame != null)
                {
                    var grayframe = imageFrame.Convert<Gray, Byte>();
                    var faces = _cascadeClassifier.DetectMultiScale(grayframe, 1.1, 10, Size.Empty); //the actual face detection happens here
                    foreach (var face in faces)
                    {
                        Image<Bgr, byte> faceImage = imageFrame.Copy(face);
                        faceImage = ImageHandler.ResizeImage(faceImage);
                        var grayFace = faceImage.Convert<Gray, Byte>();
                        var result = Recognizer.RecognizeFace(grayFace);
                        if (result > 0)
                        {
                            List<String> names = new List<String>();
                            var personsDataQuery = from p in MainForm.persons select new { p.Name };

                            foreach (var person in personsDataQuery)
                                names.Add(person.Name);

                            string name = names.ElementAt(result - 1);

                            CvInvoke.PutText(imageFrame, name, new Point(face.Location.X + 10,
                                face.Location.Y - 10), Emgu.CV.CvEnum.FontFace.HersheyComplex, 1.0, new Bgr(0, 255, 0).MCvScalar);
                        }
                        imageFrame.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them

                    }
                }
                imgCamUser.Image = imageFrame;
            }
        }

        public WebcamForm()
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
            MainForm.closeMainForm();
        }
    }
}
