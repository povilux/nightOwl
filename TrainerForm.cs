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
using System.Globalization;
using System.Text.RegularExpressions;
using nightOwl.Views;

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
            pictureBox2.Image = nightOwl.Properties.Resources.Capone_dark;
            nameField.Enabled = false;

            createNewPersonDataButton.Hide();
            updateInfoButton.Show();

            foreach (var person in FirstPageView.persons)
                listBox1.Items.Add(person.Name);
        }

 
        private void addNewPersonButton_Click(object sender, EventArgs e)
        {
            if (personSelected) // pasirinktas žmogus, tai panaikiname pasirinkimą.
            {
                personSelected = false;

                listBox1.Hide();
                listBoxLabel.Hide();

                pictureBox2.Image = nightOwl.Properties.Resources.NewPerson;
            
                nameField.Text = "";
                missingDateField.Text = "";
                birthDateField.Text = "";
                additionalInfoField.Text = "";

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

                pictureBox2.Image = nightOwl.Properties.Resources.Capone_dark;
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
                string birthDate = birthDateField.Text;
                string missingDate = missingDateField.Text;
                string additionalInfo = additionalInfoField.Text;

                DateTime dt;
                Regex regx = new Regex(@"^[\p{L}\p{M}' \.\-]+$");
              
               if (regx.IsMatch(newName) && DateTime.TryParseExact(birthDate, "yyyy-MM-dd", new CultureInfo("lt-LT"), DateTimeStyles.None, out dt) &&
                       DateTime.TryParseExact(missingDate, "yyyy-MM-dd hh:mm", new CultureInfo("lt-LT"), DateTimeStyles.AssumeLocal, out dt))
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
                        FirstPageView.persons.Add(new Person(newName, birthDate, missingDate, additionalInfo));
                      //  ImageHandler.WriteDataToFile(FirstPageView.persons);

                        MessageBox.Show(String.Format("{0} was added to database. ({1}/{2} pics was suitable.)", newName, viablePicsCount, picFilenames.Count));

                        pictureBox2.Image = nightOwl.Properties.Resources.NewPerson;
                        nameField.Text = "";
                        missingDateField.Text = "";
                        birthDateField.Text = "";
                        additionalInfoField.Text = "";
                       
                        nameField.Enabled = true;
                    }
                    else
                        MessageBox.Show("No good photos for face recognition!");  
                }
                else
                    MessageBox.Show("Please insert correct information!");

            }
            else
                MessageBox.Show("Please add some photos!");
        }

        private void updateInfoButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < FirstPageView.persons.Count())
            {
                string chosenName = listBox1.GetItemText(listBox1.SelectedItem);

                var person = FirstPageView.persons.Where(p => String.Equals(p.Name, chosenName)).First();

                DateTime dt;
                if (!DateTime.TryParseExact(birthDateField.Text, "yyyy-MM-dd", new CultureInfo("lt-LT"), DateTimeStyles.None, out dt) ||
                    !DateTime.TryParseExact(missingDateField.Text, "yyyy-MM-dd hh:mm", new CultureInfo("lt-LT"), DateTimeStyles.AssumeLocal, out dt))
                   {
                    MessageBox.Show("The missing or birth date is not correct!");
                }
                else
                {
                    person.BirthDate = birthDateField.Text;
                    person.MissingDate = missingDateField.Text;
                    person.AdditionalInfo = additionalInfoField.Text;

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
                                ImageHandler.SaveGrayFacetoFile(chosenName, grayFace);

                                viablePicsCount++;
                            }
                        }
                        updateMessage = String.Format("{0}/{1} pics was added, information was updated", viablePicsCount, picFilenames.Count);
                    }
                    MessageBox.Show(updateMessage);
                }
            }
            else
                MessageBox.Show("Select person from the list.");
        }
                     
      
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedItem = listBox1.SelectedIndex;

            if (selectedItem >= 0 && selectedItem < FirstPageView.persons.Count())
            {
                personSelected = true;

                nameField.Text = listBox1.GetItemText(listBox1.SelectedItem);              
                string chosenName = nameField.Text;

                Person person = FirstPageView.persons.Where(p => String.Equals(p.Name, chosenName)).First();
             
                birthDateField.Text = person.BirthDate;
                missingDateField.Text = person.MissingDate;
                additionalInfoField.Text = person.AdditionalInfo;               
            
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
            FirstPageView.CloseMainForm();
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
            FirstPageView.self.Show();
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
