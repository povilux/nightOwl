using NightOwl.Xamarin.Components;
using System.Threading.Tasks;

namespace NightOwl.Xamarin.Services
{
    public interface IFaceRecognitionService
    {
        Task<APIMessage<string>> RecognizeFacesAsync(byte[] photo);
    }
}