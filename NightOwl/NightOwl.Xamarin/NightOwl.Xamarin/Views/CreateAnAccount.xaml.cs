using NightOwl.Xamarin.Components;
using NightOwl.Xamarin.Services;
using NightOwl.Xamarin.ViewModel;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NightOwl.Xamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateAnAccount : ContentPage
	{
        private RegisterViewModel RegisterVM { get; set; }
        private IUserService _userService;

        public CreateAnAccount(IUserService userService)
		{
			InitializeComponent ();
            RegisterVM = new RegisterViewModel();
            _userService = userService;
        }

        public async void OnRegisterButtonClicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Username.Text))
            {
                await DisplayAlert(ConfigurationManager.AppSettings["InvalidDataTitle"], ConfigurationManager.AppSettings["RegistrationInvalidUserName"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                return;
            }

            if(string.IsNullOrEmpty(Password.Text))
            {
                await DisplayAlert(ConfigurationManager.AppSettings["InvalidDataTitle"], ConfigurationManager.AppSettings["RegistrationInvalidPassword"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                return;
            }

            if (string.IsNullOrEmpty(Email.Text) || (!string.IsNullOrEmpty(Email.Text) && !Regex.IsMatch(Email.Text, "([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$")))
            {
                await DisplayAlert(ConfigurationManager.AppSettings["InvalidDataTitle"], ConfigurationManager.AppSettings["RegistrationInvalidEmail"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                return;
            }

            RegisterVM.Username = Username.Text;
            RegisterVM.Password = Password.Text;
            RegisterVM.Email = Email.Text;

            User newUser = new User
            {
                UserName = RegisterVM.Username,
                PasswordHash = RegisterVM.Password,
                Email = RegisterVM.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                PhoneNumber = "",
            };
            try
            {
                var result = await _userService.RegisterAsync(newUser);

                if (result.Success)
                {
                    var user = await _userService.GetUserByUsernameAsync(RegisterVM.Username);

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
                    await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], result.Error, ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Instance.LogException(ex);
                await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["SystemErrorMessage"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
            }
        }
    }
}