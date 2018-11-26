using NightOwl.Xamarin.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace NightOwl.Xamarin.Services
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private IHttpClientService httpClient = HttpClientService.Instance;

        public async Task<APIMessage<string>> RecognizeFacesAsync(byte[] photo)
        {
            if (photo != null)
            {
                try
                {
                    var response = await httpClient.PostAsync<string, byte[]>(APIEndPoints.RecognizeFaceEndPoint, photo);
                    return response;
                }
                catch (Exception ex)
                {
                    ErrorLogger.Instance.LogException(ex);
                }
            }
            return null;
        }

        public async Task<APIMessage<string>> TrainFacesAsync(Trainer trainingData)
        {
            if (trainingData != null)
            {
                try
                {
                    var response = await httpClient.PostAsync<string, Trainer>(APIEndPoints.TrainFaceEndPoint, trainingData);
                    return response;
                }
                catch (Exception ex)
                {
                    ErrorLogger.Instance.LogException(ex);
                }
            }
            return null;
        }
    }
}
