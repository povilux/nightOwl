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

            CloudBlobContainer cloudBlobContainer = _blobClient.GetContainerReference(containerName);

            await cloudBlobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);

            // Set the permissions so the blobs are public. 
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await cloudBlobContainer.SetPermissionsAsync(permissions);

            await cloudBlobContainer.DeleteAsync();
        }

        public async Task<ICollection<Face>> UploadFaceBlobAsync(int personID, string containerName, IEnumerable<byte[]> faces)
        {
            CloudStorageAccount _cloudStorageAccount = CloudStorageAccount.Parse(Connections.CloudBlobStorageConnection);
            CloudBlobClient _blobClient = _cloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = _blobClient.GetContainerReference(containerName);
            await cloudBlobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);

            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await cloudBlobContainer.SetPermissionsAsync(permissions);

            ICollection<Face> faceBlobsList = new List<Face>();
            int i = cloudBlobContainer.ListBlobs().Count();
            string blobName = "";

            foreach (byte[] face in faces)
            {
                blobName = i + ".bmp";

                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
                await blockBlob.UploadFromByteArrayAsync(face, 0, face.Length);

                faceBlobsList.Add(new Face()
                {
                    BlobURI = "https://nightowl.blob.core.windows.net/" + containerName + "/" + blobName,
                    OwnerId = personID
                });
                i++;
            }
            return faceBlobsList;
        }
    }
}