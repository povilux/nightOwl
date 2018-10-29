using System.Collections.Generic;

namespace nightOwl.Data
{
    public interface IDataManagement
    {
        void AddPerson(Person person);
        Person FindPerson(int ID);
        List<Person> GetPersonsCatalog();
        int GetPersonsCount();
        void LoadData();
        void SaveData();
    }
}