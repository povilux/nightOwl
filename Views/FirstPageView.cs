using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using nightOwl.Models;
using nightOwl.Properties;

namespace nightOwl.Views
{
    public partial class FirstPageView : Form
    {
        public static List<Person> persons = new List<Person>();

        public static FirstPageView self;

        public FirstPageView()
        {
            InitializeComponent();
            self = this;

            // to do: put to DataManagement class
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
            // to do end
        }

        public static void CloseMainForm()
        {
            ImageHandler.WriteDataToFile(persons); // to do: data management class
            Application.Exit();
        }

        private void SelectVideoButton_Click(object sender, EventArgs e)
        {
         //   Close();
            VideoPlayerForm firstForm = new VideoPlayerForm();
            firstForm.Show();
            this.Hide();
        }

        private void WatchCameraButton_Click(object sender, EventArgs e)
        {
            //Close();
            WebcamForm thirdForm = new WebcamForm();
            thirdForm.Show();
            this.Hide();
        }

        private void ShowMapButton_Click(object sender, EventArgs e)
        {
            //Close();
            LastSeenMapForm lsmForm = new LastSeenMapForm();
            lsmForm.Show();
            this.Hide();
        }

        private void AddPersonButton_Click(object sender, EventArgs e)
        { 
            new AddPersonView(new PersonModel()).Show();
            this.Hide();
        }

        private void FirstPageView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.WindowLocation = this.Location;

            if (this.WindowState == FormWindowState.Normal)
                Settings.Default.WindowSize = this.Size;
            else
                Settings.Default.WindowSize = this.RestoreBounds.Size;
   
            Settings.Default.Save();
        }

        private void FirstPageView_Load(object sender, EventArgs e)
        {
            this.Size = Settings.Default.WindowSize;
            this.Location = Settings.Default.WindowLocation;
        }
    }
}
