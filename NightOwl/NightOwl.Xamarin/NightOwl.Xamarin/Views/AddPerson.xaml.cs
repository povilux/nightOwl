using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NightOwl.Xamarin.Models;
using NightOwl.Xamarin.Views;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace NightOwl.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPerson : ContentPage
    {
        private Person personToSend = new Person();
        private static List<Image> images = new List<Image>();
        private Image image = null;
        public static List<Person> personsList = null;
        public static void SetPersonsList(List<Person> persons)
        {
            personsList = persons;
        }

        public AddPerson()
        {
            InitializeComponent();
            addPhotoButton.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                });


                if (file == null)
                    return;

                image = new Image();
                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
                images.Add(image);
            };
        }

        async void OnSelectPersonButtonClicked(object sender, EventArgs e)
        {
            // Sukurta nauja Person klase - Models.Person; joje truksta Creator lauko (gal galima kazkaip ir be jo?)

            // testiniai duomenys - reikia tikrus pasiimt per webservisa ( List<Person>)
            List<Person> testPersons = new List<Person>();
            Person testPerson1 = new Person();
            testPerson1.Id = 1;
            testPerson1.Name = "jonas";
            testPerson1.BirthDate = "19950315";
            testPerson1.MissingDate = "20170512";
            testPerson1.AdditionalInfo = "";
            Person testPerson2 = new Person();
            testPerson2.Id = 2;
            testPerson2.Name = "petras";
            testPerson2.BirthDate = "19850415";
            testPerson2.MissingDate = "20160911";
            testPerson2.AdditionalInfo = "";
            Person testPerson3 = new Person();
            testPerson3.Id = 3;
            testPerson3.Name = "algis";
            testPerson3.BirthDate = "19250322";
            testPerson3.MissingDate = "20180521";
            testPerson3.AdditionalInfo = "Should be considered armed and dangerous";

            testPersons.Add(testPerson1);
            testPersons.Add(testPerson2);
            testPersons.Add(testPerson3);
            // testiniu duomenu pabaiga; nutrinti, kai nebereiks

            SetPersonsList(testPersons);                        // pakeisti testPersons tikru List<Person>
            if(personsList != null)
            {
                MessagingCenter.Subscribe<PeopleList, Person>(this, "PersonPicked", (personSender, personObject) =>
                {
                    SetValues(personObject);
                });
                await Navigation.PushAsync(new PeopleList());
            }
        }

        void OnAddPersonsDataButtonClicked(object sender, EventArgs e)
        {
            if(nameTextBox.Text != "")
            {
                // set name for object to be sent
                personToSend.Name = nameTextBox.Text;

                // set birth date for object to be sent
                string birthDate = birthdatePicker.Date.Year.ToString() + birthdatePicker.Date.Month.ToString();
                if(birthdatePicker.Date.Day < 10)
                {
                    birthDate += "0";
                }
                birthDate += birthdatePicker.Date.Day.ToString();

                // set missing date for object to be sent
                string missingDate = missingdatePicker.Date.Year.ToString() + missingdatePicker.Date.Month.ToString();
                if (missingdatePicker.Date.Day < 10)
                {
                    missingDate += "0";
                }
                missingDate += missingdatePicker.Date.Day.ToString();

                // set additional info for object to be sent
                personToSend.AdditionalInfo = addInfoTextBox.Text;

                SetViewToDefaultValues();
                ClearPhotoList();

                try
                {
                    // SEND PERSON OBJECT TO WEBSERVICE     - To be implemented
                }
                catch (Exception)
                {
                    DisplayAlert("Action failed", "Adding person's data failed.", "OK");
                }



            } else
            {
                DisplayAlert("Invalid data", "Please write a name", "OK");
            }
        }

        private void SetValues(Person person)
        {
            nameTextBox.Text = person.Name;
            var birthDate = DateTime.ParseExact(person.BirthDate,
                                  "yyyyMMdd",
                                   CultureInfo.InvariantCulture);
            birthdatePicker.Date = birthDate;
            var missingDate = DateTime.ParseExact(person.MissingDate,
                                  "yyyyMMdd",
                                   CultureInfo.InvariantCulture);
            missingdatePicker.Date = missingDate;
            addInfoTextBox.Text = person.AdditionalInfo;
        }

        public static void ClearPhotoList()
        {
            images.Clear();
        }

        public void SetViewToDefaultValues()
        {
            nameTextBox.Text = "";
            addInfoTextBox.Text = "";
            birthdatePicker.Date = birthdatePicker.MinimumDate;
            missingdatePicker.Date = missingdatePicker.MaximumDate;
        }
    }
}
