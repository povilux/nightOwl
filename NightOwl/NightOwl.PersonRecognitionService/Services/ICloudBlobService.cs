using System.Collections.Generic;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace NightOwl.PersonRecognitionService.Services
{
    public interface ICloudBlobService
    {
        void GetFaceBlobAsync();
        void RemoveFaceBlobAsync(IEnumerable<Image<Gray, byte>> faces);
        Task UploadFaceBlobAsync(IEnumerable<Image<Gray, byte>> faces);
    }
}