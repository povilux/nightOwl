using NightOwl.Xamarin.Components;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace NightOwl.Xamarin.Services
{
    public interface IFaceDetectionService
    {
        Task<APIMessage<byte[]>> DetectFacesAsync(byte[] photo);
    }
}