using Emgu.CV;
using Emgu.CV.Structure;
using nightOwl.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nightOwl.Models
{
    public class PersonModel : IPersonModel
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string MissingDate { get; set; }
        public string AdditionalInfo { get; set; }
        public double CoordX { get; set; }
        public double CoordY { get; set; }
        public string LastSeenDate { get; set; }
        public PhotoList<Image<Bgr, byte>> personPhotos = new PhotoList<Image<Bgr, byte>>();

        public PersonModel() { }

        public PersonModel(string name, string birth, string missingd, string add, PhotoList<Image<Bgr, byte>> photos, double coordx = 0.0, double coordy = 0.0, string lastseendate = "-")
        {
            this.Name = name;
            this.BirthDate = birth;
            this.MissingDate = missingd;
            this.AdditionalInfo = add;
            this.CoordX = coordx;
            this.CoordY = coordy;
            this.LastSeenDate = lastseendate;

            personPhotos = photos;
        }
    }
}
