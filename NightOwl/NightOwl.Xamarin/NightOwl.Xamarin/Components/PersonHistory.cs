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
        public int SourceFaceId { get; set; }

        public int PersonId { get; set; }
        public string PersonName { get; set; }
    }
}
