using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using nightOwl.Properties;
using nightOwl.Components;
using System.Windows.Forms;

namespace nightOwl.Data
{
    public class DataManagement : IDataManagement
    {
        public List<Person> PersonsCatalog = new List<Person>();
        public List<User> UsersCatalog = new List<User>();
        public int UserID = 0;

        private string DirectoryPath { get; set; } = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Settings.Default.DataFolderPath;
        private string PersonsPath = Settings.Default.PersonsFileName;
        private string UsersPath = Settings.Default.UsersFileName;


        protected static DataManagement _obj;

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

        public static DataManagement GetInstance()
        {
            if (_obj == null)
                _obj = new DataManagement();

            return _obj;
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
            catch (Exception e)
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
