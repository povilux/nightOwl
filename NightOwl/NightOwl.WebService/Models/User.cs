using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NightOwl.WebService.Models
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        [NotMapped]
        public ICollection<Person> AddedPersons { get; set; }
    }
}
