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

namespace nightOwl
{
    public partial class LastSeenMapForm : Form
    {
        public LastSeenMapForm()
        {
            InitializeComponent();
        }

        private void gmap_Load(object sender, EventArgs e)
        {
            gmap.DragButton = MouseButtons.Left;
            gmap.MapProvider = BingHybridMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.SetPositionByKeywords("Vilnius, Lithuania");
            gmap.ShowCenter = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            MainForm.self.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            MainForm.closeMainForm();
        }
    }
}
