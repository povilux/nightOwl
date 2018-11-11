﻿using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using nightOwl.Components;
using System.Collections.Generic;
using nightOwl.Properties;
using nightOwl.Data;
using System.Configuration;
using Emgu.CV.Face;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;
using System.Diagnostics;

namespace nightOwl.BusinessLogic
{
    public sealed class PersonRecognizer
    {
        private readonly CascadeClassifier _cascadeClassifier;

        private VideoCapture Grabber;
        private EventHandler GrabberEvent;
        private List<Face> Faces = new List<Face>();

        private static readonly string ImageDataPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
                                                       Settings.Default.DataFolderPath + Settings.Default.ImagesFolderPath;

        private static readonly Lazy<PersonRecognizer> recognizer =
            new Lazy<PersonRecognizer>(() => new PersonRecognizer());

        public static PersonRecognizer Instance { get { return recognizer.Value; } }
        private bool IsCaptureOpened = false;

        private static readonly string RecognizerDataPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
           Settings.Default.DataFolderPath + Settings.Default.RecognizerFilePath;

        private PersonRecognizer()
        {
            LoadTrainedFaces();

            IsCaptureOpened = false;

            _cascadeClassifier = new CascadeClassifier(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
     Settings.Default.DataFolderPath + Settings.Default.ImagesFolderPath + Settings.Default.FaceInformationFilePath);

        }


        public static EigenFaceRecognizer NewEigen()
        {
            EigenFaceRecognizer eigenRec = new EigenFaceRecognizer(80, 8000);
            eigenRec.Write(RecognizerDataPath);
            return eigenRec;
        }

        public static EigenFaceRecognizer OldEigen()
        {
            EigenFaceRecognizer eigenRec = new EigenFaceRecognizer(80, 8000);

            try
            {
                eigenRec.Read(RecognizerDataPath);
            }
            catch
            {
                eigenRec = NewEigen();
            }
            return eigenRec;
        }

        public bool LoadTrainedFaces()
        {
            EigenFaceRecognizer eigen = NewEigen();

            string directory = "", picturePath = "";
            int picNumber = 1, personLabelId = 0;

            foreach (Person person in DataManagement.Instance.GetPersonsCatalog())
            {
                directory = person.Name;
                directory = directory.Replace(" ", "_");

                picNumber = 1;
                picturePath = ImageDataPath + directory + "/";
                personLabelId++;

                while (File.Exists(picturePath + picNumber + ConfigurationManager.AppSettings["PictureFormat"]))
                {
                    Faces.Add(new Face
                    {
                        PersonLabelId = personLabelId,
                        Name = directory,
                        FileName = picturePath + picNumber + ConfigurationManager.AppSettings["PictureFormat"],
                        Image = new Image<Gray, byte>(picturePath + picNumber + ConfigurationManager.AppSettings["PictureFormat"])
                    });
                    picNumber++;
                }
            }
            Image<Gray, byte>[] faceArray = Faces.Select(f => f.Image).ToArray();
            int[] labelArray = Faces.Select(f => f.PersonLabelId).ToArray();

            if (faceArray.Length != labelArray.Length || faceArray.Length < 1 || labelArray.Length < 1)
            {
                return false;
            }
            else
            {
                eigen.Train(faceArray, labelArray);
                SaveRecognizer(eigen);
                return true;
            }
        }

        public void SaveRecognizer(EigenFaceRecognizer rec)
        {
            rec.Write(RecognizerDataPath);
        }
        Stopwatch watch;
        System.Windows.Forms.Timer t;
        public void StartCapture(Action<Bitmap> onFrameUpdate, bool fromVideo, string CurrentVideoFile = "")
        {
            CloseCapture();

            if (fromVideo)
            {
                Grabber = new VideoCapture(CurrentVideoFile);
            }
            else
            {
                Grabber = new VideoCapture();
            }
            IsCaptureOpened = true;

            GrabberEvent = new EventHandler((sender, e) =>
            {
                var frame = FrameGrabber(sender, e);
                onFrameUpdate.Invoke(frame);
            });

            //   Grabber.ImageGrabbed += GrabberEvent;
            watch = new Stopwatch();
            watch.Start();

           t = new System.Windows.Forms.Timer();
            t.Interval = 30; // specify interval time as you want
            t.Tick += GrabberEvent;
            t.Start();

            //   Application.Idle += GrabberEvent;
        }


        public bool CloseCapture()
        {
            if (IsCaptureOpened)
            {
                try
                {
                    Grabber.Dispose();
                    //  Grabber.ImageGrabbed -= GrabberEvent;
                    watch.Stop();
                    t.Stop();
                 //   Application.Idle -= GrabberEvent;
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
            /*var grayFrame = image.Convert<Gray, byte>();

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
            }*/
            return null;
        }

        /// Return an array of all faces in current frame
        private Rectangle[] GetFacesFromCurrentFrame(Image<Gray, byte> frame)
        {
            var faces = _cascadeClassifier.DetectMultiScale(frame, 1.2, 10, new Size(20, 20)); //the actual face detection happens here
            return faces;
        }

        private Image<Bgr, byte> RecognizePersons(Image<Bgr, byte> frame)
        {
            if (Faces.Any())
            {
                Image<Gray, byte> gray = frame.Convert<Gray, byte>();

                foreach (var face in GetFacesFromCurrentFrame(gray))
                {
                    Image<Gray, byte> proccessedFrame = gray.Copy(face)
                             .Resize(
                                    int.Parse(ConfigurationManager.AppSettings["FacePicWidth"]),
                                    int.Parse(ConfigurationManager.AppSettings["FacePicHeight"]),
                                    Emgu.CV.CvEnum.Inter.Cubic
                              );

                    var result = Recognizer.RecognizeFace(proccessedFrame);

                    if (result > 0)
                    {
                        Face currentFace = Faces.Where(f => f.PersonLabelId == result).FirstOrDefault();

                        CvInvoke.PutText(frame, currentFace.Name, new Point(face.Location.X + 10,
                            face.Location.Y - 10), Emgu.CV.CvEnum.FontFace.HersheyComplex, 0.5, new Bgr(0, 255, 0).MCvScalar);

                        frame.Draw(face, new Bgr(Color.BurlyWood), 2); //the detected face(s) is highlighted here using a box that is drawn around it/them*
                    }
                }
            }
            return frame;//.ToBitmap();
        }
        public Bitmap FrameGrabber(object sender, EventArgs e)
        {
            try
            {

                    Image<Bgr, byte> frame = Grabber.QuerySmallFrame().ToImage<Bgr, byte>();
                    frame = frame.Resize(
                                int.Parse(ConfigurationManager.AppSettings["FrameWidth"]),
                                int.Parse(ConfigurationManager.AppSettings["FrameHeight"]),
                                Emgu.CV.CvEnum.Inter.Cubic
                             );

               
              
                    return RecognizePersons(frame).ToBitmap();
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
 