namespace nightOwl
{
    partial class VideoRecognitionView
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
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.VideoTrackBar = new System.Windows.Forms.TrackBar();
            this.ImgVideoBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.VideoTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgVideoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.OpenFileButton.FlatAppearance.BorderSize = 0;
            this.OpenFileButton.Location = new System.Drawing.Point(511, 692);
            this.OpenFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(667, 86);
            this.OpenFileButton.TabIndex = 2;
            this.OpenFileButton.Text = "Open file";
            this.OpenFileButton.UseVisualStyleBackColor = false;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.BackgroundImage = global::nightOwl.Properties.Resources.Back;
            this.BackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackButton.Location = new System.Drawing.Point(47, 43);
            this.BackButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(200, 62);
            this.BackButton.TabIndex = 3;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackgroundImage = global::nightOwl.Properties.Resources.Exiit;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(320, 43);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(200, 62);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // VideoTrackBar
            // 
            this.VideoTrackBar.Location = new System.Drawing.Point(569, 587);
            this.VideoTrackBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VideoTrackBar.Name = "VideoTrackBar";
            this.VideoTrackBar.Size = new System.Drawing.Size(549, 56);
            this.VideoTrackBar.TabIndex = 6;
            this.VideoTrackBar.Scroll += new System.EventHandler(this.VideoTrackBar_Scroll);
            // 
            // ImgVideoBox
            // 
            this.ImgVideoBox.Location = new System.Drawing.Point(569, 222);
            this.ImgVideoBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ImgVideoBox.Name = "ImgVideoBox";
            this.ImgVideoBox.Size = new System.Drawing.Size(549, 327);
            this.ImgVideoBox.TabIndex = 5;
            this.ImgVideoBox.TabStop = false;
            this.ImgVideoBox.Click += new System.EventHandler(this.ImgVideoBox_Click);
            // 
            // VideoRecognitionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = global::nightOwl.Properties.Resources.background3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1685, 838);
            this.Controls.Add(this.VideoTrackBar);
            this.Controls.Add(this.ImgVideoBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.OpenFileButton);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "VideoRecognitionView";
            this.Text = "Video recognition";
            this.Load += new System.EventHandler(this.VideoRecognitionView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VideoTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgVideoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.PictureBox ImgVideoBox;
        private System.Windows.Forms.TrackBar VideoTrackBar;
    }
}

