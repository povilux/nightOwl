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

        private readonly int _recognizerThreshold;

        private readonly IFaceDetectionService _faceDetectionService;
        private EigenFaceRecognizer _eigen;

        private int[] _PersonsIdArray;

        public FaceRecognitionService()
        {
            try
            {
                _eigen = new EigenFaceRecognizer();
                _eigen.Read(_recognizerFileName);
                _faceDetectionService = new FaceDetectionService();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FaceRecognitionService(int recognizerThreshold)
        {
            try
            {
                _recognizerThreshold = recognizerThreshold;
                _eigen = new EigenFaceRecognizer(0, _recognizerThreshold); 
                _faceDetectionService = new FaceDetectionService();
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

                var response = await HttpClient.GetAsync(new Uri("https://nightowlwebservice.azurewebsites.net/api/Persons/Get"));
                var contents = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                   throw new Exception("Error: " + contents);
                }
                else
                {
                    personsList = JsonConvert.DeserializeObject<IEnumerable<Person>>(contents);                 
                }

                int y = 0;
                IList<Image<Gray, byte>> PersonsPhotoList = new List<Image<Gray, byte>>();
                IList<int> PersonsIdList = new List<int>();

                foreach (var person in personsList)
                {
                    y = 0;

                    foreach(var face in person.FacePhotos)
                    {
                        CloudStorageAccount _cloudStorageAccount = CloudStorageAccount.Parse(Connections.CloudBlobStorageConnection);
                        CloudBlobClient blobClient = _cloudStorageAccount.CreateCloudBlobClient();

                        CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(person.Id + "-container");
                        CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(y + ".bmp");
                        MemoryStream memStream = new MemoryStream();
                        blockBlob.DownloadToStream(memStream);

                        Image<Gray, byte> faceImg = new Image<Gray, byte>(new Bitmap(Image.FromStream(memStream)));
                        PersonsIdList.Add(person.Id);
                        PersonsPhotoList.Add(faceImg);
                        y++;
                    }
                }

                _PersonsIdArray = new int[PersonsIdList.Count()];
                _PersonsIdArray = PersonsIdList.ToArray();

                _eigen.Train(PersonsPhotoList.ToArray(), _PersonsIdArray);
                _eigen.Write(_recognizerFileName);
                File.WriteAllText(_recognizerFacesFileName, string.Join(Environment.NewLine, _PersonsIdArray), Encoding.UTF8);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
      
        public async Task<IEnumerable<Person>> RecognizeFace(byte[] photoByteArray)
        {
            try
            {
                if (_eigen == null)
                    throw new Exception(ConfigurationManager.AppSettings["RecognizerError"]);

                Image<Bgr, byte> photo = photoByteArray.ByteArrayToImage();

                IFaceDetectionService faceDetectionService = new FaceDetectionService();
                Rectangle[] faces = faceDetectionService.DetectFacesAsRect(photo);

                ICollection<int> recognizedPersons = new List<int>();

                foreach (Rectangle faceRectangle in faces)
                {
                    var face = photo.Copy(faceRectangle).ConvertToRecognition();                  
                    FaceRecognizer.PredictionResult result = _eigen.Predict(face);

                    if (result.Label > 0)
                        recognizedPersons.Add(result.Label);
                }

                if (recognizedPersons.Count() <= 0)
                    return null; 

                HttpClient HttpClient = new HttpClient();

                IEnumerable<Person> personsList = new List<Person>();

                var response = await HttpClient.GetAsync(new Uri("https://nightowlwebservice.azurewebsites.net/api/Persons/Get"));
                var contents = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error: " + contents);
                }
                else
                {
                    personsList = JsonConvert.DeserializeObject<IEnumerable<Person>>(contents);
                }
                IEnumerable<string> a = personsList.Select(p => p.Name);

                return personsList.Where(p => recognizedPersons.All(p2 => p2 == p.Id)).ToList();
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