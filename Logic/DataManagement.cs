using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nightOwl.Logic
{
    public static class DataManagement
    {
        private static Catalog<Person> PersonsCatalog = new Catalog<Person>();

        public static Catalog<Person> GetPersonsCatalog()
        {
            return PersonsCatalog;
        }

        // to do: save/load data
    }
}
