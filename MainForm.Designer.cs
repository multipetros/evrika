/* Evrika: Main form designer
 * (c) 2019, Petros Kyladitis <http://www.multipetros.gr>
 * 
 * This is free software distributed under the GNU GPL 3, for license details see at license.txt 
 * file, distributed with this program source, or see at <http://www.gnu.org/licenses/> 
 */
namespace Evrika
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.buttonSearch = new System.Windows.Forms.Button();
			this.comboBoxType = new System.Windows.Forms.ComboBox();
			this.checkBoxExactMatch = new System.Windows.Forms.CheckBox();
			this.textBoxFilter = new System.Windows.Forms.TextBox();
			this.comboBoxDrive = new System.Windows.Forms.ComboBox();
			this.listViewFiles = new System.Windows.Forms.ListView();
			this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderPath = new System.Windows.Forms.ColumnHeader();
			this.menuStripMain = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.langToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.greekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.menuStripMain.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(439, 3);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(75, 23);
			this.buttonSearch.TabIndex = 3;
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.ButtonSearchClick);
			// 
			// comboBoxType
			// 
			this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxType.FormattingEnabled = true;
			this.comboBoxType.Location = new System.Drawing.Point(649, 3);
			this.comboBoxType.Name = "comboBoxType";
			this.comboBoxType.Size = new System.Drawing.Size(116, 21);
			this.comboBoxType.TabIndex = 5;
			// 
			// checkBoxExactMatch
			// 
			this.checkBoxExactMatch.Location = new System.Drawing.Point(520, 3);
			this.checkBoxExactMatch.Name = "checkBoxExactMatch";
			this.checkBoxExactMatch.Size = new System.Drawing.Size(123, 24);
			this.checkBoxExactMatch.TabIndex = 4;
			this.checkBoxExactMatch.UseVisualStyleBackColor = true;
			// 
			// textBoxFilter
			// 
			this.textBoxFilter.Location = new System.Drawing.Point(64, 3);
			this.textBoxFilter.Name = "textBoxFilter";
			this.textBoxFilter.Size = new System.Drawing.Size(369, 20);
			this.textBoxFilter.TabIndex = 2;
			// 
			// comboBoxDrive
			// 
			this.comboBoxDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDrive.FormattingEnabled = true;
			this.comboBoxDrive.Location = new System.Drawing.Point(3, 3);
			this.comboBoxDrive.Name = "comboBoxDrive";
			this.comboBoxDrive.Size = new System.Drawing.Size(55, 21);
			this.comboBoxDrive.TabIndex = 1;
			// 
			// listViewFiles
			// 
			this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeaderName,
									this.columnHeaderPath});
			this.listViewFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewFiles.FullRowSelect = true;
			this.listViewFiles.GridLines = true;
			this.listViewFiles.Location = new System.Drawing.Point(3, 43);
			this.listViewFiles.Name = "listViewFiles";
			this.listViewFiles.ShowItemToolTips = true;
			this.listViewFiles.Size = new System.Drawing.Size(778, 468);
			this.listViewFiles.TabIndex = 6;
			this.listViewFiles.UseCompatibleStateImageBehavior = false;
			this.listViewFiles.View = System.Windows.Forms.View.Details;
			this.listViewFiles.DoubleClick += new System.EventHandler(this.ListViewFilesDoubleClick);
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "";
			this.columnHeaderName.Width = 152;
			// 
			// columnHeaderPath
			// 
			this.columnHeaderPath.Text = "";
			this.columnHeaderPath.Width = 600;
			// 
			// menuStripMain
			// 
			this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.langToolStripMenuItem,
									this.aboutToolStripMenuItem});
			this.menuStripMain.Location = new System.Drawing.Point(0, 0);
			this.menuStripMain.Name = "menuStripMain";
			this.menuStripMain.Size = new System.Drawing.Size(784, 24);
			this.menuStripMain.TabIndex = 0;
			this.menuStripMain.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.refreshToolStripMenuItem,
									this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "file";
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.refreshToolStripMenuItem.Text = "refresh";
			this.refreshToolStripMenuItem.MouseLeave += new System.EventHandler(this.ToolStripMenuItemMouseLeave);
			this.refreshToolStripMenuItem.MouseEnter += new System.EventHandler(this.RefreshToolStripMenuItemMouseEnter);
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItemClick);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.exitToolStripMenuItem.Text = "exit";
			this.exitToolStripMenuItem.MouseLeave += new System.EventHandler(this.ToolStripMenuItemMouseLeave);
			this.exitToolStripMenuItem.MouseEnter += new System.EventHandler(this.ExitToolStripMenuItemMouseEnter);
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
			// 
			// langToolStripMenuItem
			// 
			this.langToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.greekToolStripMenuItem,
									this.englishToolStripMenuItem});
			this.langToolStripMenuItem.Name = "langToolStripMenuItem";
			this.langToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
			this.langToolStripMenuItem.Text = "lang";
			// 
			// greekToolStripMenuItem
			// 
			this.greekToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("greekToolStripMenuItem.Image")));
			this.greekToolStripMenuItem.Name = "greekToolStripMenuItem";
			this.greekToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.greekToolStripMenuItem.Text = "Ελληνικά";
			this.greekToolStripMenuItem.Click += new System.EventHandler(this.GreekToolStripMenuItemClick);
			// 
			// englishToolStripMenuItem
			// 
			this.englishToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("englishToolStripMenuItem.Image")));
			this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
			this.englishToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.englishToolStripMenuItem.Text = "English";
			this.englishToolStripMenuItem.Click += new System.EventHandler(this.EnglishToolStripMenuItemClick);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.aboutToolStripMenuItem.Text = "about";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabel1,
									this.toolStripStatusLabel2,
									this.toolStripStatusLabel3,
									this.toolStripStatusLabel4});
			this.statusStrip1.Location = new System.Drawing.Point(0, 538);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.ShowItemToolTips = true;
			this.statusStrip1.Size = new System.Drawing.Size(784, 24);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 19);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
			this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(122, 19);
			this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(118, 19);
			this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
			// 
			// toolStripStatusLabel4
			// 
			this.toolStripStatusLabel4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
			this.toolStripStatusLabel4.Size = new System.Drawing.Size(4, 19);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.listViewFiles, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 514);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.comboBoxDrive);
			this.flowLayoutPanel1.Controls.Add(this.textBoxFilter);
			this.flowLayoutPanel1.Controls.Add(this.buttonSearch);
			this.flowLayoutPanel1.Controls.Add(this.checkBoxExactMatch);
			this.flowLayoutPanel1.Controls.Add(this.comboBoxType);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(778, 34);
			this.flowLayoutPanel1.TabIndex = 0;
			this.flowLayoutPanel1.WrapContents = false;
			// 
			// MainForm
			// 
			this.AcceptButton = this.buttonSearch;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStripMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStripMain;
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "MainForm";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormFormClosed);
			this.menuStripMain.ResumeLayout(false);
			this.menuStripMain.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem greekToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem langToolStripMenuItem;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.CheckBox checkBoxExactMatch;
		private System.Windows.Forms.TextBox textBoxFilter;
		private System.Windows.Forms.MenuStrip menuStripMain;
		private System.Windows.Forms.ListView listViewFiles;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ColumnHeader columnHeaderPath;
		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ComboBox comboBoxType;
		private System.Windows.Forms.ComboBox comboBoxDrive;
	}
}
