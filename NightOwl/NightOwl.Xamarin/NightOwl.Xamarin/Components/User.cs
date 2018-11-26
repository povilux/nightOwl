using System;
using System.Collections.Generic;

namespace NightOwl.Xamarin.Components
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public bool PhoneNumberConfirmed { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Person> AddedPersons { get; set; }
    }
}
