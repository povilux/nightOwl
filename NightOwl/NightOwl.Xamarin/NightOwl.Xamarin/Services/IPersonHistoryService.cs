using System.Collections.Generic;
using System.Threading.Tasks;
using NightOwl.Xamarin.Components;

namespace NightOwl.Xamarin.Services
{
    public interface IPersonHistoryService
    {
        Task<APIMessage<PersonHistory>> AddPersonHistoryAsync(PersonHistory newHistory);
        Task<APIMessage<PersonHistoryData>> GetPersonHistoryByName(string name, int page);
        Task<APIMessage<PersonHistoryData>> GetPersonsHistoryList(int page);
    }
}