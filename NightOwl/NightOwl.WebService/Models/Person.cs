using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NightOwl.WebService.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set;  }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required]
        public string MissingDate { get; set; }

        public string AdditionalInfo { get; set; }

        [Required]
        public int CreatorId { get; private set; }
    }
}

