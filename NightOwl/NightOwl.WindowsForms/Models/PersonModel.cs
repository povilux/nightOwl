﻿using System.Drawing;
using System.Linq;
using NightOwl.WindowsForms.Components;
using NightOwl.WindowsForms.Data;
using NightOwl.WindowsForms.Views;

namespace NightOwl.WindowsForms.Models
{
    public class PersonModel : IPersonModel
    {
        public PersonModel() { }

        public Person CurrentPerson { get; set; }

        public void Add(string name, string birthdate, string missingdate, string addinfo)
        {
            CurrentPerson = new Person(DataManagement.Instance.UserID, name, MissingDate:missingdate, BirthDate:birthdate, AdditionalInfo:addinfo);
            DataManagement.Instance.AddPerson(CurrentPerson);
        }

        public void GroupPersonsByCreator()
        {
                var query = (DataManagement.Instance.GetUsersCatalog()).
                GroupJoin(DataManagement.Instance.GetPersonsCatalog(),
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
