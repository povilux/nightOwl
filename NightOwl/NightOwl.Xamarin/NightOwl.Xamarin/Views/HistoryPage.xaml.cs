using NightOwl.Xamarin.Components;
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
	public partial class HistoryPage : ContentPage
	{
        private string name;
        private int currentPageNumber = 1;
        private int numberOfPages = 0;
        private int numberOfItems = 0;
        private List<PersonHistory> historyListToShow;    

		public HistoryPage()
		{
			InitializeComponent();
            filterPicker.SelectedIndex = 0;
            entry.IsVisible = false;
            pageNumberLabel.IsVisible = false;
            previousPageButton.IsVisible = false;
            nextPageButton.IsVisible = false;
            filterPicker.SelectedIndexChanged += FilterPickerIndexChanged;
            historyListToShow = new List<PersonHistory>();
            imageToShow = null;
            listToShow.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                PersonHistory selectedPerson = (PersonHistory)e.SelectedItem;

                // imageToShow = ?
            };
		}

        private void FilterPickerIndexChanged(object sender, EventArgs e)
        {
            if(filterPicker.SelectedIndex == 0)
            {
                entry.IsVisible = false;
            } else
            {
                if(filterPicker.SelectedIndex == 1)
                {
                    entry.IsVisible = true;
                }
            }
        }

        async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            numberOfItems = 0;
            imageToShow = null;

            if (filterPicker.SelectedIndex == 0)
            {
                // get with no filter:                  currentPage = 1, t.y. norime gauti pirmus 10 irasu is db
                    
                //  historyListToShow = ?         //  gaunam 10 pirmu irasu is db kaip List<PersonHistory>            
                //  numberOfItems = ?             //  gaunam visu tinkamu irasu skaiciu db
            }
            else
            {
                if(filterPicker.SelectedIndex == 1)
                {
                    name = entry.Text;               // turime varda kintamajam "name", kuri vartotojas nurode kaip filtra

                    // get filtered by name:            currentPage = 1, t.y. norime gauti pirmus 10 irasu is db
                     
                    // historyListToShow = ?       //  gaunam 10 pirmu irasu is db kaip List<PersonHistory>
                    // numberOfItems = ?           //  gaunam visu tinkamu irasu skaiciu db
                }
            }

            // numberOfItems = 35;          // test

            if (numberOfItems > numberOfItems / 10 * 10)
            {
                numberOfPages = numberOfItems / 10 + 1;
            }
            else
            {
                numberOfPages = numberOfItems / 10;
            }

            if(numberOfPages > 1)
            {
                nextPageButton.IsVisible = true;
            }
            pageNumberLabel.IsVisible = true;

            /*          test
            PersonHistory testph1 = new PersonHistory();
            PersonHistory testph2 = new PersonHistory();
            PersonHistory testph3 = new PersonHistory();
            testph1.PersonID = 1;
            testph2.PersonID = 2;
            testph3.PersonID = 3;
            testph1.PersonName = "Jonas";
            testph2.PersonName = "Algis";
            testph3.PersonName = "Tomas";
            testph1.PersonDate = "1940";
            testph2.PersonDate = "2010";
            testph3.PersonDate = "1850";
            historyListToShow.Add(testph1);
            historyListToShow.Add(testph2);
            historyListToShow.Add(testph3);
            */

            BindingContext = historyListToShow;
        }

        void OnNextButtonClicked(object sender, EventArgs e)
        {
            currentPageNumber++;
            if(currentPageNumber == numberOfPages)
            {
                nextPageButton.IsVisible = false;
            }
            previousPageButton.IsVisible = true;

            GetHistoryPages();
        }

        void OnPreviousButtonClicked(object sender, EventArgs e)
        {
            currentPageNumber--;
            if(currentPageNumber == 1)
            {
                previousPageButton.IsVisible = false;
            }
            nextPageButton.IsVisible = true;

            GetHistoryPages();
        }

        async void GetHistoryPages()
        {
            if (filterPicker.SelectedIndex == 0)
            {
                // get with no filter:

                // current page turi reiksme, kuri atspindi, kuriuos elementus norime gauti:
                // jei currentPage = 1, tai 0-9, jei currentPage = 2, tai 10-19 ir t.t.
                //  numberOfItems keisti nebereikia, nes jis ir taip inicializuotas

                //  historyListToShow = ?         //  gaunam 1-10 irasu is db kaip List<PersonHistory>         

            }
            else
            {
                if (filterPicker.SelectedIndex == 1)
                {
                    name = entry.Text;               // turime varda, kuri vartotojas nurode kaip filtra

                    // get filtered by name:           

                    // currentPage turi reiksme, kuri atspindi, kuriuos elementus norime gauti:
                    // jei currentPage = 1, tai 0-9, jei currentPage = 2, tai 10-19 ir t.t.
                    //  numberOfItems keisti nebereikia, nes jis ir taip inicializuotas

                    // historyListToShow = ?       //  gaunam 1-10 irasu is db kaip List<PersonHistory>

                }
            }

            BindingContext = historyListToShow;
        } 

        void OnIDButtonClicked(object sender, EventArgs e)
        {
            var peopleInOrder = (historyListToShow.OrderBy(PersonHistory => PersonHistory.PersonID)).ToList();
            historyListToShow = peopleInOrder;
            BindingContext = historyListToShow;
        }

        void OnPersonNameButtonClicked(object sender, EventArgs e)
        {
            var peopleInOrder = (historyListToShow.OrderBy(PersonHistory => PersonHistory.PersonName)).ToList();
            historyListToShow = peopleInOrder;
            BindingContext = historyListToShow;
        }

        void OnPersonDateButtonClicked(object sender, EventArgs e)
        {
            var peopleInOrder = (historyListToShow.OrderBy(PersonHistory => PersonHistory.PersonDate)).ToList();
            historyListToShow = peopleInOrder;
            BindingContext = historyListToShow;
        }
    }
}
