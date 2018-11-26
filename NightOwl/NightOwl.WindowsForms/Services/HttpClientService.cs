using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using NightOwl.WindowsForms.Exceptions;
using System.Text;

namespace NightOwl.WindowsForms.Services
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

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await HttpClient.GetAsync(url);
            var contents = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new BadHttpRequestException(contents);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(contents);
            }
        }

        public async Task<V> PostAsync<V, T>(string url, T postData)
        {
            var postContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(url, postContent);

            if (!response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                throw new BadHttpRequestException(contents.Length > 0 ? contents.Substring(1, contents.Length - 2) : response.ReasonPhrase);
            }
            else
            {
                var contents = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<V>(contents);
            }
        }

        public async Task<T> PutAsync<T>(string url, T putData)
        {
            var putContent = new StringContent(JsonConvert.SerializeObject(putData), Encoding.UTF8, "application/json");

            var response = await HttpClient.PutAsync(url, putContent);
            var contents = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new BadHttpRequestException(contents);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(contents);
            }
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            var response = await HttpClient.DeleteAsync(url);
            var contents = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new BadHttpRequestException(contents);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(contents);
            }
        }
    }
}
