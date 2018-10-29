using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nightOwl
{
    public class Person
    {
        private static int Count;

        public int ID { get; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string MissingDate { get; set; }
        public string AdditionalInfo { get; set; }
        public double CoordX { get; set; }
        public double CoordY { get; set; }
        public string LastSeenDate { get; set; }

        public Person(string name, string birth, string missingd, string add, double coordx=0.0, double coordy=0.0, string lastseendate="-")
        {
            ID = ++Count;
            Name = name;
            BirthDate = birth;
            MissingDate = missingd;
            AdditionalInfo = add;
            CoordX = coordx;
            CoordY = coordy;
            LastSeenDate = lastseendate;
        }

        public override string ToString() { return Name; }

    }
}
