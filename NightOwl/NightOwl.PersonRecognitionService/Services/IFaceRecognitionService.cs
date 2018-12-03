using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using NightOwl.PersonRecognitionService.DAL;
using NightOwl.PersonRecognitionService.Models;

namespace NightOwl.PersonRecognitionService.Services
{
    public interface IFaceRecognitionService
    {
        string RecognizeFace(byte[] photo);
        bool TrainRecognizer(IEnumerable<Face> Data);
    }
}