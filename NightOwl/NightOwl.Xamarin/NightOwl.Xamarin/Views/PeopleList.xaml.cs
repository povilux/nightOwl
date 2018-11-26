using NightOwl.Xamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NightOwl.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PeopleList : ContentPage
    {
        public static Person selectedPerson;
        List<Person> persons;
        public PeopleList()
        {
            persons = AddPerson.personsList;
            ObservableCollection<Person> observableCollection = new ObservableCollection<Person>();  
            foreach(Person p in persons)
            {
                observableCollection.Add(p);
            }
            BindingContext = observableCollection;
            InitializeComponent();
            namesView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                var item = (Person)e.SelectedItem;
                selectedPerson = item;
                AddPerson.ClearPhotoList();
                MessagingCenter.Send(this, "PersonPicked", selectedPerson);
                Navigation.PopAsync();
            };
        }
	}
}
