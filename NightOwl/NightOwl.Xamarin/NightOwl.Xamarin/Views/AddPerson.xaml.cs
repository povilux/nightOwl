using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NightOwl.Xamarin.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NightOwl.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPerson : ContentPage
    {
        public AddPerson()
        {
            InitializeComponent();
        }

        async void OnSelectPersonButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PeopleList());
        }
    }
}