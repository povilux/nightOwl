using System;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using nightOwl.Components;
using System.Collections.Generic;
using nightOwl.Properties;
using nightOwl.Data;
using System.Configuration;

namespace nightOwl.BusinessLogic
{
    public sealed class PersonRecognizer
    {
        private Image<Bgr, Byte> CurrentFrame;
        private Capture Grabber;
        private EventHandler GrabberEvent;
        private HaarCascade FaceHaarCascade;
        private MCvFont Font;
        private Image<Gray, byte> Result;
        private List<Face> Faces = new List<Face>();

        private static readonly string ImageDataPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
                                                       Settings.Default.DataFolderPath + Settings.Default.ImagesFolderPath;

        private static readonly Lazy<PersonRecognizer> recognizer =
            new Lazy<PersonRecognizer>(() => new PersonRecognizer());

        public static PersonRecognizer Instance { get { return recognizer.Value; } }
        private bool IsCaptureOpened = false;

        private PersonRecognizer()
        {
            IsCaptureOpened = false;

            FaceHaarCascade = new HaarCascade(ImageDataPath + Settings.Default.FaceInformationFilePath);
            Font = new MCvFont(
                FONT.CV_FONT_HERSHEY_DUPLEX,
               double.Parse(ConfigurationManager.AppSettings["HaarCascadeHScale"]),
               double.Parse(ConfigurationManager.AppSettings["HaarCascadeVScale"])
           );
        }

        public void LoadTrainedFaces()
        {
            string name = "", picturePath = "";
            int picNumber = 1;

            foreach (Person person in DataManagement.Instance.GetPersonsCatalog())
            {
                name = person.Name;
                name = name.Replace(" ", "_");

                picNumber = 1;
                picturePath = ImageDataPath + name + "/";

                while (File.Exists(picturePath + picNumber + ConfigurationManager.AppSettings["PictureFormat"]))
                {
                    Faces.Add(new Face
                    {
                        Name = name,
                        FileName = picturePath + picNumber + ConfigurationManager.AppSettings["PictureFormat"],
                        Image = new Image<Gray, byte>(picturePath + picNumber + ConfigurationManager.AppSettings["PictureFormat"])
                    });
                    picNumber++;
                }
            }
        }

        Timer timer;

        public void StartCapture(Action<Bitmap> onFrameUpdate, bool fromVideo, string CurrentVideoFile = "")
        {
            CloseCapture();

            if (fromVideo)
            {
               Grabber = new Capture(CurrentVideoFile);
            }
            else
            {
                Grabber = new Capture();
            }
            IsCaptureOpened = true;

            GrabberEvent = new EventHandler((sender, e) =>
            {
                var frame = FrameGrabber(sender, e);
                onFrameUpdate.Invoke(frame);
            });
         

           Application.Idle += GrabberEvent;
        }

