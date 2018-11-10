﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using nightOwl.Data;
using nightOwl.Models;
using nightOwl.Views;
using System.Configuration;
using Emgu.CV.Face;
using nightOwl.Components;

namespace nightOwl.Presenters
{
    public class AddPersonPresenter
    {
        bool picSelected = false;
        Image<Bgr, byte> tempImage;
        List<string> picFilenames;

        private readonly IAddPersonView _view;
        private readonly IPersonModel _model;
        private readonly IDataManagement _data;


        // ATEICIAI: datos kol kas saugomos kaip string (nors is view gaunamos kaip datetime). Galbut galima saugoti kaip datetime

        public AddPersonPresenter(IAddPersonView view, IPersonModel model)
        {
            _view = view;
            _model = model;
            _data = DataManagement.Instance;
            Initialize();
        }

        private void Initialize()
        {
            _view.BackButtonClicked += new EventHandler(OnBackButtonClicked);
            _view.CloseButtonClicked += new EventHandler(OnCloseButtonClicked);
            _view.CreateNewPersonDataButtonClicked += new EventHandler(OnCreateNewPersonClicked);
            _view.PersonSelectedFromList += new EventHandler(OnPersonSelected);
            _view.NewPersonCreatingClicked += new EventHandler(OnNewPersonCreatingClicked);
            _view.UpdatePersonCliked += new EventHandler(OnUpdatePersonCliked);
            _view.SelectPersonButtonClicked += new EventHandler(OnSelectPersonButtonClicked);
            _view.AddPhotoButtonClicked += new EventHandler(OnAddPhotoButtonCicked);

            // Make the appropriate comparer.
            PersonComparer pc = new PersonComparer
            {
                SortBy = PersonComparer.CompareField.BirthDate
            };

            List<Person> SortedList = _data.GetPersonsCatalog().ToList();
            SortedList.Sort(pc);
       
            foreach (var person in SortedList)
                _view.AddPersonToList(person);
        }

        public void OnBackButtonClicked(object sender, EventArgs e)
        {
            _view.Close();

            if (!Recognizer.TrainRecognizer())
                Console.WriteLine(Properties.Resources.ErrorWhileTrainingRecognizer);

            FirstPageView.self.Show();
        }
        public void OnCloseButtonClicked(object sender, EventArgs e)
        {
            if (!Recognizer.TrainRecognizer())
                Console.WriteLine(Properties.Resources.ErrorWhileTrainingRecognizer);

            Application.Exit();
        }

        public void OnPersonSelected(object sender, EventArgs e)
        {
            _model.GroupPersonsByCreator();

            if (_view.SelectedPersonIndex >= 0 && _view.SelectedPersonIndex < _data.GetPersonsCount())
            {
                _model.CurrentPerson = _view.SelectedPerson;
                _view.NameSurname = _model.CurrentPerson.Name;
                _view.BirthDate = DateTime.Parse(_model.CurrentPerson.BirthDate);
                _view.MissingDate = DateTime.Parse(_model.CurrentPerson.MissingDate);
                _view.AdditionalInfo = _model.CurrentPerson.AdditionalInfo;

                // to do: something...
                string chosenName = _model.CurrentPerson.Name;
                chosenName = chosenName.Replace(" ", "_");
                _view.PersonImage = ImageHandler.LoadRepresentativePic(chosenName);
            }
        }

   

        public void OnAddPhotoButtonCicked(object sender, EventArgs e)
        {
            OpenFileDialog browser = new OpenFileDialog
            {
                Filter = ConfigurationManager.AppSettings["BrowserFilterPhoto"],
                Title = Properties.Resources.BrowserTitle,
                Multiselect = true
            };

            if (browser.ShowDialog() == DialogResult.OK)
            {
                // to do: do something about this... separate to another class
                picSelected = true;
                _view.PersonImage = new Bitmap(browser.FileNames[0]);
                tempImage = new Image<Bgr, byte>(browser.FileNames[0]);
                picFilenames = browser.FileNames.ToList();
            }
        }

        public void OnSelectPersonButtonClicked(object sender, EventArgs e)
        {
            _view.PersonsListEnabled = true;
            _view.PersonsListTitle = true;
            _view.PersonImage = Properties.Resources.Capone_dark;
            _view.NameSurnameEnabled = false;

            _view.AddNewPersonBtnEnabled = true;
            _view.SelectPersonBtnEnabled = false;
 
            _view.UpdateInfoBtnEnabled = true;
            _view.CreateNewPersonDataBtnEnabled = false;
        }

