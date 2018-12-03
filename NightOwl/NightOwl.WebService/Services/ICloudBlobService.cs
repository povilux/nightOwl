using System.Collections.Generic;
using System.Threading.Tasks;

namespace NightOwl.WebService.Services
{
    public interface ICloudBlobService
    {
        Task UploadFaceBlobAsync(string containerName, IEnumerable<byte[]> faces);
        Task DeleteFaceBlobAsync(string containerName);
    }
}