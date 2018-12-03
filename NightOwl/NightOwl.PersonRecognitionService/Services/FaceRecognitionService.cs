using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NightOwl.PersonRecognitionService.DAL;
using NightOwl.PersonRecognitionService.Models;
using NightOwl.PersonRecognitionWebService.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightOwl.PersonRecognitionService.Services
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly string _recognizerFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["RecognizerTrainFile"]);
        private readonly string _recognizerFacesFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["RecognizerNamesFile"]);

        private readonly int _recognizerNumOfComponents;
        private readonly int _recognizerThreshold;

        private IFaceDetectionService _faceDetectionService;
        private DatabaseContext _context;
        private EigenFaceRecognizer _eigen;

        private string[] _FacesNamesArray { get; set; }

        public FaceRecognitionService(DatabaseContext context)
        {
            try
            {
                _eigen = new EigenFaceRecognizer();
                _FacesNamesArray = File.ReadAllLines(_recognizerFacesFileName);
                _eigen.Read(_recognizerFileName);
                _faceDetectionService = new FaceDetectionService();
                _context = context;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FaceRecognitionService(DatabaseContext context, int recognizerNumOfComponents, int recognizerThreshold)
        {
            try
            {
                _recognizerNumOfComponents = recognizerNumOfComponents;
                _recognizerThreshold = recognizerThreshold;
                _eigen = new EigenFaceRecognizer(_recognizerNumOfComponents, _recognizerThreshold); 
                _faceDetectionService = new FaceDetectionService();
                _context = context;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool TrainRecognizer(IEnumerable<Face> Data)
        {
            try
            {
                if (_eigen == null)
                    throw new Exception(ConfigurationManager.AppSettings["RecognizerError"]);

                List<Image<Gray, byte>> FacesOnly = new List<Image<Gray, byte>>();
                int nameId = 0, j = 0;
                var FacesPhotos = new List<Image<Bgr, byte>>();
                var FacesNamesArray = new int[Data.Count()];
                _FacesNamesArray = new string[Data.Count()];

                Data.ToList().ForEach(f => FacesPhotos.Add(f.Photo.ByteArrayToImage()));
                // to do: remove face detection because we are giving a face photo already
                foreach (var face in FacesPhotos)
                {
                    var facePhoto = _faceDetectionService.DetectFaceAsGrayImage(face);

                    if (facePhoto != null)
                        FacesOnly.Add(facePhoto);
                }

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

                _eigen.Train(FacesOnly.ToArray(), FacesNamesArray);
                _eigen.Write(_recognizerFileName);
                File.WriteAllLines(_recognizerFacesFileName, _FacesNamesArray, Encoding.UTF8);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
      
        public string RecognizeFace(byte[] photoByteArray)
        {
            try
            {
                if (_eigen == null)
                    throw new Exception(ConfigurationManager.AppSettings["RecognizerError"]);

                Image<Bgr, byte> photo = photoByteArray.ByteArrayToImage();

                IFaceDetectionService faceDetectionService = new FaceDetectionService();
                Rectangle[] faces = faceDetectionService.DetectFacesAsRect(photo);

                ICollection<string> recognizedNames = new List<string>();

                foreach (Rectangle faceRectangle in faces)
                {
                    var face = photo.Copy(faceRectangle).ConvertToRecognition();                  
                    FaceRecognizer.PredictionResult result = _eigen.Predict(face);

                    if (result.Label > 0)
                        recognizedNames.Add(_FacesNamesArray[result.Label - 1]);
                }
                return string.Join(Environment.NewLine, recognizedNames);
            }
            catch (Exception)
            {
                throw;
            }
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