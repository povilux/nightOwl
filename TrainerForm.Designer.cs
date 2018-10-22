namespace nightOwl
{
    partial class TrainerForm
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
            this.backButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.addPhotoButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nameField = new System.Windows.Forms.TextBox();
            this.updateInfoButton = new System.Windows.Forms.Button();
            this.addNewPersonButton = new System.Windows.Forms.Button();
            this.createNewPersonDataButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.birthDateField = new System.Windows.Forms.TextBox();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.additionalInfoField = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.missingDateField = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.BackgroundImage = global::nightOwl.Properties.Resources.Back;
            this.backButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.Location = new System.Drawing.Point(47, 43);
            this.backButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(200, 62);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.BackgroundImage = global::nightOwl.Properties.Resources.Exiit;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(314, 43);
            this.closeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(200, 62);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(420, 299);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(202, 164);
            this.listBox1.TabIndex = 11;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(685, 299);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(165, 158);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // addPhotoButton
            // 
            this.addPhotoButton.Location = new System.Drawing.Point(685, 485);
            this.addPhotoButton.Name = "addPhotoButton";
            this.addPhotoButton.Size = new System.Drawing.Size(165, 46);
            this.addPhotoButton.TabIndex = 14;
            this.addPhotoButton.Text = "Add photo";
            this.addPhotoButton.UseVisualStyleBackColor = true;
            this.addPhotoButton.Click += new System.EventHandler(this.addPhotoButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(681, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 24);
            this.label1.TabIndex = 15;
            this.label1.Text = "Manage missing persons data:";
            // 
            // listBoxLabel
            // 
            this.listBoxLabel.AutoSize = true;
            this.listBoxLabel.Location = new System.Drawing.Point(417, 268);
            this.listBoxLabel.Name = "listBoxLabel";
            this.listBoxLabel.Size = new System.Drawing.Size(99, 17);
            this.listBoxLabel.TabIndex = 16;
            this.listBoxLabel.Text = "Select person:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(889, 299);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Name, surname:";
            // 
            // nameField
            // 
            this.nameField.Location = new System.Drawing.Point(1031, 299);
            this.nameField.Name = "nameField";
            this.nameField.Size = new System.Drawing.Size(171, 22);
            this.nameField.TabIndex = 18;
            // 
            // updateInfoButton
            // 
            this.updateInfoButton.Location = new System.Drawing.Point(685, 551);
            this.updateInfoButton.Name = "updateInfoButton";
            this.updateInfoButton.Size = new System.Drawing.Size(165, 47);
            this.updateInfoButton.TabIndex = 19;
            this.updateInfoButton.Text = "Update persons data";
            this.updateInfoButton.UseVisualStyleBackColor = true;
            this.updateInfoButton.Click += new System.EventHandler(this.updateInfoButton_Click);
            // 
            // addNewPersonButton
            // 
            this.addNewPersonButton.Location = new System.Drawing.Point(420, 485);
            this.addNewPersonButton.Name = "addNewPersonButton";
            this.addNewPersonButton.Size = new System.Drawing.Size(202, 46);
            this.addNewPersonButton.TabIndex = 20;
            this.addNewPersonButton.Text = "Add new person";
            this.addNewPersonButton.UseVisualStyleBackColor = true;
            this.addNewPersonButton.Click += new System.EventHandler(this.addNewPersonButton_Click);
            // 
            // createNewPersonDataButton
            // 
            this.createNewPersonDataButton.Location = new System.Drawing.Point(685, 551);
            this.createNewPersonDataButton.Name = "createNewPersonDataButton";
            this.createNewPersonDataButton.Size = new System.Drawing.Size(165, 51);
            this.createNewPersonDataButton.TabIndex = 21;
            this.createNewPersonDataButton.Text = "Add persons data";
            this.createNewPersonDataButton.UseVisualStyleBackColor = true;
            this.createNewPersonDataButton.Click += new System.EventHandler(this.createNewPersonDataButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(889, 351);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 34);
            this.label2.TabIndex = 22;
            this.label2.Text = "Birth date :\r\n(yyyy-mm-dd)";
            // 
            // birthDateField
            // 
            this.birthDateField.Location = new System.Drawing.Point(1031, 351);
            this.birthDateField.Name = "birthDateField";
            this.birthDateField.Size = new System.Drawing.Size(171, 22);
            this.birthDateField.TabIndex = 23;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(889, 416);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 34);
            this.label4.TabIndex = 24;
            this.label4.Text = "Additional \r\ninformation:";
            // 
            // additionalInfoField
            // 
            this.additionalInfoField.Location = new System.Drawing.Point(1031, 416);
            this.additionalInfoField.Multiline = true;
            this.additionalInfoField.Name = "additionalInfoField";
            this.additionalInfoField.Size = new System.Drawing.Size(171, 79);
            this.additionalInfoField.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(888, 533);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 34);
            this.label5.TabIndex = 26;
            this.label5.Text = "Missing date:\r\n(yyyy-MM-dd hh:mm)";
            // 
            // missingDateField
            // 
            this.missingDateField.Location = new System.Drawing.Point(1031, 533);
            this.missingDateField.Name = "missingDateField";
            this.missingDateField.Size = new System.Drawing.Size(171, 22);
            this.missingDateField.TabIndex = 27;
            // 
            // TrainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::nightOwl.Properties.Resources.bgWebCam;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1850, 838);
            this.Controls.Add(this.missingDateField);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.additionalInfoField);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.birthDateField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.createNewPersonDataButton);
            this.Controls.Add(this.addNewPersonButton);
            this.Controls.Add(this.updateInfoButton);
            this.Controls.Add(this.nameField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addPhotoButton);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.backButton);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TrainerForm";
            this.Text = "Tvarkyti žmonių duomenis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrainerForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button addPhotoButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label listBoxLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nameField;
        private System.Windows.Forms.Button updateInfoButton;
        private System.Windows.Forms.Button addNewPersonButton;
        private System.Windows.Forms.Button createNewPersonDataButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox birthDateField;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox additionalInfoField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox missingDateField;
    }
}