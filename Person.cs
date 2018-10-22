using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nightOwl
{
    public struct Person
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string MissingDate { get; set; }
        public string AdditionalInfo { get; set; }
        public double CoordX { get; set; }
        public double CoordY { get; set; }
        public string LastSeenDate { get; set; }

        public Person(string name, string birth, string missingd, string add, double coordx=0.0, double coordy=0.0, string lastseendate="-")
        {
            this.Name = name;
            this.BirthDate = birth;
            this.MissingDate = missingd;
            this.AdditionalInfo = add;
            this.CoordX = coordx;
            this.CoordY = coordy;
            this.LastSeenDate = lastseendate;
        }

    }
}
