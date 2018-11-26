using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using NightOwl.PersonRecognitionService.Models;

namespace NightOwl.PersonRecognitionService.Services
{
    public interface IFaceRecognitionService
    {
        Image<Gray, byte> ConvertImageToGrayImage(Image image);
        string RecognizeFace(byte[] photoByteArray);
        bool TrainRecognizer(Image<Gray, byte>[] images, int[] names);
    }
}