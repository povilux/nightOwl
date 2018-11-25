using NightOwl.Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.FaceAnalysis;
using NightOwl.Xamarin.UWP;
using System.IO;
using System.Drawing;
using Xamarin.Forms;

[assembly: Dependency(typeof(NightOwl.Xamarin.UWP.FaceDetectionService))]
namespace NightOwl.Xamarin.UWP
{
    public class FaceDetectionService : IFaceDetectionService
    {
        private const BitmapPixelFormat faceDetectionPixelFormat = BitmapPixelFormat.Gray8;

        public async Task<int> DetectFacesAsync(byte[] photoByteArray)
        {
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(photoByteArray.ToRandomAccessMemory());

            BitmapTransform transform = new BitmapTransform();
            const float sourceImageHeightLimit = 1280;

            if (decoder.PixelHeight > sourceImageHeightLimit)
            {
                float scalingFactor = (float)sourceImageHeightLimit / (float)decoder.PixelHeight;
                transform.ScaledWidth = (uint)Math.Floor(decoder.PixelWidth * scalingFactor);
                transform.ScaledHeight = (uint)Math.Floor(decoder.PixelHeight * scalingFactor);
            }

            SoftwareBitmap sourceBitmap = await decoder.GetSoftwareBitmapAsync(decoder.BitmapPixelFormat, BitmapAlphaMode.Premultiplied, transform, ExifOrientationMode.IgnoreExifOrientation, ColorManagementMode.DoNotColorManage);
            SoftwareBitmap convertedBitmap = sourceBitmap;

            if (sourceBitmap.BitmapPixelFormat != faceDetectionPixelFormat)
                convertedBitmap = SoftwareBitmap.Convert(sourceBitmap, faceDetectionPixelFormat);

            FaceDetector detector = await FaceDetector.CreateAsync();

            IList<DetectedFace> faces = null;
            faces = await detector.DetectFacesAsync(convertedBitmap);

           /* ICollection<System.Drawing.Rectangle> rectangles = new List<System.Drawing.Rectangle>();

            foreach(DetectedFace face in faces)
                rectangles.Add(new System.Drawing.Rectangle(Convert.ToInt32(face.FaceBox.X), Convert.ToInt32(face.FaceBox.Y), Convert.ToInt32(face.FaceBox.Width), Convert.ToInt32(face.FaceBox.Height)));
                */
            sourceBitmap.Dispose();
            convertedBitmap.Dispose();

            return faces.Count;
        }
    }
}
