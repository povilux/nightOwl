using nightOwl.Components;
using nightOwl.Data;
using nightOwl.Models;
using nightOwl.Views;
using System;
using System.Windows.Forms;

namespace nightOwl.Presenters
{
    public class RegisterPresenter
    {
        private readonly IRegisterView _view;

        public RegisterPresenter(IRegisterView view)
        {
            _view = view;

            _view.UserRegistered += new EventHandler(OnUserRegisterClicked);
        }

        public void OnUserRegisterClicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(_view.Username) && !String.IsNullOrWhiteSpace(_view.Password) && !String.IsNullOrWhiteSpace(_view.Email))
            {
                if (DataManagement.Instance.FindUserByName(_view.Username) != null)
                {
                    if (DataManagement.Instance.FindUserByEmail(_view.Email) != null)
                    {
                        // register user
                        User NewUser = new User(_view.Username, _view.Password, _view.Email);
                        DataManagement.Instance.AddUser(NewUser);

                        LoginFormView.self.Show();
                        _view.Hide();
                    }
                    else
                        MessageBox.Show("User with this email already are!");
                }
                else
                    MessageBox.Show("User with this username already are!");

            }
            else
                MessageBox.Show("Informatio is not filled!");
        }

    }
}
