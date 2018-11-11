using nightOwl.Models;
using nightOwl.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nightOwl.Views
{
    public partial class RegisterView : Form, IRegisterView
    {
        private readonly RegisterPresenter _presenter;

        public RegisterView()
        {
            InitializeComponent();
            _presenter = new RegisterPresenter(this);
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            UserRegistered(sender, e);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            LoginFormView.self.Show();
            Close();
        }

        public string Username { get { return UserNameField.Text; } }
        public string Password { get { return PasswordField.Text; } }
        public string Email { get { return EmailField.Text; } }

        public event EventHandler UserRegistered;
    }
}