        public bool CloseCapture()
        {
            if (IsCaptureOpened)
            {
                try
                {
                    Grabber.Dispose();
                    Application.Idle -= GrabberEvent;
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            IsCaptureOpened = false;
            return true;
        }


     
        public Bitmap GetFaceFromImage(Image<Bgr, byte> image)
        {
            var grayFrame = image.Convert<Gray, byte>();

            // Detect faces in that frame
            var facesDetected = FaceHaarCascade.Detect(grayFrame,
                scaleFactor: double.Parse(ConfigurationManager.AppSettings["HaarCascadeScaleFactor"]),
                minNeighbors: int.Parse(ConfigurationManager.AppSettings["HaarCascadeMinNeighbors"]),
                flag: HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                minSize: new Size(
                            int.Parse(ConfigurationManager.AppSettings["HaarCascadeMinSize"]),
                            int.Parse(ConfigurationManager.AppSettings["HaarCascadeMinSize"])
                            )
            );

            Image<Gray, byte> DetectedFace = null;

            foreach (var face in facesDetected)
            {
                DetectedFace = grayFrame.Copy(face.rect);
                break;
            }

            if (facesDetected.Length != 1)
                return null;
            else
            {
                return DetectedFace.Resize(int.Parse(ConfigurationManager.AppSettings["FacePicWidth"]), 
                                        int.Parse(ConfigurationManager.AppSettings["FacePicHeight"]), INTER.CV_INTER_CUBIC).ToBitmap();
            }
        /*    DetectedFace = Result.Resize(int.Parse(ConfigurationManager.AppSettings["FacePicWidth"]),
                                            int.Parse(ConfigurationManager.AppSettings["FacePicHeight"]),
                            INTER.CV_INTER_CUBIC);

            return DetectedFace.ToBitmap();

            Console.WriteLine(facesDetected.Length);
            if (facesDetected.Length != 1)
            {
                return null;
            }
            else
            {
                Image<Gray, byte> faceImage = grayFrame.Copy(facesDetected[0].rect);
                return faceImage;
            }*/
        }

        /// Return an array of all faces in current frame
        private MCvAvgComp[] GetFacesFromCurrentFrame(Image<Gray, Byte> frame)
        {
            var facesDetected = FaceHaarCascade.Detect(frame,
                scaleFactor: double.Parse(ConfigurationManager.AppSettings["HaarCascadeScaleFactor"]),
                minNeighbors: int.Parse(ConfigurationManager.AppSettings["HaarCascadeMinNeighbors"]),
                flag: HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                minSize: new Size(
                            int.Parse(ConfigurationManager.AppSettings["HaarCascadeMinSize"]),
                            int.Parse(ConfigurationManager.AppSettings["HaarCascadeMinSize"])
                            )
            );

            return facesDetected;
        }

        private Bitmap RecognizePersons(Image<Gray, Byte> gray, Image<Bgr, Byte> CurrentFrame)
        {
            int frameThickness = 2;

            foreach (var face in GetFacesFromCurrentFrame(gray))
            {
                Result = CurrentFrame
                                   .Copy(face.rect)
                                   .Convert<Gray, byte>()
                                   .Resize(
                                            int.Parse(ConfigurationManager.AppSettings["FacePicWidth"]),
                                            int.Parse(ConfigurationManager.AppSettings["FacePicHeight"]),
                                            INTER.CV_INTER_CUBIC
                                          );

                if (Faces.Any())
                {
                    var termCriteria = new MCvTermCriteria(
                        Faces.Count,
                        double.Parse(ConfigurationManager.AppSettings["RecognizerInfinitive"]));

                    var recognizer = new EigenObjectRecognizer(
                       Faces.Select(f => f.Image).ToArray(),
                       Faces.Select(f => f.Name).ToArray(),
                       int.Parse(ConfigurationManager.AppSettings["RecognizerThreshold"]),
                       ref termCriteria);

                    // actual recognition
                    string name = recognizer.Recognize(Result);

                    if (name != string.Empty)
                    {
                            CurrentFrame.Draw(face.rect, new Bgr(Color.Green), frameThickness);

                             //Draw the label for each recognized face
                             var textSize = Font.GetTextSize(name, 0);
                             var x = face.rect.Left + (face.rect.Width - textSize.Width) / 2;
                             var y = face.rect.Bottom + textSize.Height;
                             CurrentFrame.Draw(name, ref Font, new Point(x, y + 5), new Bgr(Color.White));
                    }
                    else
                    {
                        CurrentFrame.Draw(face.rect, new Bgr(Color.Red), frameThickness);
                    }
                }
                else
                {
                   CurrentFrame.Draw(face.rect, new Bgr(Color.Red), frameThickness);
                }
            }
            return CurrentFrame.ToBitmap();
        }

        public Bitmap FrameGrabber(object sender, EventArgs e)
        {
            try
            {
                //Get the current frame form capture device
                CurrentFrame = Grabber.QueryFrame().Resize(
                       int.Parse(ConfigurationManager.AppSettings["FrameWidth"]),
                       int.Parse(ConfigurationManager.AppSettings["FrameHeight"]),
                       INTER.CV_INTER_CUBIC);

                //Convert it to Grayscale
                var gray = CurrentFrame.Convert<Gray, Byte>();

                return RecognizePersons(gray, CurrentFrame);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString());
                CloseCapture();
                return null;
            }
        }
    }
}