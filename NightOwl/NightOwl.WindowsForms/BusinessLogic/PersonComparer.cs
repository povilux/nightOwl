using System.Collections.Generic;
using NightOwl.WindowsForms.Components;

namespace NightOwl.WindowsForms.BusinessLogic
{
        class PersonNameComparer : IComparer<Person>
        {
            public int Compare(Person x, Person y)
            {
                return x.Name.CompareTo(y.Name);
            }
        }

        class PersonMissingDateComparer : IComparer<Person>
        {
            public int Compare(Person x, Person y)
            {
                return x.MissingDate.CompareTo(y.MissingDate);
            }
        }

        class PersonBirthDateComparer : IComparer<Person>
        {
            public int Compare(Person x, Person y)
            {
                return x.BirthDate.CompareTo(y.BirthDate);
            }
        }

        class PersonComparer : IComparer<Person>
        {
            // The field to compare.
            public enum CompareField
            {
                Name,
                MissingDate,
                BirthDate,
            }
            public CompareField SortBy = CompareField.Name;

            public int Compare(Person x, Person y)
            {
                switch (SortBy)
                {
                    case CompareField.Name:
                        return x.Name.CompareTo(y.Name);
                    case CompareField.MissingDate:
                        return x.MissingDate.CompareTo(y.MissingDate);
                    case CompareField.BirthDate:
                        return x.BirthDate.CompareTo(y.BirthDate);
                }
                return x.Name.CompareTo(y.Name);
            }
        }

    
}
