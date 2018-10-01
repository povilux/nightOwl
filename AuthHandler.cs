using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nightOwl
{
    class AuthHandler
    {
        private static int userRole = 0;

        public static int GetUserRole() { return userRole; }

        public static bool CheckAuth(String username, String password)
        {
            String line = "";
            String[] splitedLine;

            try
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + "/data/users.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        splitedLine = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        if (username.Equals(splitedLine[0]) && password.Equals(splitedLine[1]))
                        {
                            userRole = Int32.Parse(splitedLine[2]);
                            return true;
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Sistema neveikia! Prisijungti neįmanoma!");
            }
            return false;
        }
    }
}
