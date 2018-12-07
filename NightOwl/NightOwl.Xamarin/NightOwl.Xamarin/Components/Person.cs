using System;
using System.Collections.Generic;

namespace NightOwl.Xamarin.Components
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string MissingDate { get; set; }
        public string AdditionalInfo { get; set; }
        public string CreatorId { get; set; }

        public ICollection<byte[]> Photos;

        public IEnumerable<Face> FacePhotos { get; set; }




     //   public IEnumerable<PersonHistory> History { get; set; }


        public string CreatorName { get; set; }
        public string CreatorEmail { get; set; }
        public string CreatorPhone { get; set; }

    }
}
