using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using nightOwl.Exceptions;
using System.Text;
using System.Windows.Forms;

namespace nightOwl.Services
{
    public class HttpClientService : IHttpClientService
    {
        private static HttpClient HttpClient { get; set; }

        private static readonly Lazy<HttpClientService> http =
               new Lazy<HttpClientService>(() => new HttpClientService());

        private HttpClientService()
        {
            if(HttpClient == null)
                HttpClient = new HttpClient();
        }

        public static HttpClientService Instance {
            get {
                return http.Value;
            }
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await HttpClient.GetAsync(url);
            var contents = await response.Content.ReadAsStringAsync();

            if(!response.IsSuccessStatusCode)
            {
                throw new BadHttpRequestException(contents);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(contents);
            }
        }   
        
        public async Task<T> PostAsync<T>(string url, T postData)
        {
            var postContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

            var response = await HttpClient.PostAsync(url, postContent);
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
