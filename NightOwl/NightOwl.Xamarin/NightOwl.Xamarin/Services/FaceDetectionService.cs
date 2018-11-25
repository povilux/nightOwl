using NightOwl.Xamarin.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightOwl.Xamarin.Services
{
    public class FaceDetectionService : IFaceDetectionService
    {
        private IHttpClientService httpClient = HttpClientService.Instance;

        public async Task<byte[][]> DetectFacesAsync(byte[] photo)
        {
            var detection = await DetectFacesAPIAsync(photo);

            if (!detection.Success)
            {
                // to do: log detection.Error
                return null;
            }
            else
                return detection.Message;
        }

        public async Task<APIMessage<byte[][]>> DetectFacesAPIAsync(byte[] photo)
        {
            if (photo != null)
            {
                try
                {
                    var response = await httpClient.PostAsync<byte[][], byte[]>(APIEndPoints.DetectFacesEndPoint, photo);
                    return response;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex);
                }
            }
            return null;
        }
    }
}
