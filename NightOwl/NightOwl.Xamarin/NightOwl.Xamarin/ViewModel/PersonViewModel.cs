using NightOwl.Xamarin.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace NightOwl.Xamarin.ViewModel
{
    class PersonViewModel
    {
        public int Id;
        public string Username;
        public DateTime BirthDate;
        public string AdditionalInfo;
        public DateTime MissingDate;

       // public IList<byte[]> Faces = new List<byte[]>();
        public IList<Face> Faces = new List<Face>();
    }
}
