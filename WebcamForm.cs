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
        private readonly int frameRecognitionBundleSize = 300;
        private VideoCapture capture;

        private void ProcessFrame(object sender, EventArgs e)
        {
            /*
            Image<Bgr, Byte> ImageFrame = capture.QueryFrame().ToImage<Bgr, Byte>();
            imgCamUser.Image = ImageFrame;
            */
            CascadeClassifier _cascadeClassifier;
            _cascadeClassifier = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");

            int frameNumber = 1;
            List<Rectangle> facesToShowList = new List<Rectangle>();
            List<string> namesToShowList = new List<string>();

            using (var imageFrame = capture.QueryFrame().ToImage<Bgr, Byte>())
            {
                if (imageFrame != null)
                {

                    if(frameNumber == 1)
                    {
                        var grayframe = imageFrame.Convert<Gray, Byte>();
                        Rectangle[] faces = _cascadeClassifier.DetectMultiScale(grayframe, 1.1, 10, Size.Empty); //the actual face detection happens here
                        Parallel.ForEach(faces, face =>
                        {
                            facesToShowList.Add(face);
                            try
                            {
                                Image<Bgr, byte> faceImage = imageFrame.Copy(face);
                                faceImage = ImageHandler.ResizeImage(faceImage);
                                var grayFace = faceImage.Convert<Gray, Byte>();
                                var result = Recognizer.RecognizeFace(grayFace);
                                if (result > 0)
                                {
                                    string name = MainForm.names.ElementAt(result - 1);
                                    namesToShowList.Add(name);
                                    Emgu.CV.CvInvoke.PutText(imageFrame, name, new Point(face.Location.X + 10,
                                        face.Location.Y - 10), Emgu.CV.CvEnum.FontFace.HersheyComplex, 1.0, new Bgr(0, 255, 0).MCvScalar);
                                }
                                else
                                {
                                    namesToShowList.Add("unknown");
                                }
                                imageFrame.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them
                            }
                            catch { }

                            
                        });
                        frameNumber++;
                    }
                    if(frameNumber > 1)
                    {
                        for(int i = 0; i < facesToShowList.Count; i++)
                        {
                            try
                            {
                                var face = facesToShowList.ElementAt(i);
                                string name = namesToShowList.ElementAt(i);
                                imageFrame.Draw(face, new Bgr(Color.BurlyWood), 3);
                                if (name != "unknown")
                                {
                                    Emgu.CV.CvInvoke.PutText(imageFrame, name, new Point(face.Location.X + 10,
                                        face.Location.Y - 10), Emgu.CV.CvEnum.FontFace.HersheyComplex, 1.0, new Bgr(0, 255, 0).MCvScalar);
                                }
                            } catch { }
                            
                        }
                        frameNumber++;
                        if(frameNumber % frameRecognitionBundleSize == 1)
                        {
                            frameNumber = 1;
                        }
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

        private void ButtonToPictureFrame_Click(object sender, EventArgs e)
        {
            DataStorage.picFromWebcam = capture.QueryFrame().ToImage<Bgr, Byte>();
            Close();
            PictureForm picForm = new PictureForm();
            picForm.Show();
        }
    }
}
