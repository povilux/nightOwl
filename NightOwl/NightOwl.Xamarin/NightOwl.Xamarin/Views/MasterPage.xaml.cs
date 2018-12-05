using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NightOwl.Xamarin.Components;
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

        public MasterPage()
        {
            InitializeComponent();
            SetItems();
        }

        void SetItems()
        {
            items = new List<MasterMenuItem>();
            items.Add(new MasterMenuItem("Video", "C:\\Users\\vidma\\Desktop", typeof(VideoRecognition)));
            items.Add(new MasterMenuItem("Picture", "C:\\Users\\vidma\\Desktop", typeof(PictureRecognition)));
            items.Add(new MasterMenuItem("Camera", "C:\\Users\\vidma\\Desktop", typeof(CameraRecognition)));
            items.Add(new MasterMenuItem("New Person", "C:\\Users\\vidma\\Desktop", typeof(ManagePerson)));
            items.Add(new MasterMenuItem("Map", "C:\\Users\\vidma\\Desktop", typeof(Map)));
            ListView.ItemsSource = items;
        }
    }
}
