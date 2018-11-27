using System;
using System.Collections.Generic;
using System.Text;

namespace NightOwl.Xamarin.ViewModel
{
    class PersonViewModel
    {
        public string Username;
        public DateTime BirthDate;
        public string AdditionalInfo;
        public DateTime MissingDate;

        public ICollection<byte[]> Faces = new List<byte[]>();
    }
}
