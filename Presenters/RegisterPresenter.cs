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
        private readonly UserModel _model;

        public RegisterPresenter(IRegisterView view, UserModel model)
        {
            _view = view;
            _model = model;

            _view.UserRegistered += new EventHandler(OnUserRegisterClicked);
        }

        public void OnUserRegisterClicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(_view.Username) && !String.IsNullOrWhiteSpace(_view.Password) && !String.IsNullOrWhiteSpace(_view.Email))
            {
                if (DataManagement.GetInstance().FindUserByName(_view.Username) != null)
                {
                    if (DataManagement.GetInstance().FindUserByEmail(_view.Email) != null)
                    {
                        // register user
                        User NewUser = new User(_view.Username, _view.Password, _view.Email);
                        DataManagement.GetInstance().AddUser(NewUser);

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
