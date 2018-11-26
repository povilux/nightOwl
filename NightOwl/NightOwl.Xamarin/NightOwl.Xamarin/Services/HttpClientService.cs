using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using NightOwl.Xamarin.Components;

namespace NightOwl.Xamarin.Services
{
    public class HttpClientService : IHttpClientService
    {
        private static HttpClient HttpClient { get; set; }

        private static readonly Lazy<HttpClientService> http =
               new Lazy<HttpClientService>(() => new HttpClientService());

        private HttpClientService()
        {
            if (HttpClient == null)
                HttpClient = new HttpClient();
        }

        public static HttpClientService Instance
        {
            get
            {
                return http.Value;
            }
        }

        public async Task<APIMessage<T>> GetAsync<T>(string url)
        {
            var response = await HttpClient.GetAsync(url);
            var contents = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new APIMessage<T>()
                {
                    Success = false,
                    Error = contents
                };
            }
            else
            {
                return new APIMessage<T>()
                {
                    Success = true,
                    Message = JsonConvert.DeserializeObject<T>(contents)
                };
            }
        }

        public async Task<APIMessage<V>> PostAsync<V, T>(string url, T postData)
        {
            var postContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(url, postContent);

            if (!response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();

                return new APIMessage<V>()
                {
                    Success = false,
                    Error = contents
                };
            }
            else
            {
                var contents = await response.Content.ReadAsStringAsync();

                return new APIMessage<V>()
                {
                    Success = true,
                    Message = JsonConvert.DeserializeObject<V>(contents)
                };
            }
        }

        public async Task<APIMessage<T>> PutAsync<T>(string url, T putData)
        {
            var putContent = new StringContent(JsonConvert.SerializeObject(putData), Encoding.UTF8, "application/json");

            var response = await HttpClient.PutAsync(url, putContent);
            var contents = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new APIMessage<T>()
                {
                    Success = false,
                    Error = contents
                };
            }
            else
            {
                return new APIMessage<T>()
                {
                    Success = true,
                    Message = JsonConvert.DeserializeObject<T>(contents)
                };
            }
        }

        public async Task<APIMessage<T>> DeleteAsync<T>(string url)
        {
            var response = await HttpClient.DeleteAsync(url);
            var contents = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new APIMessage<T>()
                {
                    Success = false,
                    Error = contents
                };
            }
            else
            {
                return new APIMessage<T>()
                {
                    Success = true,
                    Message = JsonConvert.DeserializeObject<T>(contents)
                };
            }
        }
    }
}
