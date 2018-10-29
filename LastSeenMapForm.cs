using System;
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
using nightOwl.Views;

namespace nightOwl
{
    public partial class LastSeenMapForm : Form
    {
        GMapOverlay markers = new GMapOverlay("markers");
        bool personSelected = false;

        public LastSeenMapForm()
        {
            InitializeComponent();
            var personsDataQuery = from p in FirstPageView.persons select new { p.Name };
            foreach (var person in personsDataQuery)
                listBox1.Items.Add(person.Name);
        }

        private void gmap_Load(object sender, EventArgs e)
        {
            gmap.DragButton = MouseButtons.Left;
            gmap.MapProvider = BingMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.SetPositionByKeywords("Vilnius, Lithuania");
            gmap.ShowCenter = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            FirstPageView.self.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            FirstPageView.CloseMainForm();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedItem = listBox1.SelectedIndex;
            GMapMarker marker;
            if (selectedItem >= 0 && selectedItem < FirstPageView.persons.Count)
            {
                personSelected = true;
                string chosenName = listBox1.GetItemText(listBox1.SelectedItem);
                Person person = FirstPageView.persons.Where(p => String.Equals(p.Name, chosenName)).First();
                gmap.Position = new PointLatLng(54.733521, 25.260810);
                marker = new GMarkerGoogle(new PointLatLng(54.733521, 25.260810), GMarkerGoogleType.blue_pushpin);
                marker.ToolTipText = chosenName + "\n" + person.BirthDate + "\n" + person.MissingDate + "\n" + person.AdditionalInfo;
                markers.Markers.Add(marker);
                gmap.Overlays.Add(markers);
            }

            marker = null;
        }
    }
}
