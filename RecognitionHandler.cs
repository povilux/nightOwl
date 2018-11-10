using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using System.Windows.Forms;
using System.Drawing;
using nightOwl.Views;
using nightOwl.Data;
using nightOwl.Properties;
using System.IO;

namespace nightOwl
{
    class RecognitionHandler
    {
        Emgu.CV.UI.ImageBox picBox;
        VideoCapture capture;
        public RecognitionHandler(Emgu.CV.UI.ImageBox imageBox, VideoCapture capture)   // webcam and pic
        {
            this.capture = capture;
            picBox = imageBox;
        }

        public RecognitionHandler()                                                     // video player
        {

        }

        public void ProcessFrameForWebcam(object sender, EventArgs e)
        {
            /*
            Image<Bgr, Byte> ImageFrame = capture.QueryFrame().ToImage<Bgr, Byte>();
            imgCamUser.Image = ImageFrame;
            */
            CascadeClassifier _cascadeClassifier;
            _cascadeClassifier = new CascadeClassifier(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
     Settings.Default.DataFolderPath + Settings.Default.ImagesFolderPath + Settings.Default.FaceInformationFilePath);
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

                            foreach (Person person in DataManagement.Instance.GetPersonsCatalog())
                                names.Add(person.Name);

                            string name = names.ElementAt(result - 1);

                            CvInvoke.PutText(imageFrame, name, new Point(face.Location.X + 10,
                                face.Location.Y - 10), Emgu.CV.CvEnum.FontFace.HersheyComplex, 1.0, new Bgr(0, 255, 0).MCvScalar);
                        }
                        imageFrame.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them

                    }
                }
                picBox.Image = imageFrame;
            }
        }

        public void ProcessFrameForPicture()
        {
            if(PictureForm.image == null)
            {
                return;
            }
            CascadeClassifier _cascadeClassifier;
            _cascadeClassifier = new CascadeClassifier(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
     Settings.Default.DataFolderPath + Settings.Default.ImagesFolderPath + Settings.Default.FaceInformationFilePath);
            using (var imageFrame = PictureForm.image)
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

                            foreach (Person person in DataManagement.Instance.GetPersonsCatalog())
                                names.Add(person.Name);

                            string name = names.ElementAt(result - 1);

                            CvInvoke.PutText(imageFrame, name, new Point(face.Location.X + 10,
                                face.Location.Y - 10), Emgu.CV.CvEnum.FontFace.HersheyComplex, 1.0, new Bgr(0, 255, 0).MCvScalar);
                        }
                        imageFrame.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them

                    }
                }
                picBox.Image = imageFrame;
            }
        }
    }
}
