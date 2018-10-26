using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using nightOwl.Logic;
using nightOwl.Models;
using nightOwl.Views;
using System.Configuration;

namespace nightOwl.Presenters
{
    public class AddPersonPresenter
    {
        bool picSelected = false;
        Image<Bgr, byte> tempImage;
        List<string> picFilenames;

        private readonly IAddPersonView _view;
        private readonly IPersonModel _model;

        /* modelis turetu gauti Person duomenis is view per presenteri, padaryti veiksmus su Person ir grazinti atgal presenteriui
         pvz: keiciami Person duomenis
         View persiuncia presenteriui Person duomenis: name, birth date, missing date.
         Presenteris siuncia duomenis modeliui ir igyvendina metoda "Update".
         Modelis konkreciam Person atnaujina informacija ir grazina presenteriui kad pavyko/nepavyko tai padaryti.
         Presenteris tai parodo i view
         */

            // ATEICIAI: datos kol kas saugomos kaip string (nors is view gaunamos kaip datetime). Galbut galima saugoti kaip datetime

        public AddPersonPresenter(IAddPersonView view, IPersonModel model)
        {
            _view = view;
            _model = model;
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

            foreach(var person in FirstPageView.persons)
                _view.AddPersonToList(person.Name);
        }

        public void OnBackButtonClicked(object sender, EventArgs e)
        {
            _view.Close();
            FirstPageView.self.Show();
        }
        public void OnCloseButtonClicked(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void OnPersonSelected(object sender, EventArgs e)
        {
            if (_view.SelectedPersonIndex >= 0 && _view.SelectedPersonIndex < FirstPageView.persons.Count)
            {
                _view.NameSurname = _view.SelectedPersonName;

                _model.FindPerson(_view.NameSurname);
                _view.BirthDate = DateTime.Parse(_model.CurrentPerson.BirthDate);
                _view.MissingDate = DateTime.Parse(_model.CurrentPerson.MissingDate);
                _view.AdditionalInfo = _model.CurrentPerson.AdditionalInfo;

                // to do: something...
                string chosenName = _view.NameSurname;
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

            _view.MissingDate = new DateTime(); 
            _view.BirthDate = new DateTime();
            _view.AdditionalInfo = "";

            _view.AddNewPersonBtnEnabled = false;
            _view.SelectPersonBtnEnabled = true;

            _view.UpdateInfoBtnEnabled = false;
            _view.CreateNewPersonDataBtnEnabled = true;          
        }

        public void OnUpdatePersonCliked(object sender, EventArgs e)
        {
            if (_view.SelectedPersonIndex >= 0 && _view.SelectedPersonIndex < FirstPageView.persons.Count)
            {
                if(DateTime.Compare(_view.BirthDate, _view.MissingDate) <= 0)
                {              
                    _model.FindPerson(_view.SelectedPersonName);
                    _model.CurrentPerson.BirthDate = _view.BirthDate.ToString();
                    _model.CurrentPerson.MissingDate = _view.MissingDate.ToString();
                    _model.CurrentPerson.AdditionalInfo = _view.AdditionalInfo;
 
                    string updateMessage = Properties.Resources.AddPersonInfoUpdatedMsg;

                    // to do: class with method 'checkphotosvalidation'
                    if (picSelected)
                    {
                        int viablePicsCount = 0;
                        picSelected = false;

                        foreach (string filename in picFilenames)
                        {
                            tempImage = new Image<Bgr, byte>(filename);

                            if (ImageHandler.GetFaceFromImage(tempImage) != null)
                            {
                                var face = ImageHandler.GetFaceFromImage(tempImage);
                                var grayFace = face.Convert<Gray, Byte>();
                                ImageHandler.SaveGrayFacetoFile(_view.SelectedPersonName, grayFace);

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
                        _view.AddPersonToList(_view.NameSurname);
                        _model.Add(_view.NameSurname, _view.BirthDate.ToString(), _view.MissingDate.ToString(), _view.AdditionalInfo);
                           
                        // to do: load/save data doing on start/exit program
                         ImageHandler.WriteDataToFile(FirstPageView.persons);

                        _view.ShowMessage(String.Format(Properties.Resources.AddPersonPhotosAddedMsg, _view.NameSurname, viablePicsCount, picFilenames.Count));
                        _view.PersonImage = Properties.Resources.NewPerson;

                        _view.NameSurname = "";
                        _view.NameSurnameEnabled = true;

                        _view.MissingDate = new DateTime();
                        _view.BirthDate = new DateTime();
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
