using NightOwl.WindowsForms.Components;
using NightOwl.WindowsForms.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightOwl.WindowsForms.Services
{
    public class PersonsService
    {
        private IHttpClientService httpClient = HttpClientService.Instance;

        public async Task<Person2> GetPersonsList()
        {    
            try
            {
                var response = await httpClient.GetAsync<Person2>("http://localhost:5001/api/Persons/Get/");
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
            return null;
        }
        public async Task<Person2> AddNewPersonAsync(Person2 newPerson)
        {
            if (newPerson != null)
            {
                try
                {
                    var response = await httpClient.PostAsync<Person2, Person2>("https://localhost:5001/api/Persons/Post/", newPerson);
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
            throw new Exception("Person cannot be null.");
        }


    }
}
