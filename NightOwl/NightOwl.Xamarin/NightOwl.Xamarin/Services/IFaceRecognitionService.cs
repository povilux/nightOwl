using NightOwl.Xamarin.Components;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace NightOwl.Xamarin.Services
{
    public interface IFaceRecognitionService
    {
        Task<APIMessage<IEnumerable<Person>>> RecognizeFacesAsync(byte[] photo);
    }
}