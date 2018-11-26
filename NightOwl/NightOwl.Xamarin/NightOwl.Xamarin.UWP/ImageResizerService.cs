using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms;
using NightOwl.Xamarin.Services;
using Windows.Storage.Streams;

[assembly: Dependency(typeof(NightOwl.Xamarin.UWP.ImageResizerService))]
namespace NightOwl.Xamarin.UWP
{
    class ImageResizerService : IImageResizerService
    {
        public ImageResizerService() { }

        public async Task<byte[]> ResizeImageAsync(byte[] imageData, float width, float height)
        {
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(imageData.ToRandomAccessMemory());

            BitmapTransform transform = new BitmapTransform();
            const float sourceImageHeightLimit = 400;

            if (decoder.PixelHeight > sourceImageHeightLimit)
            {
                float scalingFactor = (float)sourceImageHeightLimit / (float)decoder.PixelHeight;
                transform.ScaledWidth =  (uint)Math.Floor(decoder.PixelWidth * scalingFactor);
                transform.ScaledHeight = (uint) Math.Floor(decoder.PixelHeight * scalingFactor);
            }

            SoftwareBitmap sourceBitmap = await decoder.GetSoftwareBitmapAsync(decoder.BitmapPixelFormat, BitmapAlphaMode.Premultiplied, transform, ExifOrientationMode.IgnoreExifOrientation, ColorManagementMode.DoNotColorManage);
            SoftwareBitmap convertedBitmap = sourceBitmap;

            // First: Use an encoder to copy from SoftwareBitmap to an in-mem stream (FlushAsync)
            // Next:  Use ReadAsync on the in-mem stream to get byte[] array

            byte[] imageBytes = null;

            using (var ms = new InMemoryRandomAccessStream())
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, ms);
                encoder.SetSoftwareBitmap(convertedBitmap);

                try
                {
                    await encoder.FlushAsync();
                }
                catch (Exception ex) {
                    ErrorLogger.Instance.LogException(ex);
                }

                imageBytes = new byte[ms.Size];
                await ms.ReadAsync(imageBytes.AsBuffer(), (uint)ms.Size, InputStreamOptions.None);
            }
            sourceBitmap.Dispose();
            convertedBitmap.Dispose();
          
            return imageBytes;
        }
    }
}
