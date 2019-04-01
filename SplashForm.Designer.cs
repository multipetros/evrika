/* Evrika: Splash form designer
 * (c) 2019, Petros Kyladitis <http://www.multipetros.gr>
 * 
 * This is free software distributed under the GNU GPL 3, for license details see at license.txt 
 * file, distributed with this program source, or see at <http://www.gnu.org/licenses/> 
 */
namespace Evrika
{
	partial class SplashForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
			this.progressBarMarquee = new System.Windows.Forms.ProgressBar();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.labelTitle = new System.Windows.Forms.Label();
			this.labelJob = new System.Windows.Forms.Label();
			this.backgroundWorkerPreload = new System.ComponentModel.BackgroundWorker();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// progressBarMarquee
			// 
			this.progressBarMarquee.Location = new System.Drawing.Point(30, 189);
			this.progressBarMarquee.Name = "progressBarMarquee";
			this.progressBarMarquee.Size = new System.Drawing.Size(302, 23);
			this.progressBarMarquee.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBarMarquee.TabIndex = 0;
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
			this.pictureBoxLogo.Location = new System.Drawing.Point(148, 27);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(64, 64);
			this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxLogo.TabIndex = 1;
			this.pictureBoxLogo.TabStop = false;
			// 
			// labelTitle
			// 
			this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
			this.labelTitle.Location = new System.Drawing.Point(30, 107);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new System.Drawing.Size(302, 23);
			this.labelTitle.TabIndex = 2;
			this.labelTitle.Text = "labelTitle";
			this.labelTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// labelJob
			// 
			this.labelJob.Location = new System.Drawing.Point(30, 154);
			this.labelJob.Name = "labelJob";
			this.labelJob.Size = new System.Drawing.Size(302, 32);
			this.labelJob.TabIndex = 3;
			this.labelJob.Text = "labelJob";
			this.labelJob.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// backgroundWorkerPreload
			// 
			this.backgroundWorkerPreload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerPreloadDoWork);
			this.backgroundWorkerPreload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerPreloadRunWorkerCompleted);
			// 
			// SplashForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(367, 240);
			this.Controls.Add(this.labelJob);
			this.Controls.Add(this.labelTitle);
			this.Controls.Add(this.pictureBoxLogo);
			this.Controls.Add(this.progressBarMarquee);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SplashForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SplashForm";
			this.Load += new System.EventHandler(this.SplashFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.ComponentModel.BackgroundWorker backgroundWorkerPreload;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.Label labelJob;
		private System.Windows.Forms.ProgressBar progressBarMarquee;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
	}
}
