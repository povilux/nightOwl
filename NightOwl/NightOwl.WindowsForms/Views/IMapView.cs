using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using NightOwl.WindowsForms.Components;

namespace NightOwl.WindowsForms.Views
{
    interface IMapView
    {
        void AddPersonToList(string item);
        void Close();

        int SelectedPersonIndex { get; set; }
        GMapControl Map { get; }
        Person SelectedPerson { get; } 

        event EventHandler MapLoaded;
        event EventHandler BackButtonClicked;
        event EventHandler CloseButtonClicked;
        event EventHandler PersonSelectedFromList;
        
    }
}
