using System.Threading.Tasks;

namespace nightOwl.Services
{
    public interface IHttpClientService
    {
        Task<T> DeleteAsync<T>(string url);
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, T postData);
        Task<T> PutAsync<T>(string url, T putData);
    }
}