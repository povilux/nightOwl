using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NightOwl.PersonRecognitionService.Components
{
    public class Person
    {
        public int Id { get; set; } 
        public IEnumerable<Face> FacePhotos { get; set; }

        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string MissingDate { get; set; }
        public string AdditionalInfo { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string CreatorEmail { get; set; }
        public string CreatorPhone { get; set; }
    }

    public class Face
    {
        public string BlobURI { get; set; }
        public int OwnerId { get; set; }
    }
}