using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nightOwl
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Check if person is logged or not
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if(AuthHandler.CheckAuth(loginBox.Text, passwordBox.Text))
            {
                MessageBox.Show("Prisijungėte sėkmingai!");

                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                loginBox.Clear();
                passwordBox.Clear();
                MessageBox.Show("Neteisingas vartotojo vardas arba slaptažodis!");
            }
           
        }
    }
}
