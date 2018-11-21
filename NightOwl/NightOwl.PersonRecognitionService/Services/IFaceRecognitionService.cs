using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using PersonRecognitionService.Models;

namespace PersonRecognitionService.Services
{
    public interface IFaceRecognitionService
    {
        Image<Gray, byte> ConvertImageToGrayImage(Image image);
        string RecognizeFace(byte[] face);
        bool TrainRecognizer(Image<Gray, byte>[] images, int[] names);
    }
}