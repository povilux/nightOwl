using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using nightOwl.Properties;

namespace nightOwl.Data
{
    public class DataManagement : IDataManagement
    {
        public List<Person> PersonsCatalog = new List<Person>();
        private string DirectoryPath { get; set; } = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Settings.Default.DataFolderPath;
        private string PersonsPath = Settings.Default.PersonsFileName;

        protected static DataManagement _obj;

        private DataManagement()
        {
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);

            if (!File.Exists(DirectoryPath + PersonsPath))
            {
                var newFile = File.Create(DirectoryPath + PersonsPath);
                File.WriteAllText(DirectoryPath + PersonsPath, "[]");
                newFile.Close();
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

        /*public List<Person> GetPersonsCatalog()
        {
            return PersonsCatalog;
        }*/

        public void SaveData()
        {
            // Save persons
            File.WriteAllText(DirectoryPath + PersonsPath, JsonConvert.SerializeObject(PersonsCatalog));

            // Save users
       //     File.WriteAllText(DirectoryPath + UsersPath, JsonConvert.SerializeObject(UsersCatalog));
        }

        public void LoadData()
        {
            try
            {
                PersonsCatalog = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(DirectoryPath + PersonsPath));
         //    UsersPath = JsonConvert.DeserializeObject<Catalog<User>>(File.ReadAllText(DirectoryPath + PersonsPath));
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

     public void AddPerson(Person person)
     {
         PersonsCatalog.Add(person);
     }
        public int GetPersonsCount() { return PersonsCatalog.Count; }

        /* public void AddUser(User user)
         {
             UsersCatalog.Add(user);
         }*/

        public Person FindPerson(int ID)
        {
            return PersonsCatalog.Where(p => p.ID == ID).First();
        }
    }

    /*

    public void AddBook(IBookModel book)
    {
        Books.Add(book);
        SerializeBooks();
    }

    public void RemoveBook(IBookModel book)
    {
        //TODO logic for taken books here or in the book class
        Books.Remove(book);
        SerializeBooks();
    }

    public void AddUser(IUserModel user)
    {
        Users.Add(user);
        SerializeUsers();
    }

    public void RemoveUser(IUserModel user)
    {
        Users.Remove(user);
        SerializeUsers();
    }

    public void AddAuthor(Author author)
    {
        Authors.Add(author);
        SerializeAuthors();
    }

    public void RemoveAuthor(Author author)
    {
        Authors.Remove(author);
        SerializeAuthors();
    }

    public IUserModel FindUser(string label)
    {
        return Instance.Users.Find(x => x.ID == int.Parse(label));
    }

    public IBookModel FindBook(string label)
    {
        return Instance.Books.Find(x => x.ID == int.Parse(label));
    }*/
}
