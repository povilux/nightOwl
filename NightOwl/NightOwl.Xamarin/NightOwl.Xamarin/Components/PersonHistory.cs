using System;
using System.Collections.Generic;
using System.Text;

namespace NightOwl.Xamarin.Components
{
    public class PersonHistory
    {
        public int Id { get; set; }

        public double CoordX { get; set; }
        public double CoordY { get; set; }
        public DateTime Date { get; set; }

        public string SourceFaceUrl { get; set; }
        public string SpottedFaceUrl { get; set; }

        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string CreatorName { get; set; }
        public string CreatorEmail { get; set; }

        public override string ToString()
        {
            return Id + "; " + Date + "; " + PersonName + "; " + CreatorName;
        }
    }
}
