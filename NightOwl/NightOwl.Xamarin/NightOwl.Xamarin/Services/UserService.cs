using NightOwl.Xamarin.Components;
using NightOwl.Xamarin.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NightOwl.Xamarin.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientService httpClient = HttpClientService.Instance;

        public async Task<APIMessage<User>> RegisterAsync(User user)
        {
            if (user != null)
            {
                try
                {
                    var response = await httpClient.PostAsync<User, User>(APIEndPoints.RegisterUserEndPoint, user);
                    return response;
                }
  
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex);
                }
            }
            return null;
        }

        public async Task<APIMessage<bool>> LoginAsync(string username, string password)
        { 
            try
            {
                var info = new KeyValuePair<string, string>(username, password);
                var response = await httpClient.PostAsync<bool, KeyValuePair<string, string>>(APIEndPoints.LoginUserEndPoint,  info);

                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }
            return null;
        }

        public async Task<APIMessage<User>> GetUserByUsernameAsync(string username)
        {
            try
            {
                var response = await httpClient.GetAsync<User>(APIEndPoints.GetUserByUsernameEndPoint + username);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }
            return null;
        }
    }
    
}
