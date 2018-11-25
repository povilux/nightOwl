using NightOwl.Xamarin.Components;
using NightOwl.Xamarin.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NightOwl.Xamarin.Services
{
    public class PersonsService
    {
        private IHttpClientService httpClient = HttpClientService.Instance;

        public async Task<APIMessage<Person>> GetPersonsList()
        {    
            try
            {
                var response = await httpClient.GetAsync<Person>(APIEndPoints.GetPersonEndPoint);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }
            return null;
        }

        public async Task<APIMessage<Person>> AddNewPersonAsync(Person newPerson)
        {
            if (newPerson != null)
            {
                try
                {
                    var response = await httpClient.PostAsync<Person, Person>(APIEndPoints.AddNewPersonEndPoint, newPerson);
                    return response;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex);
                }
            }
            return null;
        }
    }
}
