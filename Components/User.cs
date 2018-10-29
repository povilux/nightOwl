using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nightOwl.Components
{
    public struct User
    {
        private static int Count;

        public int ID { get; set; }
        public string Username { get; set;  }
        public string Password { get; set;  }
        public string Email { get; set; }

        public User(string username, string password, string email)
        {
            ID = ++Count;
            Username = username;
            Password = password;
            Email = email;
        }

    }
}
