using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NightOwl.Xamarin.Components;
using NightOwl.Xamarin.Services;
using NightOwl.Xamarin.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NightOwl.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listview; } }
        public List<MasterMenuItem> items;

        public delegate void LogoutEventHandler(object sender, EventArgs args);
        public event LogoutEventHandler LogoutClicked;

        public MasterPage()
        {
            InitializeComponent();
            SetItems();
        }

        void SetItems()
        {
            items = new List<MasterMenuItem>
            {
                new MasterMenuItem("Video recognition", "Images\\video.png", typeof(VideoRecognition)),
                new MasterMenuItem("Picture recognition", "Images\\photo.png", typeof(PictureRecognition)),
                new MasterMenuItem("Persons management", "Images\\plus.png", typeof(ManagePage)),
                new MasterMenuItem("History", "Images\\history.png", typeof(HistoryPage))
        };
            ListView.ItemsSource = items;
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            LogoutClicked?.Invoke(sender, e);
        }
    }
}
