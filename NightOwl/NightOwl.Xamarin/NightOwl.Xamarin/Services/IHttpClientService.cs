using NightOwl.Xamarin.Components;
using System.Threading.Tasks;

namespace NightOwl.Xamarin.Services
{
    public interface IHttpClientService
    {
        Task<APIMessage<T>> DeleteAsync<T>(string url);
        Task<APIMessage<T>> GetAsync<T>(string url);
        Task<APIMessage<V>> PostAsync<V, T>(string url, T postData);
        Task<APIMessage<T>> PutAsync<T>(string url, T putData);
    }
}