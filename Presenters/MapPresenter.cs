using GMap.NET.WindowsForms;
using nightOwl.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using nightOwl.Models;
using nightOwl.Data;

namespace nightOwl.Presenters
{
    class MapPresenter
    {
        private readonly IMapView _view;
        private readonly IPersonModel _model;
        private readonly IDataManagement _data;

        GMapOverlay markers = new GMapOverlay("markers");

        public MapPresenter(IMapView view, IPersonModel model)
        {
            _view = view;
            _model = model;
            _data = DataManagement.GetInstance();
            Initialize();
        }

        private void Initialize()
        {
            _view.BackButtonClicked += new EventHandler(OnBackButtonClicked);
            _view.CloseButtonClicked += new EventHandler(OnCloseButtonClicked);
            _view.PersonSelectedFromList += new EventHandler(OnPersonSelected);
            _view.MapLoaded += new EventHandler(OnMapLoaded);

            foreach (var person in _data.GetPersonsCatalog())
                _view.AddPersonToList(person.Name);
        }


        public void OnBackButtonClicked(object sender, EventArgs e)
        {
            _view.Close();
            FirstPageView.self.Show();
        }

        public void OnCloseButtonClicked(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void OnMapLoaded(object sender, EventArgs e) {
            _view.Map.DragButton = MouseButtons.Left;
            _view.Map.MapProvider = BingMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            _view.Map.SetPositionByKeywords("Vilnius, Lithuania"); //Perkelti "Vilnius, Lithuania" į strings skiltį
            _view.Map.ShowCenter = false;
        }

        public void OnPersonSelected(object sender, EventArgs e)
        {
            GMapMarker marker;
            if (_view.SelectedPersonIndex >= 0 && _view.SelectedPersonIndex < _data.GetPersonsCount())
            {
                _model.CurrentPerson = _view.SelectedPerson;
                _view.Map.Position = new PointLatLng(_model.CurrentPerson.CoordX, _model.CurrentPerson.CoordY);
                marker = new GMarkerGoogle(new PointLatLng(_model.CurrentPerson.CoordX, _model.CurrentPerson.CoordY), GMarkerGoogleType.blue_pushpin);
                marker.ToolTipText = _model.CurrentPerson.Name + "\n" + _model.CurrentPerson.BirthDate + "\n" + _model.CurrentPerson.MissingDate + "\n" + _model.CurrentPerson.AdditionalInfo;
                markers.Markers.Add(marker);
                _view.Map.Overlays.Add(markers);
            }
            marker = null;
        }

    }
}
