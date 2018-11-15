namespace NightOwl.WindowsForms.Views
{
    partial class LoginFormView
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
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UserNameField = new System.Windows.Forms.TextBox();
            this.PasswordField = new System.Windows.Forms.TextBox();
            this.LoginTitleLabel = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.UserNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameLabel.Location = new System.Drawing.Point(464, 293);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(108, 25);
            this.UserNameLabel.TabIndex = 0;
            this.UserNameLabel.Text = "Username:";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.PasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.Location = new System.Drawing.Point(464, 347);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(104, 25);
            this.PasswordLabel.TabIndex = 1;
            this.PasswordLabel.Text = "Password:";
            this.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // UserNameField
            // 
            this.UserNameField.Location = new System.Drawing.Point(612, 295);
            this.UserNameField.Name = "UserNameField";
            this.UserNameField.Size = new System.Drawing.Size(158, 22);
            this.UserNameField.TabIndex = 2;
            // 
            // PasswordField
            // 
            this.PasswordField.Location = new System.Drawing.Point(612, 351);
            this.PasswordField.Name = "PasswordField";
            this.PasswordField.PasswordChar = '*';
            this.PasswordField.Size = new System.Drawing.Size(158, 22);
            this.PasswordField.TabIndex = 3;
            // 
            // LoginTitleLabel
            // 
            this.LoginTitleLabel.AutoSize = true;
            this.LoginTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginTitleLabel.Location = new System.Drawing.Point(723, 206);
            this.LoginTitleLabel.Name = "LoginTitleLabel";
            this.LoginTitleLabel.Size = new System.Drawing.Size(186, 25);
            this.LoginTitleLabel.TabIndex = 4;
            this.LoginTitleLabel.Text = "Login to the system:";
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(647, 421);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(110, 51);
            this.LoginButton.TabIndex = 5;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // RegisterButton
            // 
            this.RegisterButton.Location = new System.Drawing.Point(803, 421);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(116, 51);
            this.RegisterButton.TabIndex = 6;
            this.RegisterButton.Text = "Register";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // LoginFormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::NightOwl.WindowsForms.Properties.Resources.bgWebCam;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1685, 838);
            this.Controls.Add(this.RegisterButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.LoginTitleLabel);
            this.Controls.Add(this.PasswordField);
            this.Controls.Add(this.UserNameField);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UserNameLabel);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoginFormView";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox UserNameField;
        private System.Windows.Forms.TextBox PasswordField;
        private System.Windows.Forms.Label LoginTitleLabel;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Button RegisterButton;
    }
}