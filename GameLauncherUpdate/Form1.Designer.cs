namespace GameLauncherUpdater
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.downloadProgress = new System.Windows.Forms.ProgressBar();
            this.information = new System.Windows.Forms.Label();
            this.animateMe = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.animateMe)).BeginInit();
            this.SuspendLayout();
            // 
            // downloadProgress
            // 
            this.downloadProgress.Location = new System.Drawing.Point(12, 233);
            this.downloadProgress.Name = "downloadProgress";
            this.downloadProgress.Size = new System.Drawing.Size(331, 10);
            this.downloadProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.downloadProgress.TabIndex = 0;
            // 
            // information
            // 
            this.information.AutoSize = true;
            this.information.BackColor = System.Drawing.Color.Transparent;
            this.information.ForeColor = System.Drawing.Color.Snow;
            this.information.Location = new System.Drawing.Point(9, 214);
            this.information.Name = "information";
            this.information.Size = new System.Drawing.Size(131, 13);
            this.information.TabIndex = 1;
            this.information.Text = "Checking for latest update";
            // 
            // animateMe
            // 
            this.animateMe.BackColor = System.Drawing.Color.Transparent;
            this.animateMe.Image = global::GameLauncherUpdater.Properties.Resources.icon_100;
            this.animateMe.Location = new System.Drawing.Point(12, 12);
            this.animateMe.Name = "animateMe";
            this.animateMe.Size = new System.Drawing.Size(331, 187);
            this.animateMe.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.animateMe.TabIndex = 2;
            this.animateMe.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::GameLauncherUpdater.Properties.Resources.luncher;
            this.ClientSize = new System.Drawing.Size(355, 255);
            this.Controls.Add(this.animateMe);
            this.Controls.Add(this.information);
            this.Controls.Add(this.downloadProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.animateMe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar downloadProgress;
        private System.Windows.Forms.Label information;
        private System.Windows.Forms.PictureBox animateMe;
    }
}

