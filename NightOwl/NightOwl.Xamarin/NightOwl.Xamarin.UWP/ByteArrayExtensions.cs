using System.IO;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace NightOwl.Xamarin.UWP
{
    public static class ByteArrayExtensions
    {
      
        

        public static IRandomAccessStream ToRandomAccessMemory(this byte[] arr)
        {
            MemoryStream stream = new MemoryStream(arr);
            return stream.AsRandomAccessStream();
        }
    }

}
