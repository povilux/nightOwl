using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NightOwl.WebService.Models
{
    public class Face
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string BlobURI { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [JsonIgnore]
        public Person Owner { get; set; }

        [JsonIgnore]
        public IEnumerable<PersonHistory> History { get; set; }

        [NotMapped]
        public byte[] PhotoByteArr { get; set; }

        [JsonIgnore]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
