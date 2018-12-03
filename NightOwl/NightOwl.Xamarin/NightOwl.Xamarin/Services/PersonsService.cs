using NightOwl.Xamarin.Components;
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

        public async Task<APIMessage<IEnumerable<Person>>> GetPersonsList()
        {    
            try
            {
                var response = await httpClient.GetAsync<IEnumerable<Person>>(APIEndPoints.GetPersonEndPoint);
                return response;
            }
            catch (Exception ex)
            {
                ErrorLogger.Instance.LogException(ex);
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
                    ErrorLogger.Instance.LogException(ex);
                }
            }
            return null;
        }

        public async Task<APIMessage<bool>> DeletePersonAsync(int id = -1)
        {
            if (id != -1)
            {
                try
                {
                    var response = await httpClient.DeleteAsync<bool>(APIEndPoints.DeletePersonEndPoint + id);
                    return response;
                }
                catch (Exception ex)
                {
                    ErrorLogger.Instance.LogException(ex);
                }
            }
            return null;
        }

        public async Task<APIMessage<Person>> UpdatePersonAsync(Person updatePerson, int id)
        {
            if (updatePerson != null)
            {
                try
                {
                    var response = await httpClient.PutAsync<Person>(APIEndPoints.UpdatePersonEndPoint + id, updatePerson);
                    return response;
                }
                catch (Exception ex)
                {
                    ErrorLogger.Instance.LogException(ex);
                }
            }
            return null;
        }
    }
}
