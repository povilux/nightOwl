using NightOwl.WindowsForms.Components;
using NightOwl.WindowsForms.Exceptions;
using NightOwl.WindowsForms.Models;
using NightOwl.WindowsForms.Presenters;
using NightOwl.WindowsForms.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NightOwl.WindowsForms.Views
{
    public partial class AddPersonView : Form, IAddPersonView
    {
        private readonly AddPersonPresenter _presenter;

        private List<Face3> trainingData = new List<Face3>();
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        public AddPersonView(IPersonModel model)
        {
            InitializeComponent();
            _presenter = new AddPersonPresenter(this, model);

          

        }

        public async Task<string> RecognizeAsync(byte[] face)
        {
            try
            {
                IHttpClientService httpClient = HttpClientService.Instance;
                var name = await httpClient.PostAsync<string, byte[]>("http://localhost:54357/api/Faces/RecognizeFace/", face);
                Console.WriteLine("Name: " + name);
                return name;
            }
            catch (BadHttpRequestException ex)
            {
                Console.WriteLine("Erroras: " + ex);
                throw ex ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void SendAsync(Trainer trainer)
        {
            try
            {
                IHttpClientService httpClient = HttpClientService.Instance;
                await httpClient.PostAsync<Trainer, Trainer>("http://localhost:54357/api/Faces/Train/", trainer);
            }
            catch (BadHttpRequestException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
          

            public void ShowMessage(string message)
            {
                MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        private void BackButton_Click(object sender, EventArgs e) { BackButtonClicked(sender, e); }
        private void CloseButton_Click(object sender, EventArgs e) { CloseButtonClicked(sender, e); }
        private void CreateNewPersonDataButton_Click(object sender, EventArgs e) { CreateNewPersonDataButtonClicked(sender, e); }
        private void PersonsList_SelectedIndexChanged(object sender, EventArgs e) { PersonSelectedFromList(sender, e); }
        private void AddNewPersonButton_Click(object sender, EventArgs e) { NewPersonCreatingClicked(sender, e); }
        private void SelectPersonButton_Click(object sender, EventArgs e) { SelectPersonButtonClicked(sender, e); }
        private void UpdateInfoButton_Click(object sender, EventArgs e) { UpdatePersonCliked(sender, e); }

        public void AddPersonToList(Person item) { PersonsList.Items.Add(item); }
        public Person SelectedPerson { get { return (Person)PersonsList.SelectedItem; } }

        public string NameSurname { get { return NameField.Text; } set { NameField.Text = value; } }
        public DateTime BirthDate { get { return BirthDatePicker.Value; } set { BirthDatePicker.Value = value; } }
        public DateTime MissingDate { get { return MissingDatePicker.Value; } set { MissingDatePicker.Value = value; } }
        public string AdditionalInfo { get { return AdditionalInfoField.Text; } set { AdditionalInfoField.Text = value; } }
        public string SelectedPersonName { get { return PersonsList.SelectedItem.ToString(); } set { } }
        public int SelectedPersonIndex { get { return PersonsList.SelectedIndex; } set { } }
        public Image PersonImage { get { return PersonImageBox.Image; } set { PersonImageBox.Image = value; } }
        public bool NameSurnameEnabled { get { return NameField.Enabled; } set { NameField.Enabled = value; } }
        public bool PersonsListEnabled { get { return PersonsList.Visible; } set { PersonsList.Visible = value; } }
        public bool PersonsListTitle { get { return PersonsListLabel.Visible; } set { PersonsListLabel.Visible = value; } }
        public bool AddNewPersonBtnEnabled { get { return AddNewPersonButton.Visible; } set { AddNewPersonButton.Visible = value; } }
        public bool SelectPersonBtnEnabled { get { return SelectPersonButton.Visible; } set { SelectPersonButton.Visible = value; } }
        public bool UpdateInfoBtnEnabled { get { return UpdateInfoButton.Visible; } set { UpdateInfoButton.Visible = value; } }
        public bool CreateNewPersonDataBtnEnabled { get { return CreateNewPersonDataButton.Visible; } set { CreateNewPersonDataButton.Visible = true; } }

        public event EventHandler BackButtonClicked;
        public event EventHandler CloseButtonClicked;
        public event EventHandler CreateNewPersonDataButtonClicked;
        public event EventHandler PersonSelectedFromList;
        public event EventHandler NewPersonCreatingClicked;
        public event EventHandler UpdatePersonCliked;
        public event EventHandler SelectPersonButtonClicked;
        public event EventHandler AddPhotoButtonClicked;

        private void AddPhotoButton_Click(object sender, EventArgs e)
        {
            AddPhotoButtonClicked(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog browser = new OpenFileDialog
            {
                Filter = ConfigurationManager.AppSettings["BrowserFilterPhoto"],
                Title = Properties.Resources.BrowserTitle,
                Multiselect = true
            };

            if (browser.ShowDialog() == DialogResult.OK)
            {
                /*try
                {
                    byte[] array = { 1, 2, 3 };
                            var name = RecognizeAsync(array);

                            MessageBox.Show("Name: " + name.Result);
                            Console.WriteLine("Recognized");
                        
                    
                }
                catch (BadHttpRequestException ex)
                {
                    Console.WriteLine("Neteisinga uzklausa:" + ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERRORRRAS:" + ex);
                }*/

                foreach (var file in browser.FileNames.ToList())
                {
                    Bitmap imageIn = new Bitmap(file);
                    MemoryStream ms = new MemoryStream();
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                    /*trainingData.Add(new Face3
                    {
                        Photo = ms.ToArray(),
                        PersonName = textBox1.Text
                    });*/
                    try
                    {
                        RecognizeAsync(ms.ToArray());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Bad error: " + ex);
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var trainer = new Trainer()
            {
                Data = trainingData,
                NumOfComponents = trainingData.Count,
                Threshold = 3900
            };

            try
            {
                   SendAsync(trainer);
               // Face3 face = new Face3 { PersonName = " ", Photo = ms.ToArray() };
               // RecognizeAsync(face);
                //   MessageBox.Show("Result:" + result.Result);
            }
            catch (BadHttpRequestException ex)
            {
                Console.WriteLine("Neteisinga uzklausa:" + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRORRRAS:" + ex);
            }
        }
    }
}
