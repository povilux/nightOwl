using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace NightOwl.PersonRecognitionService.Services
{
    public interface IFaceDetectionService
    {
        byte[][] DetectFaces(Image<Bgr, byte> frame);
        Image<Bgr, byte> DrawFaces(Image<Bgr, byte> input);
        Image<Gray, byte> DetectFaceAsGrayImage(Image<Bgr, byte> input);
        Rectangle[] DetectFacesAsRect(Image<Bgr, byte> input);
    }
}