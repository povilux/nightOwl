using NightOwl.Xamarin.Components;
using NightOwl.Xamarin.Services;
using NightOwl.Xamarin.Views;
using System;
using System.Configuration;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NightOwl.Xamarin
{
    public partial class App : Application
    {
        public static Guid CurrentUser { get; set; }

        public App()
        {
            InitializeComponent();

            if (CurrentUser == Guid.Empty)
                MainPage = new NavigationPage(new Login(new UserService()));
            

            /*
            if (CurrentUser == null)
                MainPage = new NavigationPage(new Login(new UserService()));
            else
                MainPage = new NavigationPage(new MainPage());
            */
            
            // Application.Current.MainPage = new NavigationPage(new MasterDetail());
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            CurrentUser = Guid.Empty;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
