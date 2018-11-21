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
	public partial class CreateAnAccount : ContentPage
	{
        private RegisterViewModel RegisterVM { get; set; }

		public CreateAnAccount ()
		{
			InitializeComponent ();
            RegisterVM = new RegisterViewModel();
		}

        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            RegisterVM.Username = Username.Text;
            RegisterVM.Password = Password.Text;
            RegisterVM.Email = Email.Text;
        }
    }
}