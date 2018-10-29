using nightOwl.Components;
using nightOwl.Data;
using nightOwl.Models;
using nightOwl.Views;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nightOwl.Presenters
{
    public class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly UserModel _model;

        public LoginPresenter(ILoginView view, UserModel model)
        {
            _view = view;
            _model = model;

            _view.LoginUserClicked += new EventHandler(OnUserLogin);
        }

        public void OnUserLogin(object sender, EventArgs e)
        {
            object obj;
            if((obj = DataManagement.GetInstance().FindUserByName(_view.UserName)) != null)
            {
                User CurrentUser = (User)obj;

                if (String.Equals(_view.Password, CurrentUser.Password))
                {
                    DataManagement.GetInstance().UserID = CurrentUser.ID;

                    FirstPageView firstPage = new FirstPageView();
                    firstPage.StartPosition = FormStartPosition.Manual;
                    //firstPage.Location = Location;
                    firstPage.Show();
                    _view.Hide();
                }
                else
                {
                    MessageBox.Show("The password is wrong!");
                    _view.Password = "";
                }
            }
            else
            {
                MessageBox.Show("Not correct data!");
                _view.Password = "";
            }
        }
    }
}
