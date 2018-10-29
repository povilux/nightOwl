using nightOwl.Properties;

namespace nightOwl.Views
{
    partial class FirstPageView
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
            this.SelectVideoButton = new System.Windows.Forms.Button();
            this.WatchCameraButton = new System.Windows.Forms.Button();
            this.AddNewPersonButton = new System.Windows.Forms.Button();
            this.ShowMapButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SelectVideoButton
            // 
            this.SelectVideoButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.SelectVideoButton.FlatAppearance.BorderSize = 0;
            this.SelectVideoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectVideoButton.ForeColor = System.Drawing.Color.White;
            this.SelectVideoButton.Location = new System.Drawing.Point(511, 209);
            this.SelectVideoButton.Margin = new System.Windows.Forms.Padding(4);
            this.SelectVideoButton.Name = "SelectVideoButton";
            this.SelectVideoButton.Size = new System.Drawing.Size(667, 86);
            this.SelectVideoButton.TabIndex = 0;
            this.SelectVideoButton.Text = "Select Video";
            this.SelectVideoButton.UseVisualStyleBackColor = false;
            this.SelectVideoButton.Click += new System.EventHandler(this.SelectVideoButton_Click);
            // 
            // WatchCameraButton
            // 
            this.WatchCameraButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.WatchCameraButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.WatchCameraButton.FlatAppearance.BorderSize = 0;
            this.WatchCameraButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.WatchCameraButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WatchCameraButton.ForeColor = System.Drawing.Color.White;
            this.WatchCameraButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.WatchCameraButton.Location = new System.Drawing.Point(511, 357);
            this.WatchCameraButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.WatchCameraButton.Name = "WatchCameraButton";
            this.WatchCameraButton.Size = new System.Drawing.Size(667, 86);
            this.WatchCameraButton.TabIndex = 2;
            this.WatchCameraButton.Text = "Watch camera";
            this.WatchCameraButton.UseVisualStyleBackColor = false;
            this.WatchCameraButton.Click += new System.EventHandler(this.WatchCameraButton_Click);
            // 
            // AddNewPersonButton
            // 
            this.AddNewPersonButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.AddNewPersonButton.FlatAppearance.BorderSize = 0;
            this.AddNewPersonButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddNewPersonButton.ForeColor = System.Drawing.Color.White;
            this.AddNewPersonButton.Location = new System.Drawing.Point(511, 506);
            this.AddNewPersonButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddNewPersonButton.Name = "AddNewPersonButton";
            this.AddNewPersonButton.Size = new System.Drawing.Size(667, 86);
            this.AddNewPersonButton.TabIndex = 3;
            this.AddNewPersonButton.Text = "Add new person";
            this.AddNewPersonButton.UseVisualStyleBackColor = false;
            this.AddNewPersonButton.Click += new System.EventHandler(this.AddPersonButton_Click);
            // 
            // ShowMapButton
            // 
            this.ShowMapButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.ShowMapButton.FlatAppearance.BorderSize = 0;
            this.ShowMapButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowMapButton.ForeColor = System.Drawing.Color.White;
            this.ShowMapButton.Location = new System.Drawing.Point(511, 652);
            this.ShowMapButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ShowMapButton.Name = "ShowMapButton";
            this.ShowMapButton.Size = new System.Drawing.Size(667, 86);
            this.ShowMapButton.TabIndex = 4;
            this.ShowMapButton.Text = "Map";
            this.ShowMapButton.UseVisualStyleBackColor = false;
            this.ShowMapButton.Click += new System.EventHandler(this.ShowMapButton_Click);
            // 
            // FirstPageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::nightOwl.Properties.Resources.background3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1685, 838);
            this.Controls.Add(this.ShowMapButton);
            this.Controls.Add(this.AddNewPersonButton);
            this.Controls.Add(this.WatchCameraButton);
            this.Controls.Add(this.SelectVideoButton);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FirstPageView";
            this.Text = "SmartVision";
            this.Load += new System.EventHandler(this.FirstPageView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SelectVideoButton;
        private System.Windows.Forms.Button WatchCameraButton;
        private System.Windows.Forms.Button AddNewPersonButton;
        private System.Windows.Forms.Button ShowMapButton;
    }
}