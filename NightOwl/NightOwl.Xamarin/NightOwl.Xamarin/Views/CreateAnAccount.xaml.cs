using NightOwl.Xamarin.Components;
using NightOwl.Xamarin.Exceptions;
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
                PhoneNumber = ""
            };
            try
            {
                var result = await _userService.RegisterAsync(newUser);

                if(result.Success)
                {
                    var user = await _userService.GetUserByUsernameAsync(RegisterVM.Username);

                    if (user.Success)
                    {
                        App.CurrentUser = user.Message;
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                        await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["SystemErrorMessage"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                }
                else
                {
                    await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], result.Error, ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bad registration: " + ex);
                await DisplayAlert(ConfigurationManager.AppSettings["SystemErrorTitle"], ConfigurationManager.AppSettings["SystemErrorMessage"], ConfigurationManager.AppSettings["MessageBoxClosingBtnText"]);
            }
        }
    }
}