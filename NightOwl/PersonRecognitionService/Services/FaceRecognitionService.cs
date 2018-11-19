using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using PersonRecognitionService.DAL;
using PersonRecognitionService.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PersonRecognitionService.Services
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private const string _recognizerFileName = "recognizer.yaml";
        private int _recognizerNumOfComponents;
        private int _recognizerThreshold;

        private DatabaseContext _databaseContext;
        private EigenFaceRecognizer _eigen;

        public FaceRecognitionService() { }

        public FaceRecognitionService(int recognizerNumOfComponents, int recognizerThreshold)
        {
            _recognizerNumOfComponents = recognizerNumOfComponents;
            _recognizerThreshold = recognizerThreshold;

            _databaseContext = new DatabaseContext(); 
        }

        public bool Train()
        {
            try
            {
                _eigen = new EigenFaceRecognizer(_recognizerNumOfComponents, _recognizerThreshold);
                ICollection<Image<Gray, byte>> imagesList = new List<Image<Gray, byte>>();

                foreach (byte[] faceByteArray in _databaseContext.Faces.ToList().Select(f => f.Photo))
                {
                    Image convertedImage = ByteArrayToImage(faceByteArray);
                    Image<Gray, byte> grayImage = ConvertImageToGrayImage(convertedImage);
                    imagesList.Add(grayImage);
                }

                Image<Gray, byte>[] faceArray = imagesList.ToArray();
                int[] labelArray = _databaseContext.Faces.ToList().Select(f => f.Id).ToArray();

                if (faceArray.Length != labelArray.Length || faceArray.Length < 1 || labelArray.Length < 1)
                {
                    throw new Exception("Not correct training data!");
                }
                else
                {
                    _eigen.Train(faceArray, labelArray);
                    _eigen.Write(_recognizerFileName);
                    return true;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public string RecognizeFace(Face face)
        {
            try
            {
                if (_eigen == null)
                    throw new Exception("Recognizer must be initialized.");

                Image convertedImage = ByteArrayToImage(face.Photo);
                Image<Gray, byte> grayImage = ConvertImageToGrayImage(convertedImage);

                var result = _eigen.Predict(grayImage);

                if (result.Label > 0)
                {
                    Face currentFace = _databaseContext.Faces.ToList().Where(f => f.Id == result.Label).FirstOrDefault();
                    return currentFace.PersonName;
                }
            }
            catch(Exception)
            {
                throw;
            }
            return null;
        }

        public byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public  Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public Image<Gray, byte> ConvertImageToGrayImage(Image image)
        {
            Bitmap masterImage = (Bitmap)image;

            Image<Gray, byte> normalizedMasterImage = new Image<Gray, byte>(masterImage);
            return normalizedMasterImage;
        }
    }
}