        public void OnNewPersonCreatingClicked(object sender, EventArgs e)
        {
            _view.PersonsListEnabled = false;
            _view.PersonsListTitle = false;

            _view.PersonImage = Properties.Resources.NewPerson;

            _view.NameSurname = "";
            _view.NameSurnameEnabled = true;

            _view.MissingDate = DateTime.Today;
            _view.BirthDate = DateTime.Today;
            _view.AdditionalInfo = "";

            _view.AddNewPersonBtnEnabled = false;
            _view.SelectPersonBtnEnabled = true;

            _view.UpdateInfoBtnEnabled = false;
            _view.CreateNewPersonDataBtnEnabled = true;          
        }

        public void OnUpdatePersonCliked(object sender, EventArgs e)
        {
            if (_view.SelectedPersonIndex >= 0 && _view.SelectedPersonIndex < _data.GetPersonsCount())
            {
                if(DateTime.Compare(_view.BirthDate, _view.MissingDate) <= 0)
                {
                    _model.CurrentPerson = _view.SelectedPerson;
                    _model.CurrentPerson.BirthDate = _view.BirthDate.ToString();
                    _model.CurrentPerson.MissingDate = _view.MissingDate.ToString();
                    _model.CurrentPerson.AdditionalInfo = _view.AdditionalInfo;
 
                    string updateMessage = Properties.Resources.AddPersonInfoUpdatedMsg;

                    // to do: class with method 'checkphotosvalidation'
                    if (picSelected)
                    {
                        int viablePicsCount = 0;
                        picSelected = false;

                        string personName = _model.CurrentPerson.Name;
                        personName = personName.Replace(" ", "_");

                        foreach (string filename in picFilenames)
                        {
                            tempImage = new Image<Bgr, byte>(filename);

                            if (ImageHandler.GetFaceFromImage(tempImage) != null)
                            {
                                var face = ImageHandler.GetFaceFromImage(tempImage);
                                var grayFace = face.Convert<Gray, Byte>();
                                ImageHandler.SaveGrayFacetoFile(personName, grayFace);

                                viablePicsCount++;
                            }
                        }
                        updateMessage = String.Format(Properties.Resources.AddPersonPicturesUpdated, viablePicsCount, picFilenames.Count, Properties.Resources.AddPersonInfoUpdatedMsg);
                    }
                    _view.ShowMessage(updateMessage);
                }
                else
                    _view.ShowMessage(Properties.Resources.AddPersonNotValidDatesError);
            }
            else
                _view.ShowMessage(Properties.Resources.AddPersonNoSelectedPersonError);
        }

        public void OnCreateNewPersonClicked(object sender, EventArgs e)
        {
            if (picSelected)
            {
                if (!String.IsNullOrWhiteSpace(_view.NameSurname) && DateTime.Compare(_view.BirthDate, _view.MissingDate) <= 0)
                {
                    picSelected = false;

                    // to do: make another class and method for photos validation
                    string directory = _view.NameSurname;
                    directory = directory.Replace(" ", "_");

                    if (!File.Exists(Application.StartupPath + "/data/" + directory + "/rep.bmp"))
                        ImageHandler.SaveRepresentativePic(tempImage.ToBitmap(), directory);

                    int viablePicsCount = 0;

                    foreach (string filename in picFilenames)
                    {
                        tempImage = new Image<Bgr, byte>(filename);

                        if (ImageHandler.GetFaceFromImage(tempImage) != null)
                        {
                            var newFace = ImageHandler.GetFaceFromImage(tempImage);
                            var newGrayFace = newFace.Convert<Gray, Byte>();
                            ImageHandler.SaveGrayFacetoFile(directory, newGrayFace);
                            viablePicsCount++;
                        }
                    }
                    if (viablePicsCount > 0)
                    {
                        _model.Add(_view.NameSurname, _view.BirthDate.ToString(), _view.MissingDate.ToString(), _view.AdditionalInfo);
                        _view.AddPersonToList(_model.CurrentPerson);

                        _view.ShowMessage(String.Format(Properties.Resources.AddPersonPhotosAddedMsg, _view.NameSurname, viablePicsCount, picFilenames.Count));
                        _view.PersonImage = Properties.Resources.NewPerson;

                        _view.NameSurname = "";
                        _view.NameSurnameEnabled = true;

                        _view.MissingDate = DateTime.Today;
                        _view.BirthDate = DateTime.Today;
                        _view.AdditionalInfo = "";
                    }
                    else
                        _view.ShowMessage(Properties.Resources.AddPersonsNoValidPersonsError);
                }
                else
                    _view.ShowMessage(Properties.Resources.AddPersonNotValidInfoError);

            }
            else
                _view.ShowMessage(Properties.Resources.AddPersonNoPhotosError);
        }
    }
}
