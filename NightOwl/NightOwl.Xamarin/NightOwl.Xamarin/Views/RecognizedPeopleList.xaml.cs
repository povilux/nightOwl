using NightOwl.Xamarin.Components;
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
	public partial class RecognizedPeopleList : ContentPage
	{
        List<Person> persons;
        ObservableCollection<Person> observableCollection = new ObservableCollection<Person>();
        public RecognizedPeopleList ()
		{
			InitializeComponent ();
		}

        public void AddPersonToList(Person person)
        {
            observableCollection.Add(person);
            BindingContext = observableCollection; //Nežinau ar šitas čia
        }
    }
}