using System.Collections.Generic;
using System.Threading.Tasks;
using NightOwl.PersonRecognitionService.Components;


namespace NightOwl.PersonRecognitionService.Services
{
    public interface IFaceRecognitionService
    {
        Task<IEnumerable<Person>> RecognizeFace(byte[] photo);
        Task<bool> TrainRecognizer();
    }
}