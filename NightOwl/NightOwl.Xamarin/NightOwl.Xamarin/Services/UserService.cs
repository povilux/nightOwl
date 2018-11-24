using NightOwl.Xamarin.Components;
using NightOwl.Xamarin.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NightOwl.Xamarin.Services
{
    public class UserService
    {
        private readonly IHttpClientService httpClient = HttpClientService.Instance;

        public async Task<User> RegisterAsync(User user)
        {
            if (user != null)
            {
                try
                {
                    var response = await httpClient.PostAsync<User, User>(APIEndPoints.RegisterUserEndPoint, user);
                    return response;
                }
                catch (BadHttpRequestException ex)
                {
                    Console.WriteLine("BadHttpRequestException: " + ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex);
                }
            }
            throw new Exception("User cannot be null.");
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new Exception("Need to fill information.");

            try
            {
                var info = new KeyValuePair<string, string>(username, password);
                var response = await httpClient.PostAsync<User, KeyValuePair<string, string>>(APIEndPoints.LoginUserEndPoint,  info);

                return response;
            }
            catch(BadHttpRequestException ex)
            {
                Console.WriteLine("BadHtpRequestException: " + ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }
            return null;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new Exception("Username must be filled");

            try
            {
                var response = await httpClient.GetAsync<User>(APIEndPoints.GetUserByUsernameEndPoint + username);
                return response;
            }
            catch(BadHttpRequestException ex)
            {
                Console.WriteLine("BadHtpRequestException: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }
            return null;
        }
    }
    
}
