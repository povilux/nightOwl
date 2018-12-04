using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NightOwl.WebService.Models
{
    public class User : IdentityUser
    {
        public ICollection<Person> AddedPersons { get; set; }
    }
}
