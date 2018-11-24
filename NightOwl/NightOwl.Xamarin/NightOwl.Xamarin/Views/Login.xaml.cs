using NightOwl.Xamarin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NightOwl.Xamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{

        private LoginViewModel LoginVM { get; set; }
        public Login ()
		{
			InitializeComponent ();
            LoginVM = new LoginViewModel();
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            //Here you need to add login code
            LoginVM.Username = Username.Text;
            LoginVM.Password = Password.Text;
        }

        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAnAccount());
        }
	}
}