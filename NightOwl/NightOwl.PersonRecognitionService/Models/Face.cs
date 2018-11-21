using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace PersonRecognitionService.Models
{
    public class Face
    {
        [Required]
        public byte[] Photo { get; set; }

        [Required]
        public string PersonName { get; set; }
    }
}