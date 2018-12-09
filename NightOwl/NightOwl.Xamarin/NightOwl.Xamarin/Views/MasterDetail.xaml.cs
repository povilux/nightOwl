using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NightOwl.Xamarin.Components;
using NightOwl.Xamarin.Services;

namespace NightOwl.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetail : MasterDetailPage
    {
        public MasterDetail()
        {
            InitializeComponent();
            masterpage.ListView.ItemSelected += OnItemSelected;
            masterpage.LogoutClicked += OnLogoutClicked;

            NavigationPage.SetHasNavigationBar(this, false);
        }

        void OnLogoutClicked(object sender, EventArgs e)
        {
            App.CurrentUser = Guid.Empty;
            Application.Current.MainPage = new NavigationPage(new Login(new UserService()));
            masterpage.ListView.SelectedItem = null;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is MasterMenuItem item)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterpage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}