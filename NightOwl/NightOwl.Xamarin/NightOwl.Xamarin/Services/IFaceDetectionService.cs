using System.Threading.Tasks;
using NightOwl.Xamarin.Components;

namespace NightOwl.Xamarin.Services
{
    public interface IFaceDetectionService
    {
        Task<byte[][]> DetectFacesAsync(byte[] photo);
    }
}