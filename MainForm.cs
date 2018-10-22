using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Face;

namespace nightOwl
{
    public partial class MainForm : Form
    {
        public static List<Person> persons = new List<Person>();

        public static MainForm self;
        public MainForm()
        {
            InitializeComponent();
            self = this;

            if(!Directory.Exists(Application.StartupPath + "/data"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/data");
            }

            if (!File.Exists(Application.StartupPath + "/data/names.txt"))
            {
                var newFile = File.Create(Application.StartupPath + "/data/names.txt");
                newFile.Close();
            }

            string line;
            string[] splitedLine;

            using (StreamReader sr = new StreamReader(Application.StartupPath + "/data/names.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    splitedLine = line.Split("|".ToCharArray(), StringSplitOptions.None);   
                    persons.Add(new Person(splitedLine[0], splitedLine[1], splitedLine[2], coordx:Convert.ToDouble(splitedLine[3]), coordy:Convert.ToDouble(splitedLine[4]), lastseendate: splitedLine[5], add:splitedLine[6]));
                }
            }
         
        }

        public static void closeMainForm()
        {
            ImageHandler.WriteDataToFile(persons);
            MainForm.self.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VideoPlayerForm firstForm = new VideoPlayerForm();
            firstForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WebcamForm thirdForm = new WebcamForm();
            thirdForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TrainerForm secondForm = new TrainerForm();
            secondForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LastSeenMapForm lsmForm = new LastSeenMapForm();
            lsmForm.Show();
            this.Hide();
        }
    }
}
