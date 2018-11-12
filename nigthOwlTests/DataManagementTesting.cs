using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using nightOwl;
using nightOwl.Data;
using nightOwl.Components;

namespace nigthOwlTests
{
    [TestClass]
    public class DataManagementTesting
    {
        Person testPerson1 = new Person(1, "Jonas", "1952-09-25", "2018-04-11");
        User testUser1 = new User("Jonas", "password", "jonas@gmail.com");
        User testUser2 = new User("Jonas", "password", "badEmail");

        [TestMethod]
        public void PersonSavesAndLoadsFromFileCorrectly()
        {
            // Arrange
            DataManagement dm = DataManagement.GetInstance();
            dm.LoadData();
            int personCatalogSize = dm.PersonsCatalog.Count;
            int userCatalogSize = dm.UsersCatalog.Count;
            dm.PersonsCatalog.Add(testPerson1);
            dm.UsersCatalog.Add(testUser1);
            dm.SaveData();
            dm.LoadData();

            Assert.AreEqual(personCatalogSize + 1, dm.PersonsCatalog.Count);
            Assert.AreEqual(userCatalogSize + 1, dm.UsersCatalog.Count);

            Person writtenAndReadPerson = dm.PersonsCatalog.ElementAt(dm.PersonsCatalog.Count - 1);
            Assert.AreEqual(testPerson1.Name, writtenAndReadPerson.Name);
            Assert.AreEqual(testPerson1.BirthDate, writtenAndReadPerson.BirthDate);
            Assert.AreEqual(testPerson1.MissingDate, writtenAndReadPerson.MissingDate);

            User writtenAndReadUser = dm.UsersCatalog.ElementAt(dm.UsersCatalog.Count - 1);
            Assert.AreEqual(testUser1.Username, writtenAndReadUser.Username);
            Assert.AreEqual(testUser1.Password, writtenAndReadUser.Password);
            Assert.AreEqual(testUser1.Email, writtenAndReadUser.Email);
        }

        [TestMethod]
        public void NewUserEmailIsValidated()
        {

        }
    }
}
