using NightOwl.Xamarin.Services;
using NightOwl.Xamarin.ViewModel;
using PCLAppConfig;
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
            if (string.IsNullOrEmpty(Username.Text))
            {
                await DisplayAlert(ConfigurationManager.AppSettings["InvalidDataTitle"], ConfigurationManager.AppSettings["NotValidLoginInfoError"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                return;
            }

            if (string.IsNullOrEmpty(Password.Text))
            {
                await DisplayAlert(ConfigurationManager.AppSettings["InvalidDataTitle"], ConfigurationManager.AppSettings["NotValidLoginInfoError"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                return;
            }

            LoginVM.Username = Username.Text;
            LoginVM.Password = Password.Text;

            try
            {
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
                    {
                        ErrorLogger.Instance.LogError(user.Error);
                        await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["SystemErrorMessage"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                    }
                }
                else
                {
                    ErrorLogger.Instance.LogError(result.Error);
                    await DisplayAlert(ConfigurationManager.AppSettings["InvalidDataTitle"], ConfigurationManager.AppSettings["NotValidLoginInfoError"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                }
            }
            catch(Exception ex)
            {
                ErrorLogger.Instance.LogException(ex);
                await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["SystemErrorMessage"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
            }

        }

        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAnAccount(new UserService()));
        }
	}
}