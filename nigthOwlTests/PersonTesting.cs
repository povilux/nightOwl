using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nightOwl;
using nightOwl.Components;

namespace nigthOwlTests
{
    [TestClass]
    public class PersonTesting
    {
        Person person1 = new Person(1, "Jonas", "1952-09-25", "2018-04-11");
        Person person2 = new Person(1, "Petras", "1945-03-21", "2017-02-25");
        Person person3 = new Person(1, "Petras", "1945-03-21", "2017-02-25");

        [TestMethod]
        public void ToStringValueIsAName()
        {
            //Arrange
            string expectedName = "Jonas";

            //Act
            string actualName = person1.ToString();

            //Assert
            Assert.AreEqual(expectedName, actualName);
        }

        PersonComparer _comparer = new PersonComparer();
        int expectedValue;
        int actualValue;

        [TestMethod]
        public void ComparerComparesByName()
        {
            //Arrange by name
            _comparer.SortBy = PersonComparer.CompareField.Name;

            // Act & Assert
            expectedValue = 0;
            actualValue = _comparer.Compare(person3, person2);
            Assert.AreEqual(expectedValue, actualValue, 0.001, "Same data comparison gives wrong answer");

            expectedValue = -1;
            actualValue = _comparer.Compare(person1, person2);
            if (actualValue < 0)
                actualValue = -1;
            Assert.AreEqual(expectedValue, actualValue, 0.001, "Comparison by name gives wrong answer");

            expectedValue = 1;
            actualValue = _comparer.Compare(person2, person1);
            if (actualValue > 0)
                actualValue = 1;
            Assert.AreEqual(expectedValue, actualValue, 0.001, "Comparison by name gives wrong answer");
        }

        [TestMethod]
        public void ComparerComparesByDate()
        {
            // Arrange By BirthDate;
            _comparer.SortBy = PersonComparer.CompareField.BirthDate;

            // Act & Assert

            expectedValue = 0;
            actualValue = _comparer.Compare(person3, person2);
            Assert.AreEqual(expectedValue, actualValue, 0.001, "Same data comparison gives wrong answer");

            expectedValue = -1;
            actualValue = _comparer.Compare(person2, person1);
            if (actualValue < 0)
                actualValue = -1;
            Assert.AreEqual(expectedValue, actualValue, 0.001, "Comparison by date gives wrong answer");

            expectedValue = 1;
            actualValue = _comparer.Compare(person1, person2);
            if (actualValue > 0)
                actualValue = 1;
            Assert.AreEqual(expectedValue, actualValue, 0.001, "Comparison by date gives wrong answer");

        }
    }
}
