using Emgu.CV;
using Emgu.CV.Structure;
using NightOwl.PersonRecognitionWebService.Extensions;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;

namespace NightOwl.PersonRecognitionService.Services
{
    public class FaceDetectionService : IFaceDetectionService
    {
        private readonly CascadeClassifier _cascadeClassifier;
        private readonly string _faceDetectionFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["FaceDetectionFileName"]);

        public FaceDetectionService()
        {
            _cascadeClassifier = new CascadeClassifier(_faceDetectionFilePath);
        }

        /// Return an image with rectangles on faces
        public Image<Bgr, byte> DrawFaces(Image<Bgr, byte> input)
        {
            Rectangle[] faces = DetectFaces(input);

            foreach(Rectangle face in faces)
                input.Draw(face, new Bgr(Color.BurlyWood), 2); //the detected face(s) is highlighted here using a box that is drawn around it/them*
 
            return input;
        }

        /// Return an array of all faces in current frame
        public Rectangle[] DetectFaces(Image<Bgr, byte> input)
        {
            Rectangle[] faces = _cascadeClassifier.DetectMultiScale(input,
                double.Parse(ConfigurationManager.AppSettings["FaceDetectionScaleFactor"]),
                int.Parse(ConfigurationManager.AppSettings["FaceDetectionMinNeighboors"]),
                new Size(
                    int.Parse(ConfigurationManager.AppSettings["FaceDetectionSize"]),
                    int.Parse(ConfigurationManager.AppSettings["FaceDetectionSize"])
                )
            );
            return faces;
        }
    }
}