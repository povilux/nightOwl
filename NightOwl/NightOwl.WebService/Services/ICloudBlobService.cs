using NightOwl.WebService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NightOwl.WebService.Services
{
    public interface ICloudBlobService
    {
        Task<ICollection<Face>> UploadFaceBlobAsync(int personId, string containerName, IEnumerable<byte[]> faces);
        Task DeleteFaceBlobAsync(string containerName);
    }
}