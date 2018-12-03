using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NightOwl.Xamarin.Services
{
    public interface IImageResizerService
    {
         Task<byte[]> ResizeImageAsync(byte[] imageData);
    }
}
