using System.Threading.Tasks;

namespace NightOwl.WindowsForms.Services
{
    public interface IHttpClientService
    {
        Task<T> DeleteAsync<T>(string url);
        Task<T> GetAsync<T>(string url);
        Task<V> PostAsync<V, T>(string url, T postData);
        Task<T> PutAsync<T>(string url, T putData);
    }
}