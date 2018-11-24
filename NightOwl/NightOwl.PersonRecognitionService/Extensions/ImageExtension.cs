using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;

namespace NightOwl.PersonRecognitionWebService.Extensions
{
    public static class ImageExtension
    {
        public static byte[] ImageToByteArray(this Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image<Gray, byte> ConvertToRecognition(this Image<Bgr, byte> image)
        {
            return image
                        .Convert<Gray, byte>()
                        .Resize(
                               int.Parse(ConfigurationManager.AppSettings["FacePicWidth"]),
                               int.Parse(ConfigurationManager.AppSettings["FacePicHeight"]),
                               Emgu.CV.CvEnum.Inter.Cubic
                        )
                        .Clone();
        }
        public static Image<Bgr, byte> ByteArrayToImage(this byte[] byteArrayIn)
        {
            try
            {
                Bitmap bitmap;

                using (MemoryStream ms = new MemoryStream(byteArrayIn))
                    bitmap = new Bitmap(Image.FromStream(ms));

                return new Image<Bgr, byte>(bitmap);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}