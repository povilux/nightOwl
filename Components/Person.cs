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
        public int CreatorID { get; private set; }

        public Person(int CreatorID, string Name, string BirthDate, string MissingDate, string AdditionalInfo="-", double CoordX=0.0, double CoordY=0.0, string LastSeenDate="-")
        {
            ID = ++Count;
            this.CreatorID = CreatorID;
            this.Name = Name;
            this.BirthDate = BirthDate;
            this.MissingDate = MissingDate;
            this.AdditionalInfo = AdditionalInfo;
            this.CoordX = CoordX;
            this.CoordY = CoordY;
            this.LastSeenDate = LastSeenDate;
        }

        public override string ToString() { return Name; }

    }
}
