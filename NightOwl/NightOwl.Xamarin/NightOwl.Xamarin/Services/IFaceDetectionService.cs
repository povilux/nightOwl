using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace NightOwl.Xamarin.Services
{
    public interface IFaceDetectionService
    {
        Task<int> DetectFacesAsync(byte[] photoByteArray);
    }
}