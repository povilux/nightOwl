using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace PersonRecognitionService.Models
{
    public class Face
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        [Required]
        public string PersonName { get; set; }
    }
}