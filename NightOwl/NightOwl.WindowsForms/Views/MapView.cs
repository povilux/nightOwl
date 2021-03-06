﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using NightOwl.WindowsForms.Presenters;
using NightOwl.WindowsForms.Models;
using NightOwl.WindowsForms.Components;

namespace NightOwl.WindowsForms.Views
{
    public partial class MapView : Form, IMapView
    {
        private readonly MapPresenter _presenter;
        private readonly IPersonModel _model;

        public MapView(IPersonModel model)
        {
            InitializeComponent();
            _model = model;
            _presenter = new MapPresenter(this, _model);
        }

        private void gmap_Load(object sender, EventArgs e){ MapLoaded(sender, e); }
        private void BackButton_Click(object sender, EventArgs e) { BackButtonClicked(sender, e); }
        private void CloseButton_Click(object sender, EventArgs e) { CloseButtonClicked(sender, e); }
        private void PeoplesList_SelectedIndexChanged(object sender, EventArgs e){ PersonSelectedFromList(sender, e); }

        public Person SelectedPerson { get { return (Person)PeoplesList.SelectedItem; } }
        public int SelectedPersonIndex { get { return PeoplesList.SelectedIndex; } set { } }
        public void AddPersonToList(string item) { PeoplesList.Items.Add(item); }
        public GMapControl Map { get { return gmap; } }

        public event EventHandler BackButtonClicked;
        public event EventHandler CloseButtonClicked;
        public event EventHandler PersonSelectedFromList;
        public event EventHandler MapLoaded;
    }
}
