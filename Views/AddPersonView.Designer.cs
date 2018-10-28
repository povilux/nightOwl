﻿using System.Windows.Forms;

namespace nightOwl.Views
{
    partial class AddPersonView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BackButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.PersonsList = new System.Windows.Forms.ListBox();
            this.PersonImageBox = new System.Windows.Forms.PictureBox();
            this.AddPhotoButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.PersonsListLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameField = new System.Windows.Forms.TextBox();
            this.UpdateInfoButton = new System.Windows.Forms.Button();
            this.AddNewPersonButton = new System.Windows.Forms.Button();
            this.CreateNewPersonDataButton = new System.Windows.Forms.Button();
            this.BirthDateLabel = new System.Windows.Forms.Label();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.AdditionalInfoField = new System.Windows.Forms.TextBox();
            this.MissingDateLabel = new System.Windows.Forms.Label();
            this.SelectPersonButton = new System.Windows.Forms.Button();
            this.BirthDatePicker = new System.Windows.Forms.DateTimePicker();
            this.MissingDatePicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.PersonImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // BackButton
            // 
            this.BackButton.BackgroundImage = global::nightOwl.Properties.Resources.Back;
            this.BackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackButton.Location = new System.Drawing.Point(35, 35);
            this.BackButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(150, 50);
            this.BackButton.TabIndex = 0;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackgroundImage = global::nightOwl.Properties.Resources.Exiit;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(236, 35);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(150, 50);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // PersonsList
            // 
            this.PersonsList.FormattingEnabled = true;
            this.PersonsList.Location = new System.Drawing.Point(315, 243);
            this.PersonsList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PersonsList.Name = "PersonsList";
            this.PersonsList.Size = new System.Drawing.Size(152, 134);
            this.PersonsList.TabIndex = 11;
            this.PersonsList.SelectedIndexChanged += new System.EventHandler(this.PersonsList_SelectedIndexChanged);
            // 
            // PersonImageBox
            // 
            this.PersonImageBox.Image = global::nightOwl.Properties.Resources.Capone_dark;
            this.PersonImageBox.Location = new System.Drawing.Point(514, 243);
            this.PersonImageBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PersonImageBox.Name = "PersonImageBox";
            this.PersonImageBox.Size = new System.Drawing.Size(124, 128);
            this.PersonImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PersonImageBox.TabIndex = 6;
            this.PersonImageBox.TabStop = false;
            // 
            // AddPhotoButton
            // 
            this.AddPhotoButton.Location = new System.Drawing.Point(514, 394);
            this.AddPhotoButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AddPhotoButton.Name = "AddPhotoButton";
            this.AddPhotoButton.Size = new System.Drawing.Size(124, 37);
            this.AddPhotoButton.TabIndex = 14;
            this.AddPhotoButton.Text = "Add photo";
            this.AddPhotoButton.UseVisualStyleBackColor = true;
            this.AddPhotoButton.Click += new System.EventHandler(this.AddPhotoButton_Click);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(511, 164);
            this.TitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(210, 18);
            this.TitleLabel.TabIndex = 15;
            this.TitleLabel.Text = "Manage missing persons data:";
            // 
            // PersonsListLabel
            // 
            this.PersonsListLabel.AutoSize = true;
            this.PersonsListLabel.Location = new System.Drawing.Point(313, 218);
            this.PersonsListLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PersonsListLabel.Name = "PersonsListLabel";
            this.PersonsListLabel.Size = new System.Drawing.Size(75, 13);
            this.PersonsListLabel.TabIndex = 16;
            this.PersonsListLabel.Text = "Select person:";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(667, 243);
            this.NameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(84, 13);
            this.NameLabel.TabIndex = 17;
            this.NameLabel.Text = "Name, surname:";
            // 
            // NameField
            // 
            this.NameField.Enabled = false;
            this.NameField.Location = new System.Drawing.Point(773, 243);
            this.NameField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.NameField.Name = "NameField";
            this.NameField.Size = new System.Drawing.Size(129, 20);
            this.NameField.TabIndex = 18;
            // 
            // UpdateInfoButton
            // 
            this.UpdateInfoButton.Location = new System.Drawing.Point(514, 448);
            this.UpdateInfoButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UpdateInfoButton.Name = "UpdateInfoButton";
            this.UpdateInfoButton.Size = new System.Drawing.Size(124, 38);
            this.UpdateInfoButton.TabIndex = 19;
            this.UpdateInfoButton.Text = "Update persons data";
            this.UpdateInfoButton.UseVisualStyleBackColor = true;
            this.UpdateInfoButton.Click += new System.EventHandler(this.UpdateInfoButton_Click);
            // 
            // AddNewPersonButton
            // 
            this.AddNewPersonButton.Location = new System.Drawing.Point(315, 394);
            this.AddNewPersonButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AddNewPersonButton.Name = "AddNewPersonButton";
            this.AddNewPersonButton.Size = new System.Drawing.Size(152, 37);
            this.AddNewPersonButton.TabIndex = 20;
            this.AddNewPersonButton.Text = "Add new person";
            this.AddNewPersonButton.UseVisualStyleBackColor = true;
            this.AddNewPersonButton.Click += new System.EventHandler(this.AddNewPersonButton_Click);
            // 
            // CreateNewPersonDataButton
            // 
            this.CreateNewPersonDataButton.Location = new System.Drawing.Point(514, 448);
            this.CreateNewPersonDataButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CreateNewPersonDataButton.Name = "CreateNewPersonDataButton";
            this.CreateNewPersonDataButton.Size = new System.Drawing.Size(124, 41);
            this.CreateNewPersonDataButton.TabIndex = 21;
            this.CreateNewPersonDataButton.Text = "Add persons data";
            this.CreateNewPersonDataButton.UseVisualStyleBackColor = true;
            this.CreateNewPersonDataButton.Visible = false;
            this.CreateNewPersonDataButton.Click += new System.EventHandler(this.CreateNewPersonDataButton_Click);
            // 
            // BirthDateLabel
            // 
            this.BirthDateLabel.AutoSize = true;
            this.BirthDateLabel.Location = new System.Drawing.Point(667, 285);
            this.BirthDateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BirthDateLabel.Name = "BirthDateLabel";
            this.BirthDateLabel.Size = new System.Drawing.Size(58, 13);
            this.BirthDateLabel.TabIndex = 22;
            this.BirthDateLabel.Text = "Birth date :\r\n";
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(667, 338);
            this.InfoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(61, 26);
            this.InfoLabel.TabIndex = 24;
            this.InfoLabel.Text = "Additional \r\ninformation:";
            // 
            // AdditionalInfoField
            // 
            this.AdditionalInfoField.Location = new System.Drawing.Point(773, 338);
            this.AdditionalInfoField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AdditionalInfoField.Multiline = true;
            this.AdditionalInfoField.Name = "AdditionalInfoField";
            this.AdditionalInfoField.Size = new System.Drawing.Size(129, 65);
            this.AdditionalInfoField.TabIndex = 25;
            // 
            // MissingDateLabel
            // 
            this.MissingDateLabel.AutoSize = true;
            this.MissingDateLabel.Location = new System.Drawing.Point(666, 433);
            this.MissingDateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MissingDateLabel.Name = "MissingDateLabel";
            this.MissingDateLabel.Size = new System.Drawing.Size(69, 13);
            this.MissingDateLabel.TabIndex = 26;
            this.MissingDateLabel.Text = "Missing date:\r\n";
            // 
            // SelectPersonButton
            // 
            this.SelectPersonButton.Location = new System.Drawing.Point(315, 394);
            this.SelectPersonButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SelectPersonButton.Name = "SelectPersonButton";
            this.SelectPersonButton.Size = new System.Drawing.Size(152, 37);
            this.SelectPersonButton.TabIndex = 28;
            this.SelectPersonButton.Text = "Select person from the list";
            this.SelectPersonButton.UseVisualStyleBackColor = true;
            this.SelectPersonButton.Visible = false;
            this.SelectPersonButton.Click += new System.EventHandler(this.SelectPersonButton_Click);
            // 
            // BirthDatePicker
            // 
            this.BirthDatePicker.CustomFormat = "yyyy MM dd";
            this.BirthDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.BirthDatePicker.Location = new System.Drawing.Point(773, 285);
            this.BirthDatePicker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BirthDatePicker.Name = "BirthDatePicker";
            this.BirthDatePicker.Size = new System.Drawing.Size(151, 20);
            this.BirthDatePicker.TabIndex = 29;
            // 
            // MissingDatePicker
            // 
            this.MissingDatePicker.CustomFormat = "yyyy MM dd hh:mm";
            this.MissingDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.MissingDatePicker.Location = new System.Drawing.Point(773, 433);
            this.MissingDatePicker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MissingDatePicker.Name = "MissingDatePicker";
            this.MissingDatePicker.Size = new System.Drawing.Size(151, 20);
            this.MissingDatePicker.TabIndex = 30;
            // 
            // AddPersonView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::nightOwl.Properties.Resources.bgWebCam;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.MissingDatePicker);
            this.Controls.Add(this.BirthDatePicker);
            this.Controls.Add(this.SelectPersonButton);
            this.Controls.Add(this.MissingDateLabel);
            this.Controls.Add(this.AdditionalInfoField);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.BirthDateLabel);
            this.Controls.Add(this.CreateNewPersonDataButton);
            this.Controls.Add(this.AddNewPersonButton);
            this.Controls.Add(this.UpdateInfoButton);
            this.Controls.Add(this.NameField);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.PersonsListLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.AddPhotoButton);
            this.Controls.Add(this.PersonsList);
            this.Controls.Add(this.PersonImageBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.BackButton);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AddPersonView";
            this.Text = "Manage persons data";
            ((System.ComponentModel.ISupportInitialize)(this.PersonImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.PictureBox PersonImageBox;
        private System.Windows.Forms.ListBox PersonsList;
        private System.Windows.Forms.Button AddPhotoButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label PersonsListLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox NameField;
        private System.Windows.Forms.Button UpdateInfoButton;
        private System.Windows.Forms.Button AddNewPersonButton;
        private System.Windows.Forms.Button CreateNewPersonDataButton;
        private System.Windows.Forms.Label BirthDateLabel;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.TextBox AdditionalInfoField;
        private System.Windows.Forms.Label MissingDateLabel;
        private System.Windows.Forms.Button SelectPersonButton;
        private System.Windows.Forms.DateTimePicker BirthDatePicker;
        private System.Windows.Forms.DateTimePicker MissingDatePicker;
    }
}