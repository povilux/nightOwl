using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NightOwl.WebService.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required]
        public string MissingDate { get; set; }

        public string AdditionalInfo { get; set; }

        public string CreatorId { get; set; }

        [JsonIgnore]
        public User Creator { get; set; }

        [NotMapped]
        public ICollection<byte[]> Photos { get; set; }

        [Required]
        public IEnumerable<Face> FacePhotos { get; set; }

        [JsonIgnore]
        public IEnumerable<PersonHistory> History { get; set; }

        [NotMapped]
        public string CreatorName;

        [NotMapped]
        public string CreatorEmail;

        [NotMapped]
        public string CreatorPhone;

    }
}

