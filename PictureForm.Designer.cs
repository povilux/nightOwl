namespace nightOwl
{
    partial class PictureForm
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
            this.components = new System.ComponentModel.Container();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.BackToCamButton = new System.Windows.Forms.Button();
            this.BackToMainButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(269, 12);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(576, 334);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // BackToCamButton
            // 
            this.BackToCamButton.Location = new System.Drawing.Point(269, 368);
            this.BackToCamButton.Name = "BackToCamButton";
            this.BackToCamButton.Size = new System.Drawing.Size(196, 39);
            this.BackToCamButton.TabIndex = 3;
            this.BackToCamButton.Text = "Back to Webcam";
            this.BackToCamButton.UseVisualStyleBackColor = true;
            this.BackToCamButton.Click += new System.EventHandler(this.BackToCamButton_Click);
            // 
            // BackToMainButton
            // 
            this.BackToMainButton.Location = new System.Drawing.Point(269, 428);
            this.BackToMainButton.Name = "BackToMainButton";
            this.BackToMainButton.Size = new System.Drawing.Size(196, 36);
            this.BackToMainButton.TabIndex = 4;
            this.BackToMainButton.Text = "Back to Main Menu";
            this.BackToMainButton.UseVisualStyleBackColor = true;
            this.BackToMainButton.Click += new System.EventHandler(this.BackToMainButton_Click);
            // 
            // PictureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 510);
            this.Controls.Add(this.BackToMainButton);
            this.Controls.Add(this.BackToCamButton);
            this.Controls.Add(this.imageBox1);
            this.Name = "PictureForm";
            this.Text = "PictureForm";
            this.Load += new System.EventHandler(this.PictureForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Button BackToCamButton;
        private System.Windows.Forms.Button BackToMainButton;
    }
}