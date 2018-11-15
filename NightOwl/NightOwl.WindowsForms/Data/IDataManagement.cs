using System.Collections.Generic;
using NightOwl.WindowsForms.Components;

namespace NightOwl.WindowsForms.Data
{
    public interface IDataManagement
    {
        void AddPerson(Person person);
        Person FindPerson(int ID);
        List<Person> GetPersonsCatalog();
        int GetPersonsCount();
        bool LoadData();
        bool SaveData();
    }
}