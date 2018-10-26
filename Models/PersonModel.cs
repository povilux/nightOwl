using System;
using System.Linq;
using nightOwl.Views;

namespace nightOwl.Models
{
    public class PersonModel : IPersonModel
    {
        public PersonModel() { }

        public Person CurrentPerson { get; set; }

        public Person FindPerson(string name)
        {
            return (CurrentPerson = FirstPageView.persons.Where(p => String.Equals(p.Name, name)).First());
        }

        public void Add(string name, string birthdate, string missingdate, string addinfo)
        {
            CurrentPerson = new Person(name, birthdate, missingdate, addinfo);
            // To do: add to main list of persons
            FirstPageView.persons.Add(CurrentPerson);
        }
    }
}
