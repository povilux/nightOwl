using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nightOwl
{

    public partial class PictureForm : Form
    {
        public PictureForm()
        {
            InitializeComponent();
        }

        private void BackToCamButton_Click(object sender, EventArgs e)
        {
            WebcamForm camForm = new WebcamForm();
            camForm.Show();
            Close();
        }

        private void BackToMainButton_Click(object sender, EventArgs e)
        {
            MainForm.self.Show();
            Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

        }
    }
}
