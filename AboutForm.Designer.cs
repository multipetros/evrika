/* Evrika: About form designer
 * (c) 2019, Petros Kyladitis <http://www.multipetros.gr>
 * 
 * This is free software distributed under the GNU GPL 3, for license details see at license.txt 
 * file, distributed with this program source, or see at <http://www.gnu.org/licenses/> 
 */
namespace Evrika
{
	partial class AboutForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.labelDescription = new System.Windows.Forms.Label();
			this.textBoxLicense = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.buttonShowLic = new System.Windows.Forms.Button();
			this.labelSeparator = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(64, 64);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// labelDescription
			// 
			this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
			this.labelDescription.Location = new System.Drawing.Point(82, 12);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(322, 64);
			this.labelDescription.TabIndex = 3;
			// 
			// textBoxLicense
			// 
			this.textBoxLicense.Location = new System.Drawing.Point(12, 92);
			this.textBoxLicense.Multiline = true;
			this.textBoxLicense.Name = "textBoxLicense";
			this.textBoxLicense.ReadOnly = true;
			this.textBoxLicense.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxLicense.Size = new System.Drawing.Size(393, 132);
			this.textBoxLicense.TabIndex = 5;
			// 
			// buttonOk
			// 
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonOk.Location = new System.Drawing.Point(290, 290);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(114, 26);
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.ButtonOkClick);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(12, 297);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(127, 26);
			this.linkLabel1.TabIndex = 7;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "www.multipetros.gr";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
			// 
			// buttonShowLic
			// 
			this.buttonShowLic.Location = new System.Drawing.Point(114, 230);
			this.buttonShowLic.Name = "buttonShowLic";
			this.buttonShowLic.Size = new System.Drawing.Size(181, 21);
			this.buttonShowLic.TabIndex = 8;
			this.buttonShowLic.UseVisualStyleBackColor = true;
			this.buttonShowLic.Click += new System.EventHandler(this.ButtonShowLicClick);
			// 
			// labelSeparator
			// 
			this.labelSeparator.BackColor = System.Drawing.SystemColors.Window;
			this.labelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelSeparator.Location = new System.Drawing.Point(12, 278);
			this.labelSeparator.Name = "labelSeparator";
			this.labelSeparator.Size = new System.Drawing.Size(393, 3);
			this.labelSeparator.TabIndex = 9;
			// 
			// AboutForm
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonOk;
			this.ClientSize = new System.Drawing.Size(416, 328);
			this.Controls.Add(this.labelSeparator);
			this.Controls.Add(this.buttonShowLic);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.textBoxLicense);
			this.Controls.Add(this.labelDescription);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button buttonShowLic;
		private System.Windows.Forms.Label labelSeparator;
		private System.Windows.Forms.Label labelDescription;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.TextBox textBoxLicense;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}
