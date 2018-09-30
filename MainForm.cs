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
        public static List<String> names = new List<String>();
        //public static List<Byte[]> photos = new List<Byte[]>();

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

            // read names from file to List<String>
            using (StreamReader sr = new StreamReader(Application.StartupPath + "/data/names.txt"))
            {
                while(sr.Peek() >= 0)
                {
                    names.Add(sr.ReadLine());
                }
            }

            
            // read binary data to List<Byte[]>
            /*
            foreach(string name in names)
            {
                if(File.Exists(Application.StartupPath + "/data/" + name + ".txt"))
                    {
                    
                        using (FileStream fs = File.OpenRead(Application.StartupPath + "/data/" + name + ".txt"))
                        {
                            BinaryReader br = new BinaryReader(fs);
                            br.Read();
                        }
                    
                        File.ReadAllBytes(Application.StartupPath + "/data/" + name + ".txt");
                    }
                
            }
            */

        }

        public static void closeMainForm()
        {
            ImageHandler.WriteNamesToFile(names);
            /*
            foreach (string name in names)
            {
                using (StreamWriter sw = new StreamWriter(Application.StartupPath + "/data/names.txt"))
                {
                    sw.WriteLine(name);
                }
                if(photos.Count == names.Count)
                {
                    using (BinaryWriter br =
                    new BinaryWriter(File.Open(Application.StartupPath + "/data/" + name + ".txt", FileMode.Create)))
                    {
                        int index = names.IndexOf("name");
                        br.Write(photos.ElementAt(index));
                    }
                }
            }
            */
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
    }
}
