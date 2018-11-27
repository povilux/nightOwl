using NightOwl.Xamarin.Components;
using NightOwl.Xamarin.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace NightOwl.Xamarin.Views
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PeopleList : ContentPage
	{     
    //    public delegate void PersonSelectedEventHandler(object sender, PersonSelectedEventArgs args);
      //  public event PersonSelectedEventHandler PersonSelected;

        private PersonsService _personsService;

        public PeopleList()
        {
            InitializeComponent();

            _personsService = new PersonsService();

            namesView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                //PersonSelected?.Invoke(sender, new PersonSelectedEventArgs((Person)e.SelectedItem));
                MessagingCenter.Send(this, "PersonPicked", (Person)e.SelectedItem);
                Navigation.PopAsync();
            };
        }
        protected async override void OnAppearing()
        {
            BindingContext = await GetPersonsList();
        }

        public async Task<IEnumerable<Person>> GetPersonsList()
        {
            var personList = await _personsService.GetPersonsList();

            if (personList.Success)
            {
                return personList.Message.ToList();
            }
            else
            {
                await DisplayAlert("Error", personList.Error /*"System error"*/, "Close");
                return null;
            }
        }
    }
    /*
    public class PersonSelectedEventArgs : EventArgs 
    {
        public Person SelectedPerson { get; set; }

        public PersonSelectedEventArgs(Person person)
        {
            SelectedPerson = person;
        }
    }*/
}