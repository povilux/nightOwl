using System;
using System.Collections.Generic;
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
            CurrentPerson = new Person(DataManagement.GetInstance().UserID, name, MissingDate:missingdate, BirthDate:birthdate, AdditionalInfo:addinfo);
            DataManagement.GetInstance().AddPerson(CurrentPerson);
        }

        public void GroupPersonsByCreator()
        {
                var query = (DataManagement.GetInstance().GetUsersCatalog()).
                GroupJoin(DataManagement.GetInstance().GetPersonsCatalog(),
                u => u.ID,
                p => p.CreatorID,
                (u, personsGroup) => new
                {
                    UserName = u.Username,
                    Persons = personsGroup
                });



          /*   foreach (var group in query)
               {
                   Console.WriteLine("Creators name:" + group.UserName);

                   foreach (var person in group.Persons)
                       Console.WriteLine(person.Name);
               }*/
        }
    }
}
