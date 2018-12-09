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
                new MasterMenuItem("Video recognition", "C:\\Users\\vidma\\Desktop", typeof(VideoRecognition)),
                new MasterMenuItem("Picture recognition", "C:\\Users\\vidma\\Desktop", typeof(PictureRecognition)),
                //items.Add(new MasterMenuItem("Camera", "C:\\Users\\vidma\\Desktop", typeof(CameraRecognition)));
                new MasterMenuItem("Persons management", "C:\\Users\\vidma\\Desktop", typeof(AddPerson)),
                new MasterMenuItem("Recognition history", "C:\\Users\\vidma\\Desktop", typeof(Map))
            };
            ListView.ItemsSource = items;
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            LogoutClicked?.Invoke(sender, e);
        }
    }
}
