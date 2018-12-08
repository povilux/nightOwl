using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NightOwl.WebService.Models
{
    public class PersonHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double CoordX { get; set; }
        public double CoordY { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(100)]
        [ForeignKey("SourceFaceUrl")]
        public string SourceFaceUrl { get; set; }

        [JsonIgnore]
        public Face SourceFace { get; set; }

        [Required]
        public string SpottedFaceUrl { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }


    }
}
