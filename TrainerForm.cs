using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Face;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nightOwl
{
    public partial class TrainerForm : Form
    {
        bool picSelected = false;
        bool personSelected = false;
        Image<Bgr, byte> tempImage;
        List<string> picFilenames;

        public TrainerForm()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            textBox1.Text = "Add new person's face to database or train an existing one";
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "/Capone.bmp");
            pictureBox2.Image = Image.FromFile(Application.StartupPath + "/Capone_dark.bmp");
            pictureBox3.Image = Image.FromFile(Application.StartupPath + "/rightArrow.bmp");
            listBox1.Hide();
            button3.Text = "";
            button3.Enabled = false;
            textBox2.Hide();
            foreach(string name in MainForm.names)
            {
                listBox1.Items.Add(name);
            }
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

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"D:\";
            openFileDialog1.Title = "Open picture location";
            openFileDialog1.Filter = "Image Files (*.bmp, *.jpg| *.bmp;*.jpg";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picSelected = true;
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "/NewPerson.bmp");
                pictureBox1.Image = new Bitmap(openFileDialog1.FileNames[0]);
                tempImage = new Image<Bgr, byte>(openFileDialog1.FileNames[0]);
                textBox2.Show();
                /*
                if (ImageHandler.GetFaceFromImage(tempImage) == null)
                {
                    textBox2.Text = "Picture is not suitable for face recognition";
                }
                else
                {
                    textBox2.Text = "Write new name here";
                    textBox2.Enabled = true;
                    listBox1.Show();
                }
                */
                textBox2.Text = "Write new name here";
                textBox2.Enabled = true;
                listBox1.Show();
                picFilenames = openFileDialog1.FileNames.ToList();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            int viablePicsCount = 0;
            int notViablePicsCount = 0;

            if ((picSelected == true) && (personSelected == false))
            {
                picSelected = false;
                personSelected = false;
                string newName = textBox2.Text;
                if (!File.Exists(Application.StartupPath + "/data/" + newName + "/rep.bmp"))
                {
                    ImageHandler.SaveRepresentativePic(tempImage.ToBitmap(), newName);
                }
                foreach(string filename in picFilenames)
                {
                    tempImage = new Image<Bgr, byte>(filename);
                    if(ImageHandler.GetFaceFromImage(tempImage) == null)
                    {
                        notViablePicsCount++;
                    } else
                    {
                        var newFace = ImageHandler.GetFaceFromImage(tempImage);
                        var newGrayFace = newFace.Convert<Gray, Byte>();
                        ImageHandler.SaveGrayFacetoFile(newName, newGrayFace);
                        viablePicsCount++;
                    }
                    
                }
                
                if(viablePicsCount > 0)
                {
                    listBox1.Items.Add(newName);
                    MainForm.names.Add(newName);
                    ImageHandler.WriteNamesToFile(MainForm.names);
                    textBox2.Text = String.Format("{0} was added to database. ({1}/{2} pics passed)", newName, viablePicsCount,
                        picFilenames.Count);
                } else
                {
                    textBox2.Text = "All pics were unsuitable. New person was not created.";
                }
                

            } else if((picSelected == true) && (personSelected == true))
            {
                picSelected = false;
                personSelected = false;

                foreach (string filename in picFilenames)
                {
                    tempImage = new Image<Bgr, byte>(filename);
                    if (ImageHandler.GetFaceFromImage(tempImage) == null)
                    {
                        notViablePicsCount++;
                    }
                    else
                    {
                        var face = ImageHandler.GetFaceFromImage(tempImage);
                        var grayFace = face.Convert<Gray, Byte>();
                        ImageHandler.SaveGrayFacetoFile(listBox1.GetItemText(listBox1.SelectedItem), grayFace);
                        viablePicsCount++;
                    }
                }

                textBox2.Text = String.Format("{0}/{1} pics were added", viablePicsCount, picFilenames.Count);

                /*
                // save coloured face version to file
                Image<Bgr, Byte> newFace = ImageHandler.GetFaceFromImage(tempImage);            
                ImageHandler.SaveFacetoFile(listBox1.GetItemText(listBox1.SelectedItem), newFace);

                // save gray face version to file
                /*
                Image<Gray, Byte> newGrayFace = newFace.Convert<Gray, Byte>();                  
                ImageHandler.SaveGrayFacetoFile(listBox1.GetItemText(listBox1.SelectedItem), newGrayFace);
                */

                // which one is better ?
                
            }
            button3.Text = "";
            button3.Enabled = false;
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "/Capone.bmp");
            pictureBox2.Image = Image.FromFile(Application.StartupPath + "/Capone_dark.bmp");
            listBox1.Hide();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            personSelected = true;
            button3.Text = "Add new pic";
            string chosenName = listBox1.GetItemText(listBox1.SelectedItem);
            pictureBox2.Image = ImageHandler.LoadRepresentativePic(chosenName);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((textBox2.Text != "") && (!MainForm.names.Contains(textBox2.Text))&&(picSelected == true))
            {
                button3.Text = "Add new person";
                button3.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EigenFaceRecognizer eigen = Recognizer.NewEigen();
            bool success = Recognizer.TrainRecognizer(eigen, ImageHandler.GetGrayFaceArrayFromFiles(),ImageHandler.GetLabelArrayFromFiles());
            if(success == false)
            {
                MessageBox.Show("Corrupted data");
            }
        }
    }
}
