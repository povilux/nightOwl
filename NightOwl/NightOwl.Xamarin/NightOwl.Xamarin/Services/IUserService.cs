using System.Threading.Tasks;
using NightOwl.Xamarin.Components;

namespace NightOwl.Xamarin.Services
{
    public interface IUserService
    {
        Task<APIMessage<User>> GetUserByUsernameAsync(string username);
        Task<APIMessage<bool>> LoginAsync(string username, string password);
        Task<APIMessage<User>> RegisterAsync(User user);
    }
}