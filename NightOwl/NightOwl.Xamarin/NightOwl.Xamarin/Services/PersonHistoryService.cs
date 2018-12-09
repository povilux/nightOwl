using NightOwl.Xamarin.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NightOwl.Xamarin.Services
{
    public class PersonHistoryService : IPersonHistoryService
    {
        private IHttpClientService httpClient = HttpClientService.Instance;

        public async Task<APIMessage<PersonHistoryData>> GetPersonsHistoryList(int page)
        {
            try
            {
                var response = await httpClient.GetAsync<PersonHistoryData>(APIEndPoints.GetPersonHistoryEndPoint + page);
                return response;
            }
            catch (Exception ex)
            {
                ErrorLogger.Instance.LogException(ex);
            }
            return null;
        }

        public async Task<APIMessage<PersonHistoryData>> GetPersonHistoryByName(string name, int page)
        {
            try
            {
                var response = await httpClient.GetAsync<PersonHistoryData>(APIEndPoints.GetPersonHistoryByNameEndPoint + name + "/" + page);
                return response;
            }
            catch (Exception ex)
            {
                ErrorLogger.Instance.LogException(ex);
            }
            return null;
        }

        public async Task<APIMessage<PersonHistory>> AddPersonHistoryAsync(PersonHistory newHistory)
        {
            if (newHistory != null)
            {
                try
                {
                    var response = await httpClient.PostAsync<PersonHistory, PersonHistory>(APIEndPoints.AddHistoryEndPoint, newHistory);
                    return response;
                }
                catch (Exception ex)
                {
                    ErrorLogger.Instance.LogException(ex);
                }
            }
            return null;
        }
        /*
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
        }*/
    }
}
