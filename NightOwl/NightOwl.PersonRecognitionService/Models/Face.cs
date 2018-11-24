using System.ComponentModel.DataAnnotations;

namespace NightOwl.PersonRecognitionService.Models
{
    public class Face
    {
        [Required]
        public byte[] Photo { get; set; }

        [Required]
        public string PersonName { get; set; }
    }
}