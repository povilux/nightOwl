using System;
using System.Configuration;
using System.Drawing;
using System.IO;

namespace NightOwl.WebService.Extensions
{
    public static class ImageExtension
    {
        public static byte[] ImageToByteArray(this Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(this byte[] byteArrayIn)
        {
            try
            {
                Bitmap bitmap;

                using (MemoryStream ms = new MemoryStream(byteArrayIn))
                    bitmap = new Bitmap(Image.FromStream(ms));

                return bitmap;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}