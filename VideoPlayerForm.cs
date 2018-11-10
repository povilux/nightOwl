using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using nightOwl.Data;
using nightOwl.Properties;
using nightOwl.Views;

namespace nightOwl
{
    public partial class VideoPlayerForm : Form
    {
        VideoCapture videocapture;
        bool IsPlaying = false;
        int TotalFrames;
        int CurrentFrameNo;
        Mat CurrentFrame;
        int FPS;

        public VideoPlayerForm()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;     // resize image to fit in pictureBox
            trackBar1.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"D:\";
            openFileDialog1.Title = "Open file";
            openFileDialog1.Filter = "Video Files (*.mp4, *.flv, *.avi| *.mp4;*.flv;*.avi";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                videocapture = new VideoCapture(openFileDialog1.FileName);
                TotalFrames = Convert.ToInt32(videocapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount));
                FPS = Convert.ToInt32(videocapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps));
                IsPlaying = true;
                CurrentFrame = new Mat();
                CurrentFrameNo = 0;
                trackBar1.Minimum = 0;
                trackBar1.Maximum = TotalFrames - 1;
                trackBar1.Value = 0;
                trackBar1.Show();
                PlayVideo();
            }

        }

        private async void PlayVideo()
        {
            if(videocapture == null)
            {
                return;
            }

            CascadeClassifier _cascadeClassifier;
            _cascadeClassifier = new CascadeClassifier(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
     Settings.Default.DataFolderPath + Settings.Default.ImagesFolderPath + Settings.Default.FaceInformationFilePath);

            try
            {
                while (IsPlaying == true && CurrentFrameNo < TotalFrames)
                {
                    videocapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames, CurrentFrameNo);
                    videocapture.Read(CurrentFrame);
                    Image<Bgr, byte> imageFrame = CurrentFrame.ToImage<Bgr, Byte>();    // convert Mat to Emgu.CV.IImage


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
                            if(result != 0)
                            {
                                List<String> names = new List<String>();

                                foreach (var person in DataManagement.Instance.GetPersonsCatalog())
                                    names.Add(person.Name);

                                string name = names.ElementAt(result - 1);
                                CvInvoke.PutText(imageFrame, name, new Point(face.Location.X + 10,
                                    face.Location.Y - 10), Emgu.CV.CvEnum.FontFace.HersheyComplex, 1.0, new Bgr(0, 255, 0).MCvScalar);
                            }
                            //imageFrame.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them

                        }
                    }

                    CurrentFrame = imageFrame.Mat;                  // convert Emgu.CV.IImage back to Mat
                    pictureBox1.Image = CurrentFrame.Bitmap;        // convert Mat to Bitmap
                    trackBar1.Value = CurrentFrameNo;
                    CurrentFrameNo += 1;
                    await Task.Delay(1000 / FPS);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (CurrentFrameNo == TotalFrames - 1)
            {
                CurrentFrameNo = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IsPlaying = false;
            Close();
            FirstPageView.self.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IsPlaying = false;
            FirstPageView.CloseMainForm();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(IsPlaying == true)
            {
                IsPlaying = false;
            } else
            {
                IsPlaying = true;
                PlayVideo();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (videocapture != null)
            {
                CurrentFrameNo = trackBar1.Value;
            }
        }
    }
}
