﻿using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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

                var FacesPhotos = new List<Image<Bgr, byte>>();
                var FacesNamesArray = new int[Data.Count()];
                _FacesNamesArray = new string[Data.Count()];

                int nameId = 0, j = 0;

                Data.ToList().ForEach(f => FacesPhotos.Add(f.Photo.ByteArrayToImage()));

                IFaceDetectionService faceDetectionService = new FaceDetectionService();

                List<Image<Gray, byte>> FacesOnly = new List<Image<Gray, byte>>();

                foreach (var face in FacesPhotos)
                {
                    Rectangle[] faceRectangle = faceDetectionService.DetectFacesAsRect(face);

                    if (faceRectangle.Count() > 1)
                        throw new Exception("There is more than 1 face in the photo");
                    if(faceRectangle.Count() == 0)
                        throw new Exception("There is no faces in the photo");

                    var faceOnly = face.Copy(faceRectangle[0]).ConvertToRecognition();
                    FacesOnly.Add(faceOnly);
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

                TrainRecognizer(FacesOnly.ToArray(), FacesNamesArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> TrainRecognizer(Image<Gray, byte>[] images, int[] names)
        {
            try
            {
                if (_eigen == null)
                    throw new Exception(ConfigurationManager.AppSettings["RecognizerError"]);

                int i = 0;

                CloudStorageAccount _cloudStorageAccount = CloudStorageAccount.Parse(Connections.CloudBlobStorageConnection);
                CloudBlobClient _blobClient = _cloudStorageAccount.CreateCloudBlobClient();

                string containerId = Guid.NewGuid().ToString();

                CloudBlobContainer cloudBlobContainer = _blobClient.GetContainerReference(containerId);
                await cloudBlobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);

                // Set the permissions so the blobs are public. 
                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await cloudBlobContainer.SetPermissionsAsync(permissions);


                foreach (Image<Gray, byte> image in images)
                {

                    // Get the reference to the block blob from the container
                    CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(i + ".bmp");

                    // Upload the file
                    byte[] byteArray = image.ToBitmap().ImageToByteArray();

                    await blockBlob.UploadFromByteArrayAsync(byteArray , 0, byteArray.Length);
                    i++;
                }
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