using NightOwl.Xamarin.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NightOwl.Xamarin.Services
{
    public class FaceDetectionService : IFaceDetectionService
    {
        private IHttpClientService httpClient = HttpClientService.Instance;

        public async Task<APIMessage<int>> DetectFacesAPIAsync(byte[] photo)
        {
            if (photo != null)
            {
                try
                {
                    var response = await httpClient.PostAsync<int, byte[]>(APIEndPoints.DetectFacesEndPoint, photo);
                    return response;
                }
                catch (Exception ex)
                {
                    ErrorLogger.Instance.LogException(ex);
                }
            }
            return null;
        }

        public async Task<int> DetectFacesAsync(byte[] photo)
        {
            var detection = await DetectFacesAPIAsync(photo);

            if (!detection.Success)
            {
                ErrorLogger.Instance.LogError(detection.Error);
                return 0;
            }
            else
                return detection.Message;
        }
    }
}
