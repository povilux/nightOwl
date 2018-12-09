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
	public partial class HistoryPage : ContentPage
	{
        private string name;
        private int currentPageNumber = 1;
        private int numberOfPages = 0;
        private int numberOfItems = 0;
        private List<PersonHistory> historyListToShow;

        private IPersonHistoryService _PersonHistoryService;
        
        public HistoryPage()
		{
			InitializeComponent();
            filterPicker.SelectedIndex = 0;
            entry.IsVisible = false;
            pageNumberLabel.IsVisible = false;
            previousPageButton.IsVisible = false;
            nextPageButton.IsVisible = false;

            personIDButton.IsVisible = false;
            personNameButton.IsVisible = false;
            personDateButton.IsVisible = false;

            listToShow.IsVisible = false;

            filterPicker.SelectedIndexChanged += FilterPickerIndexChanged;
            historyListToShow = new List<PersonHistory>();

            _PersonHistoryService = new PersonHistoryService();

            listToShow.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                PersonHistory selectedPerson = (PersonHistory)e.SelectedItem;

                if (!string.IsNullOrEmpty(selectedPerson.SourceFaceUrl))
                {
                    imageToShow.IsVisible = true;

                    imageToShow.Source = ImageSource.FromUri(new Uri(selectedPerson.SourceFaceUrl));
                }
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

        async Task GetHistoryPages()
        {
            personIDButton.IsVisible = true;
            personNameButton.IsVisible = true;
            personDateButton.IsVisible = true;

            if (filterPicker.SelectedIndex == 0)
            {
                var actionGetHistory = await _PersonHistoryService.GetPersonsHistoryList(currentPageNumber - 1);

                if (actionGetHistory.Success)
                {
                    historyListToShow = actionGetHistory.Message.PersonHistories.ToList();
                    numberOfItems = actionGetHistory.Message.PersonHistoriesCount;
                }
                else
                {
                    await DisplayAlert("asd", actionGetHistory.Error, "asd");
                }
            }
            else if (filterPicker.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(entry.Text))
                {
                    await DisplayAlert("Error", "No search value", "Close");
                    return;
                }

                name = entry.Text;
                var actionGetHistory = await _PersonHistoryService.GetPersonHistoryByName(name, currentPageNumber - 1);

                if (actionGetHistory.Success)
                {
                    historyListToShow = actionGetHistory.Message.PersonHistories.ToList();
                    numberOfItems = actionGetHistory.Message.PersonHistoriesCount;
                }
                else
                {
                    await DisplayAlert("Error", actionGetHistory.Error, "Error");
                }
            }
        }

        async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            numberOfItems = 0;
            imageToShow.Source = null;
            listToShow.IsVisible = true;

            await GetHistoryPages();
 
            if (numberOfItems > numberOfItems / 10 * 10)
            {
                numberOfPages = numberOfItems / 10 + 1;
            }
            else
            {
                numberOfPages = numberOfItems / 10;
            }

            previousPageButton.IsVisible = false;
            nextPageButton.IsVisible = false;
            if(numberOfPages > 1)
            {
                nextPageButton.IsVisible = true;
            }
            pageNumberLabel.Text = currentPageNumber.ToString();
            pageNumberLabel.IsVisible = true;

            BindingContext = historyListToShow;
        }

        async void OnNextButtonClicked(object sender, EventArgs e)
        {
            currentPageNumber++;
            pageNumberLabel.Text = currentPageNumber.ToString();
            if (currentPageNumber == numberOfPages)
            {
                nextPageButton.IsVisible = false;
            }
            previousPageButton.IsVisible = true;

            await GetHistoryPages();
            BindingContext = historyListToShow;

        }

        async void OnPreviousButtonClicked(object sender, EventArgs e)
        {
            currentPageNumber--;
            pageNumberLabel.Text = currentPageNumber.ToString();

            if (currentPageNumber == 1)
            {
                previousPageButton.IsVisible = false;
            }
            nextPageButton.IsVisible = true;

            await GetHistoryPages();
            BindingContext = historyListToShow;

        }



        void OnIDButtonClicked(object sender, EventArgs e)
        {
            var peopleInOrder = (historyListToShow.OrderBy(PersonHistory => PersonHistory.PersonId)).ToList();
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
            var peopleInOrder = (historyListToShow.OrderBy(PersonHistory => PersonHistory.Date)).ToList();
            historyListToShow = peopleInOrder;
            BindingContext = historyListToShow;
        }
    }
}
