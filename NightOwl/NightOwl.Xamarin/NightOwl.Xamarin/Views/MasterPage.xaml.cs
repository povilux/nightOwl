﻿using System;
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
            items.Add(new MasterMenuItem("Video", "Images\\video.png", typeof(VideoRecognition)));
            items.Add(new MasterMenuItem("Picture", "Images\\photo.png", typeof(PictureRecognition)));
            items.Add(new MasterMenuItem("Camera", "Images\\camera.png", typeof(CameraRecognition)));
            items.Add(new MasterMenuItem("Manage people", "Images\\plus.png", typeof(ManagePage)));
            items.Add(new MasterMenuItem("Map", "Images\\map.png", typeof(Map)));
            ListView.ItemsSource = items;
        }
    }
}
