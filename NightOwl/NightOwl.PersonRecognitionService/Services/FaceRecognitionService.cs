using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using NightOwl.PersonRecognitionService.Components;
using NightOwl.PersonRecognitionService.DAL;
using NightOwl.PersonRecognitionService.Models;
using NightOwl.PersonRecognitionWebService.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
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

        private int[] _PersonsIdArray;

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

        public async Task<bool> TrainRecognizer()
        {
            try
            {
                if (_eigen == null)
                    throw new Exception(ConfigurationManager.AppSettings["RecognizerError"]);

                HttpClient HttpClient = new HttpClient();

                IEnumerable<Person> personsList = new List<Person>();

                var response = await HttpClient.GetAsync(new Uri("https://nightowlwebservice.azurewebsites.net/api/Get"));
                var contents = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                   throw new Exception("Error: " + contents);
                }
                else
                {
                    personsList = JsonConvert.DeserializeObject<IEnumerable<Person>>(contents);                 
                }

                int i = 0, y = 0;
                IList<Image<Gray, byte>> PersonsPhotoList = new List<Image<Gray, byte>> ();

                foreach (var person in personsList)
                {
                    y = 0;

                    foreach(var face in person.FacePhotos)
                    {
                        _PersonsIdArray[i] = person.Id;

                        CloudStorageAccount _cloudStorageAccount = CloudStorageAccount.Parse(Connections.CloudBlobStorageConnection);
                        CloudBlobClient blobClient = _cloudStorageAccount.CreateCloudBlobClient();

                        CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(person.Id + "-container");
                        CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(y + ".bmp");
                        MemoryStream memStream = new MemoryStream();
                        blockBlob.DownloadToStream(memStream);

                        Image<Gray, byte> faceImg = new Image<Gray, byte>(new Bitmap(Image.FromStream(memStream)));
                        PersonsPhotoList.Add(faceImg);
                        y++;
                    }
                }

                _eigen.Train(PersonsPhotoList.ToArray(), _PersonsIdArray);
                _eigen.Write(_recognizerFileName);
                File.WriteAllLines(_recognizerFacesFileName, _FacesNamesArray, Encoding.UTF8);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
      
        public IEnumerable<int> RecognizeFace(byte[] photoByteArray)
        {
            try
            {
                if (_eigen == null)
                    throw new Exception(ConfigurationManager.AppSettings["RecognizerError"]);

                Image<Bgr, byte> photo = photoByteArray.ByteArrayToImage();

                IFaceDetectionService faceDetectionService = new FaceDetectionService();
                Rectangle[] faces = faceDetectionService.DetectFacesAsRect(photo);

                ICollection<int> recognizedNames = new List<int>();

                foreach (Rectangle faceRectangle in faces)
                {
                    var face = photo.Copy(faceRectangle).ConvertToRecognition();                  
                    FaceRecognizer.PredictionResult result = _eigen.Predict(face);

                   /* if (result.Label > 0)
                        recognizedNames.Add(_FacesNamesArray[result.Label - 1]);*/
                }
                return recognizedNames;
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