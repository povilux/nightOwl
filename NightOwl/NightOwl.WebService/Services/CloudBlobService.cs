using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NightOwl.WebService.Models;
using NightOwl.WebService.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;

namespace NightOwl.WebService.Services
{
    public class CloudBlobService : ICloudBlobService
    {
        public CloudBlobService() { }

        public async Task DeleteFaceBlobAsync(string containerName)
        {
            // Connecting to cloud blob storage.
            CloudStorageAccount _cloudStorageAccount = CloudStorageAccount.Parse(Connections.CloudBlobStorageConnection);
            CloudBlobClient _blobClient = _cloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = _blobClient.GetContainerReference(containerName + "-container");
            await cloudBlobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);

            // Set the permissions so the blobs are public. 
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await cloudBlobContainer.SetPermissionsAsync(permissions);

            await cloudBlobContainer.DeleteAsync();
        }

        public async Task UploadFaceBlobAsync(string containerName, IEnumerable<byte[]> faces)
        {
            // Connecting to cloud blob storage.
            CloudStorageAccount _cloudStorageAccount = CloudStorageAccount.Parse(Connections.CloudBlobStorageConnection);
                CloudBlobClient _blobClient = _cloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = _blobClient.GetContainerReference(containerName + "-container");
               await cloudBlobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);

                // Set the permissions so the blobs are public. 
                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await cloudBlobContainer.SetPermissionsAsync(permissions);

                int i = 0;
                
                // Uploading blobs 
                foreach (byte[] face in faces)
                {
                    // Get the reference to the block blob from the container
                    CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(i + ".bmp");

                    // Upload the file
                    await blockBlob.UploadFromByteArrayAsync(face, 0, face.Length);
                    i++;
                }
            

        }

    }
}