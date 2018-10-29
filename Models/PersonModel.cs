using System;
using System.Linq;
using nightOwl.Data;
using nightOwl.Views;

namespace nightOwl.Models
{
    public class PersonModel : IPersonModel
    {
        public PersonModel() { }

        public Person CurrentPerson { get; set; }

        public void Add(string name, string birthdate, string missingdate, string addinfo)
        {
            CurrentPerson = new Person(name, birthdate, missingdate, addinfo);
            DataManagement.GetInstance().AddPerson(CurrentPerson);
        }
    }
}
