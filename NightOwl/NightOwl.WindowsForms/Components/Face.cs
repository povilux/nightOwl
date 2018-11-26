using Emgu.CV;
using Emgu.CV.Structure;
using System.ComponentModel.DataAnnotations;

namespace NightOwl.WindowsForms.Components
{
    public struct Face
    {
        public int PersonLabelId { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public Image<Gray, byte> Image { get; set; }
    }

    public class Face2
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        [Required]
        public string PersonName { get; set; }
    }

    public class Face3
    {
        public byte[] Photo { get; set; }
        public string PersonName { get; set; }
    }
}
