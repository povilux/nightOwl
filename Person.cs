using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nightOwl
{
    public class Person
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string MissingDate { get; set; }
        public string AdditionalInfo { get; set; }

        public Person(string name, string birth, string missingd, string add)
        {
            this.Name = name;
            this.BirthDate = birth;
            this.MissingDate = missingd;
            this.AdditionalInfo = add;
        }

    }
}
