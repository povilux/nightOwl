using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public IEnumerable<Face> FacePhotos { get; set; }


        public string CreatorId { get; set; }

        [JsonIgnore]
        public User Creator { get; set; }

        [JsonIgnore]
        public IEnumerable<PersonHistory> History { get; set; }

        [NotMapped]
        public ICollection<byte[]> Photos { get; set; }

        [NotMapped]
        public string CreatorName { get; set; }

        [NotMapped]
        public string CreatorEmail { get; set; }

        [NotMapped]
        public string CreatorPhone { get; set; }
    }
}

