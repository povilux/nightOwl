using NightOwl.Xamarin.Components;
using NightOwl.Xamarin.Services;
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
	public partial class ManagePage : ContentPage
	{
        private PersonsService _personsService;
        private Person _personSelected = null;
        List<Person> _personList;

		public ManagePage ()
		{
			InitializeComponent ();
            picker.SelectedIndex = 1; 
            _personsService = new PersonsService();
            GetPersons();
            picker.SelectedIndexChanged += PickerIndexChanged;

            listToShow.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                if (e.SelectedItem != null)
                {
                    _personSelected = (Person)e.SelectedItem;
                    Navigation.PushAsync(new AddPerson(_personSelected));
                }
            };
		}

        void PickerIndexChanged(object sender, EventArgs e)
        {
            GetPersons();
        }


        protected override void OnAppearing()
        {
            _personSelected = null;
            listToShow.SelectedItem = null;
            GetPersons();
        }

        async void OnAddPersonButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPerson());
        }

        void OnNameButtonClicked(object sender, EventArgs e)
        {
            var peopleInOrder = (_personList.OrderBy(Person => Person.Name)).ToList();
            _personList = peopleInOrder;
            BindingContext = _personList;
        }

        void OnBirthdayButtonClicked(object sender, EventArgs e)
        {
            var peopleInOrder = (_personList.OrderBy(Person => Person.BirthDate)).ToList();
            _personList = peopleInOrder;
            BindingContext = _personList;
        }

        void OnMissingButtonClicked(object sender, EventArgs e)
        {
            var peopleInOrder = (_personList.OrderBy(Person => Person.MissingDate)).ToList();
            _personList = peopleInOrder;
            BindingContext = _personList;
        }

        void OnAddInfoButtonClicked(object sender, EventArgs e)
        {
            var peopleInOrder = (_personList.OrderBy(Person => Person.AdditionalInfo)).ToList();
            _personList = peopleInOrder;
            BindingContext = _personList;
        }

        private async void GetPersons()
        {
            _personList = (List<Person>)await GetPersonsList();

            if (_personList != null)
            {
                if (picker.SelectedIndex == 0)
                {
                    var newPersonsList = (from person in _personList
                                          where person.CreatorId == App.CurrentUser
                                          select person).ToList();
                    _personList = newPersonsList;
                    BindingContext = _personList;
                }
                else
                {
                    if (picker.SelectedIndex == 1)
                    {
                        BindingContext = _personList;
                    }
                }
            }         
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
                await DisplayAlert("Error", personList.Error, "Close");
                return null;
            }
        }
    }
}