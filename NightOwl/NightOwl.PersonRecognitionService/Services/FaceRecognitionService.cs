using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using NightOwl.PersonRecognitionService.Models;
using NightOwl.PersonRecognitionWebService.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace NightOwl.PersonRecognitionService.Services
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly string _recognizerFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["RecognizerTrainFile"]);
        private readonly string _recognizerFacesFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["RecognizerNamesFile"]);

        private readonly int _recognizerNumOfComponents;
        private readonly int _recognizerThreshold;

        private string[] _FacesNamesArray { get; set; }

        private EigenFaceRecognizer _eigen;

        // to do: should log exceptions 
        public FaceRecognitionService()
        {
            try
            {
                _eigen = new EigenFaceRecognizer();
                _FacesNamesArray = File.ReadAllLines(_recognizerFacesFileName);
                _eigen.Read(_recognizerFileName);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FaceRecognitionService(IEnumerable<Face> Data, int recognizerNumOfComponents, int recognizerThreshold)
        {
            try
            {
                _recognizerNumOfComponents = recognizerNumOfComponents;
                _recognizerThreshold = recognizerThreshold;
                _eigen = new EigenFaceRecognizer(_recognizerNumOfComponents, _recognizerThreshold);

                var FacesPhotos = new List<Image<Gray, byte>>();
                var FacesNamesArray = new int[Data.Count()];
                _FacesNamesArray = new string[Data.Count()];

                int nameId = 0, j = 0;

                Data.ToList().ForEach(f => FacesPhotos.Add(f.Photo.ByteArrayToImage().ConvertToRecognition()));
                Data.ToList().ForEach(f =>
                {
                    if (!_FacesNamesArray.Contains(f.PersonName))
                    {
                        _FacesNamesArray[nameId] = f.PersonName;
                        FacesNamesArray[j] = nameId + 1;
                        nameId++;
                    }
                    else
                        FacesNamesArray[j] = FindIndexInArray(_FacesNamesArray, f.PersonName) + 1;
                    j++;
                });

                TrainRecognizer(FacesPhotos.ToArray(), FacesNamesArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool TrainRecognizer(Image<Gray, byte>[] images, int[] names)
        {
            try
            {
                if (_eigen == null)
                    throw new Exception("Recognizer must be initialized");
                  
                _eigen.Train(images, names);
                _eigen.Write(_recognizerFileName);
                File.WriteAllLines(_recognizerFacesFileName, _FacesNamesArray, Encoding.UTF8);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
      
        public string RecognizeFace(byte[] face)
        {
            try
            {
                if (_eigen == null)
                    throw new Exception("Recognizer must be initialized");

                Image<Gray, byte> convertedImage = face.ByteArrayToImage().ConvertToRecognition();
                FaceRecognizer.PredictionResult result = _eigen.Predict(convertedImage);

                if (result.Label > 0)
                    return _FacesNamesArray[result.Label - 1];
                else
                    return "";
            }
            catch(Exception)
            {
                throw;
            }
        } 

        public Image<Gray, byte> ConvertImageToGrayImage(Image image)
        {
            Bitmap masterImage = (Bitmap)image;

            Image<Gray, byte> normalizedMasterImage = new Image<Gray, byte>(masterImage);
            return normalizedMasterImage;
        }


        private int FindIndexInArray(string[] array, string find)
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i].Equals(find, StringComparison.OrdinalIgnoreCase))
                    return i;
            return -1;
        }
    }
}