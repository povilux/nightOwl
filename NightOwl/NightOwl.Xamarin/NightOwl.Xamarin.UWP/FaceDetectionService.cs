using NightOwl.Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FaceDetectionService))]
namespace NightOwl.Xamarin.UWP
{
    public class FaceDetectionService : IFaceDetectionService
    {
        public async Task<byte[][]> DetectFacesAsync(byte[] photo)
        {

            return null;
        }

    }
}
