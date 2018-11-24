using NightOwl.Xamarin.Services;
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
        private IUserService _userService;
        private LoginViewModel LoginVM { get; set; }

        public Login (IUserService userService)
		{
			InitializeComponent ();
            LoginVM = new LoginViewModel();
            _userService = userService;
        }

        public async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            LoginVM.Username = Username.Text;
            LoginVM.Password = Password.Text;
 
            var result = await _userService.LoginAsync(LoginVM.Username, LoginVM.Password);

            if (result.Success)
            {
                var user = await _userService.GetUserByUsernameAsync(LoginVM.Username);

                if (user.Success)
                {
                    App.CurrentUser = user.Message;
                    await Navigation.PushAsync(new MainPage());
                }
                else
                    await DisplayAlert(Messages.SystemErrorTitle, Messages.SystemErrorMessage, Messages.MessageBoxClosingBtnText);
            }
            else
            {
                await DisplayAlert(Messages.InvalidDataTitle, Messages.NotValidLoginInfoError, Messages.MessageBoxClosingBtnText);
            }

        }

        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAnAccount(new UserService()));
        }
	}
}