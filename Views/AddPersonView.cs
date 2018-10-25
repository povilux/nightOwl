using nightOwl.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nightOwl.Views
{
    public partial class AddPersonView : Form, IAddPersonView
    {
      
        private readonly AddPersonPresenter _presenter;

        public AddPersonView()
        {
            InitializeComponent();
            _presenter = new AddPersonPresenter(this);
        
            foreach (var person in MainForm.persons)
                PersonsList.Items.Add(person.Name);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.BackButtonClicked(sender, e);
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.CloseButtonClicked(sender, e);
        }

        private void CreateNewPersonDataButton_Click(object sender, EventArgs e)
        {
            this.CreateNewPersonDataButtonClicked(sender, e);
        }

        private void PersonsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PersonSelectedFromList(sender, e);
        }

        private void AddNewPersonButton_Click(object sender, EventArgs e)
        {
            this.NewPersonCreatingClicked(sender, e);
        }
        private void SelectPersonButton_Click(object sender, EventArgs e)
        {
            this.SelectPersonButtonClicked(sender, e);
        }

        private void UpdateInfoButton_Click(object sender, EventArgs e)
        {
            this.UpdatePersonCliked(sender, e);
        }

        public void AddPersonToList(string item) { PersonsList.Items.Add(item); }
        public string NameSurname { get { return NameField.Text; } set { NameField.Text = value; } }
        public string BirthDate { get { return BirthDateField.Text; } set { BirthDateField.Text = value; } }
        public string MissingDate { get { return MissingDateField.Text; } set { MissingDateField.Text = value; } }
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
    }
}
