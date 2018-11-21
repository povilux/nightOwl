using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace PersonRecognitionWebService.Extensions
{
    public static class ImageExtension
    {
        public static byte[] ImageToByteArray(this Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image<Gray, byte> ByteArrayToImage(this byte[] byteArrayIn)
        {

            try
            {
                Bitmap bitmap;

                using (MemoryStream ms = new MemoryStream(byteArrayIn))
                    bitmap = new Bitmap(Image.FromStream(ms));

                return new Image<Gray, byte>(bitmap);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}