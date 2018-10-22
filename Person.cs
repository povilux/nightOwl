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
        public string AdditionalInfo { get; set; }

        public Person(string name, string birth, string add)
        {
            this.Name = name;
            this.BirthDate = birth;
            this.AdditionalInfo = add;
        }
    }
}
