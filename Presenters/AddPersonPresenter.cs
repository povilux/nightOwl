using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using nightOwl.Views;

namespace nightOwl.Presenters
{
    public class AddPersonPresenter
    {
        bool picSelected = false;
        bool personSelected = true;
        Image<Bgr, byte> tempImage;
        List<string> picFilenames;

        private readonly IAddPersonView _view;
        //  private readonly List<PersonModel> _models;

        public AddPersonPresenter(IAddPersonView view)
        {
            _view = view;
            //_model = new List<PersonModel>();

            Initialize();
        }

        private void Initialize()
        {
            this._view.BackButtonClicked += new EventHandler(OnBackButtonClicked);
            this._view.CloseButtonClicked += new EventHandler(OnCloseButtonClicked);
            this._view.CreateNewPersonDataButtonClicked += new EventHandler(OnCreateNewPersonClicked);
            this._view.PersonSelectedFromList += new EventHandler(OnPersonSelected);
            this._view.NewPersonCreatingClicked += new EventHandler(OnNewPersonCreatingClicked);
            this._view.UpdatePersonCliked += new EventHandler(OnUpdatePersonCliked);
            this._view.SelectPersonButtonClicked += new EventHandler(OnSelectPersonButtonClicked);
        }

        public void OnBackButtonClicked(object sender, EventArgs e)
        {
            _view.Close();
        }
        public void OnCloseButtonClicked(object sender, EventArgs e)
        {
            _view.Close();
        }

        public void OnPersonSelected(object sender, EventArgs e)
        {
            if (_view.SelectedPersonIndex >= 0 && _view.SelectedPersonIndex < MainForm.persons.Count)
            {
                personSelected = true;
                _view.NameSurname = _view.SelectedPersonName;

                Person person = MainForm.persons.Where(p => String.Equals(p.Name, _view.NameSurname)).First();

                _view.BirthDate = person.BirthDate;
                _view.MissingDate = person.MissingDate;
                _view.AdditionalInfo = person.AdditionalInfo;

                string chosenName = _view.NameSurname;
                chosenName = chosenName.Replace(" ", "_");

                _view.PersonImage = ImageHandler.LoadRepresentativePic(chosenName);
            }
        }

        public void OnSelectPersonButtonClicked(object sender, EventArgs e)
        {
            _view.PersonsListEnabled = true;
            _view.PersonsListTitle = true;
            _view.PersonImage =Properties.Resources.Capone_dark;
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

            _view.MissingDate = "";
            _view.BirthDate = "";
            _view.AdditionalInfo = "";

            _view.AddNewPersonBtnEnabled = false;
            _view.SelectPersonBtnEnabled = true;

            _view.UpdateInfoBtnEnabled = false;
            _view.CreateNewPersonDataBtnEnabled = true;          
        }

        public void OnUpdatePersonCliked(object sender, EventArgs e)
        {
            if (_view.SelectedPersonIndex >= 0 && _view.SelectedPersonIndex < MainForm.persons.Count)
            {
                string chosenName = _view.SelectedPersonName;

                var person = MainForm.persons.Where(p => String.Equals(p.Name, chosenName)).First();

                DateTime dt;
                if (!DateTime.TryParseExact(_view.BirthDate, "yyyy-MM-dd", new CultureInfo("lt-LT"), DateTimeStyles.None, out dt) ||
                    !DateTime.TryParseExact(_view.MissingDate, "yyyy-MM-dd hh:mm", new CultureInfo("lt-LT"), DateTimeStyles.AssumeLocal, out dt))
                {
                    _view.ShowMessage("The missing or birth date is not correct!");
                }
                else
                {
                    person.BirthDate = _view.BirthDate;
                    person.MissingDate = _view.MissingDate;
                    person.AdditionalInfo = _view.AdditionalInfo;

                    string updateMessage = Properties.Resources.AddPersonInfoUpdatedMsg;

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
                        updateMessage = String.Format(Properties.Resources.AddPersonPicturesUpdated, viablePicsCount, picFilenames.Count, Properties.Resources.AddPersonInfoUpdatedMsg);
                    }
                    _view.ShowMessage(updateMessage);
                }
            }
            else
                _view.ShowMessage(Properties.Resources.AddPersonNoSelectedPersonError);
        }

        public void OnCreateNewPersonClicked(object sender, EventArgs e)
        {
            int viablePicsCount = 0;
            int notViablePicsCount = 0;

            if (picSelected)
            {
                DateTime dt;
                if (!String.IsNullOrWhiteSpace(_view.NameSurname) && DateTime.TryParseExact(_view.BirthDate, "yyyy-MM-dd", new CultureInfo("lt-LT"), DateTimeStyles.None, out dt) &&
                        DateTime.TryParseExact(_view.MissingDate, "yyyy-MM-dd hh:mm", new CultureInfo("lt-LT"), DateTimeStyles.AssumeLocal, out dt))
                {
                    picSelected = false;

                    string directory = _view.NameSurname;
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
                        //listBox1.Items.Add(_view.NameSurname);
                        MainForm.persons.Add(new Person(_view.NameSurname, _view.BirthDate, _view.MissingDate, _view.AdditionalInfo));
                        ImageHandler.WriteDataToFile(MainForm.persons);

                        _view.ShowMessage(String.Format(Properties.Resources.AddPersonPhotosAddedMsg, _view.NameSurname, viablePicsCount, picFilenames.Count));

                        _view.PersonImage = Properties.Resources.NewPerson;

                        _view.NameSurname = "";
                        _view.NameSurnameEnabled = true;

                        _view.MissingDate = "";
                        _view.BirthDate = "";
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

        /* public void OnSaveButtonClicked()
         {
             var person = new PersonModel(_view.InputFirstName, _view.InputLastName, GetGender());

             _view.AddButtonEnabled = false;
             _view.InputFirstName = null;
             _view.InputLastName = null;

             _models.Add(person);

             _view.ShowMessage("Successfully added person '" + person.FirstName + @"'.");

             RefreshTable();
         }
         public void OnTextChanged()
         {
             if (_view.InputFirstName == string.Empty || _view.InputLastName == string.Empty)
             {
                 _view.AddButtonEnabled = false;
             }
             else
             {
                 _view.AddButtonEnabled = true;
             }
         }*/
    }
}
