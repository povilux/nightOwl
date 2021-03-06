﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using NightOwl.WindowsForms.Properties;
using NightOwl.WindowsForms.Components;
using System.Windows.Forms;

namespace NightOwl.WindowsForms.Data
{
    public class DataManagement : IDataManagement
    {
        public List<Person> PersonsCatalog = new List<Person>();
        public List<User> UsersCatalog = new List<User>();
        public int UserID = 0;

        private string DirectoryPath { get; set; } = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Settings.Default.DataFolderPath;
        private string PersonsPath = Settings.Default.PersonsFileName;
        private string UsersPath = Settings.Default.UsersFileName;

        private static readonly Lazy<DataManagement> dataManagement =
           new Lazy<DataManagement>(() => new DataManagement());

        public static DataManagement Instance { get { return dataManagement.Value; } }

        private DataManagement()
        {
            if (!Directory.Exists(DirectoryPath))
                try
                {
                    Directory.CreateDirectory(DirectoryPath);
                } catch
                {
                    DialogResult result = MessageBox.Show("Fatal error. Application closing.", "Error", MessageBoxButtons.OK);
                }
                

            if (!File.Exists(DirectoryPath + PersonsPath))
            {
                var newFile = File.Create(DirectoryPath + PersonsPath);
                newFile.Close();

                File.WriteAllText(DirectoryPath + PersonsPath, "[]");
            }
            if (!File.Exists(DirectoryPath + UsersPath))
            {
                var newFile = File.Create(DirectoryPath + UsersPath);
                newFile.Close();

                File.WriteAllText(DirectoryPath + UsersPath, "[]");
            }
        }

        public List<Person> GetPersonsCatalog()
        {
            return PersonsCatalog;
        }

        public List<User> GetUsersCatalog()
        {
            return UsersCatalog;
        }

        public bool SaveData()
        {
            try
            {
                // Save persons
                File.WriteAllText(DirectoryPath + PersonsPath, JsonConvert.SerializeObject(PersonsCatalog));

                // Save users
                File.WriteAllText(DirectoryPath + UsersPath, JsonConvert.SerializeObject(UsersCatalog));
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool LoadData()
        {
            try
            {
                PersonsCatalog = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(DirectoryPath + PersonsPath));
                UsersCatalog = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(DirectoryPath + UsersPath));
            }
            catch (NullReferenceException e)
            {
                Console.Write(e.Message);
                return false;
            }
            return true;
        }

         public void AddPerson(Person person)
         {
                PersonsCatalog.Add(person);
         }
        public void AddUser(User user)
        {
            UsersCatalog.Add(user);
        }
        public int GetPersonsCount() { return PersonsCatalog.Count; }
        public int GetUsersCount() { return UsersCatalog.Count; }

        public Person FindPerson(int ID)
        {
            return PersonsCatalog.Where(p => p.ID == ID).First();
        }

        public object FindUserByName(string name)
        {
            return UsersCatalog.Where(u => String.Equals(u.Username, name)).FirstOrDefault();
        }

        public object FindUserByEmail(string email)
        {
            return UsersCatalog.Where(u => String.Equals(u.Email, email)).FirstOrDefault();
        }
    }
}
