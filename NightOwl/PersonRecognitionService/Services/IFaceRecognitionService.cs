using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using PersonRecognitionService.Models;

namespace PersonRecognitionService.Services
{
    public interface IFaceRecognitionService
    {
        Image ByteArrayToImage(byte[] byteArrayIn);
        Image<Gray, byte> ConvertImageToGrayImage(Image image);
        byte[] ImageToByteArray(Image imageIn);
        string RecognizeFace(Face face);
        bool Train();
    }
}