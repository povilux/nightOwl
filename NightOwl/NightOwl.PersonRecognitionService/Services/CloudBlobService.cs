using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NightOwl.PersonRecognitionService.Models;
using NightOwl.PersonRecognitionWebService.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NightOwl.PersonRecognitionService.Services
{
    public class CloudBlobService : ICloudBlobService
    {
        public CloudBlobService() { }

        public async Task UploadFaceBlobAsync(IEnumerable<Image<Gray, byte>> faces)
        {
            // Connecting to cloud blob storage.
            CloudStorageAccount _cloudStorageAccount = CloudStorageAccount.Parse(Connections.CloudBlobStorageConnection);
            CloudBlobClient _blobClient = _cloudStorageAccount.CreateCloudBlobClient();

            // Creating container or finding. TO DO: containerId = personId
            string containerId = Guid.NewGuid().ToString();

            CloudBlobContainer cloudBlobContainer = _blobClient.GetContainerReference(containerId);
            await cloudBlobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);

            // Set the permissions so the blobs are public. 
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await cloudBlobContainer.SetPermissionsAsync(permissions);

            int i = 0;

            // Uploading blobs 
            foreach (Image<Gray, byte> face in faces)
            {
                // Get the reference to the block blob from the container
                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(i + ".bmp");

                // Upload the file
                byte[] faceByteArray = face.ToBitmap().ImageToByteArray();
                await blockBlob.UploadFromByteArrayAsync(faceByteArray, 0, faceByteArray.Length);
                i++;
            }
        }

        public async void RemoveFaceBlobAsync(IEnumerable<Image<Gray, byte>> faces)
        {

        }

        public async void GetFaceBlobAsync()
        {

        }

    }
}