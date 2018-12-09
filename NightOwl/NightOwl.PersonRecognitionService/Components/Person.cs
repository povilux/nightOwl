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
    }

    public class Face
    {
        public string BlobURI { get; set; }
        public int OwnerId { get; set; }
    }
}