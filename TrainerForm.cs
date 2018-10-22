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
        bool personSelected = true;
        Image<Bgr, byte> tempImage;
        List<string> picFilenames;

        public TrainerForm()
        {
            InitializeComponent();

            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = Image.FromFile(Application.StartupPath + "/Capone_dark.bmp");
            nameField.Enabled = false;

            createNewPersonDataButton.Hide();
            updateInfoButton.Show();

            var personsDataQuery = from name in MainForm.names select name;

            foreach (var name in personsDataQuery)
                listBox1.Items.Add(name);
        }

 
        private void addNewPersonButton_Click(object sender, EventArgs e)
        {
            if (personSelected) // pasirinktas žmogus, tai panaikiname pasirinkimą.
            {
                personSelected = false;

                listBox1.Hide();
                listBoxLabel.Hide();

                pictureBox2.Image = Image.FromFile(Application.StartupPath + "/NewPerson.bmp");
                nameField.Text = "";
                nameField.Enabled = true;

                addNewPersonButton.Text = "Select person from the list";
                addNewPersonButton.Show();

                updateInfoButton.Hide();
                createNewPersonDataButton.Show();
            }
            else // nepasirinktas žmogus, todėl atidarome pasirinkimo sąrašą
            {
                personSelected = true;

                listBox1.Show();
                listBoxLabel.Show();

                pictureBox2.Image = Image.FromFile(Application.StartupPath + "/Capone_dark.bmp");
                nameField.Enabled = false;

                addNewPersonButton.Text = "Add new person";
                addNewPersonButton.Show();

                updateInfoButton.Show();
                createNewPersonDataButton.Hide();
            }
        }
        private void createNewPersonDataButton_Click(object sender, EventArgs e)
        {
            int viablePicsCount = 0;
            int notViablePicsCount = 0;

            if (picSelected)
            {
                string newName = nameField.Text;

                if (!String.IsNullOrWhiteSpace(newName))
                {
                    picSelected = false;

                    string directory = newName;
                    directory = directory.Replace(" ", "_");

                    if (!File.Exists(Application.StartupPath + "/data/" + directory + "/rep.bmp"))
                        ImageHandler.SaveRepresentativePic(tempImage.ToBitmap(), directory);
                    
                    foreach (string filename in picFilenames)
                    {
                        tempImage = new Image<Bgr, byte>(filename);

                        if (ImageHandler.GetFaceFromImage(tempImage) == null)
                            notViablePicsCount++;
                        else
                        {
                            var newFace = ImageHandler.GetFaceFromImage(tempImage);
                            var newGrayFace = newFace.Convert<Gray, Byte>();
                            ImageHandler.SaveGrayFacetoFile(directory, newGrayFace);
                            viablePicsCount++;
                        }
                    }

                    if (viablePicsCount > 0)
                    {
                        listBox1.Items.Add(newName);
                        MainForm.names.Add(newName);
                        ImageHandler.WriteNamesToFile(MainForm.names);

                        MessageBox.Show(String.Format("{0} was added to database. ({1}/{2} pics was suitable.)", newName, viablePicsCount, picFilenames.Count));

                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "/NewPerson.bmp");
                        nameField.Text = "";
                        nameField.Enabled = true;
                    }
                    else
                        MessageBox.Show("No good photos for face recognition!");  
                }
                else
                    MessageBox.Show("Please insert name and surname!");

            }
            else
                MessageBox.Show("Please add some photos!");
        }

        private void updateInfoButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < MainForm.names.Count)
            {
                string updateMessage = "Information updated!";

                if (picSelected)
                {
                    int viablePicsCount = 0;
                    int notViablePicsCount = 0;

                    picSelected = false;

                    foreach (string filename in picFilenames)
                    {
                        tempImage = new Image<Bgr, byte>(filename);

                        if (ImageHandler.GetFaceFromImage(tempImage) == null)
                            notViablePicsCount++;
                        else
                        {
                            var face = ImageHandler.GetFaceFromImage(tempImage);
                            var grayFace = face.Convert<Gray, Byte>();
                            ImageHandler.SaveGrayFacetoFile(listBox1.GetItemText(listBox1.SelectedItem), grayFace);

                            viablePicsCount++;
                        }
                    }
                    updateMessage = String.Format("{0}/{1} pics was added, information was updated", viablePicsCount, picFilenames.Count);
                }
                MessageBox.Show(updateMessage);
            }
            else
                MessageBox.Show("Select person from the list.");
        }
                     
      
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedItem = listBox1.SelectedIndex;

            if (selectedItem >= 0 && selectedItem < MainForm.names.Count)
            {
                personSelected = true;

                nameField.Text = listBox1.GetItemText(listBox1.SelectedItem);
                string chosenName = nameField.Text;
                chosenName = chosenName.Replace(" ", "_");

                pictureBox2.Image = ImageHandler.LoadRepresentativePic(chosenName);
            }
        }
        private void addPhotoButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"D:\";
            openFileDialog1.Title = "Open picture location";
            openFileDialog1.Filter = "Image Files (*.bmp, *.jpg| *.bmp;*.jpg";
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picSelected = true;
                pictureBox2.Image = new Bitmap(openFileDialog1.FileNames[0]);
                tempImage = new Image<Bgr, byte>(openFileDialog1.FileNames[0]);
                picFilenames = openFileDialog1.FileNames.ToList();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
            MainForm.closeMainForm();
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
            MainForm.self.Show();
        }
        private void TrainerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            EigenFaceRecognizer eigen = Recognizer.NewEigen();
            bool success = Recognizer.TrainRecognizer(eigen, ImageHandler.GetGrayFaceArrayFromFiles(), ImageHandler.GetLabelArrayFromFiles());

            if (success == false)
                MessageBox.Show("Corrupted data");
        }
    }
}
