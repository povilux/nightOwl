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
    public partial class LoginFormView : Form, ILoginView
    {
        public static LoginFormView self;
        private readonly LoginPresenter _presenter;

        public LoginFormView()
        {
            InitializeComponent();
            self = this;

            _presenter = new LoginPresenter(this);
        }


        private void RegisterButton_Click(object sender, EventArgs e)
        {
            RegisterView registerForm = new RegisterView();
            registerForm.StartPosition = FormStartPosition.Manual;
            registerForm.Location = Location;
            registerForm.Show();
            this.Hide();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            LoginUserClicked(sender, e);
        }

        public event EventHandler LoginUserClicked;
        public string UserName { get { return UserNameField.Text; } }
        public string Password { get { return PasswordField.Text; } set { PasswordField.Text = value;  } }
    }
}
