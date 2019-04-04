/* Evrika: Main form
 * (c) 2019, Petros Kyladitis <http://www.multipetros.gr>
 * 
 * This is free software distributed under the GNU GPL 3, for license details see at license.txt 
 * file, distributed with this program source, or see at <http://www.gnu.org/licenses/> 
 */
 
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Resources;
using System.Globalization;
using System.ComponentModel;
using UsnJournal;
using Multipetros.Config;

namespace Evrika{
	public partial class MainForm : Form{
		//constant strings representing resources keys
		private const string RES_FRM_TITLE = "FRM_TITLE" ;
		private const string RES_BUTTON_SEARCH = "BUTTON_SEARCH" ;
		private const string RES_OPT_FILES_FOLDERS = "OPT_FILES_FOLDERS" ;
		private const string RES_OPT_FILES_ONLY = "OPT_FILES_ONLY" ;
		private const string RES_OPT_FOLDERS_ONLY = "OPT_FOLDERS_ONLY" ;
		private const string RES_MATCH_EXACT = "MATCH_EXACT" ;
		private const string RES_MATCH_CONTAINS = "MATCH_CONTAINS" ;
		private const string RES_MATCH_STARTS = "MATCH_STARTS" ;
		private const string RES_MATCH_ENDS = "MATCH_ENDS" ;
		private const string RES_MENU_ABOUT = "MENU_ABOUT" ;
		private const string RES_MENU_FILE = "MENU_FILE" ;
		private const string RES_MENU_EXIT = "MENU_EXIT" ;
		private const string RES_MENU_LANGUANGE = "MENU_LANGUANGE" ;
		private const string RES_MENU_REFRESH = "MENU_REFRESH" ;
		private const string RES_MENU_EXIT_TIP = "MENU_EXIT_TIP" ;		
		private const string RES_MENU_REFRESH_TIP = "MENU_REFRESH_TIP" ;
		private const string RES_MENU_LANGUAGE_TIP = "MENU_LANGUAGE_TIP" ;
		private const string RES_MENU_ABOUT_TIP = "MENU_ABOUT_TIP" ;
		private const string RES_COL_ELEMENT = "COL_ELEMENT" ;
		private const string RES_COL_PATH = "COL_PATH" ;
		private const string RES_STATUS_READY = "STATUS_READY" ;
		private const string RES_STATUS_SEARCHING = "STATUS_SEARCHING" ;
		private const string RES_STATUS_TIME = "STATUS_TIME" ;
		private const string RES_STATUS_FOUND = "STATUS_FOUND" ;
		private const string RES_MSG_TITLE_ERROR = "MSG_TITLE_ERROR" ;
		private const string RES_MSG_NOFILTER = "MSG_NOFILTER" ;
		private const string RES_MSG_FILENOTFOUND = "MSG_FILENOTFOUND" ;
		private const string RES_MSG_NODRIVE = "MSG_NODRIVE" ;
		
		//available language resources
		private readonly string[] AVAILABLE_LANGS = new string[]{"en","el"} ;
		private const int LANG_EN = 0 ;
		private const int LANG_EL = 1 ;
		
		//constant strings representing registry setting key names
		private const string INI_WND_TOP = "wndtop" ;
		private const string INI_WND_LEFT = "wndleft" ;
		private const string INI_LANG = "lang" ;
		private const string INI_TYPE = "type" ;
		private const string INI_MATCH = "match" ;
		
		//init vars
		private ResourceManager resmgr ;
		private CultureInfo ci ;
		private RegistryIni ini ;
		
		public MainForm(){
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			
			resmgr = new ResourceManager(typeof(MainForm)) ;
			ini = new RegistryIni(Application.CompanyName, Application.ProductName) ;
		}		
		
		private void LoadNtfsDrives(){
			comboBoxDrive.Items.Clear() ;
			foreach(DriveInfo dInf in DriveInfo.GetDrives()){
				if(dInf.IsReady && dInf.DriveFormat == "NTFS")
					comboBoxDrive.Items.Add(dInf.Name) ;
			}
			if(comboBoxDrive.Items.Count > 0)
				comboBoxDrive.SelectedIndex = 0 ;
		}
		
		private string GetRes(string key){
			return resmgr.GetString(key, ci) ;
		}
		
		private void LoadRes(){			
			this.Text = GetRes(RES_FRM_TITLE) ;
			buttonSearch.Text = GetRes(RES_BUTTON_SEARCH) ;
			
			columnHeaderName.Text = GetRes(RES_COL_ELEMENT) ;
			columnHeaderPath.Text = GetRes(RES_COL_PATH) ;
			
			fileToolStripMenuItem.Text = GetRes(RES_MENU_FILE) ;
			exitToolStripMenuItem.Text = GetRes(RES_MENU_EXIT) ;
			langToolStripMenuItem.Text = GetRes(RES_MENU_LANGUANGE) ;
			aboutToolStripMenuItem.Text = GetRes(RES_MENU_ABOUT) ;
			refreshToolStripMenuItem.Text = GetRes(RES_MENU_REFRESH) ;
			
			int curId = comboBoxType.SelectedIndex ;
			comboBoxType.Items.Clear() ;
			comboBoxType.Items.Add(GetRes(RES_OPT_FILES_FOLDERS)) ;
			comboBoxType.Items.Add(GetRes(RES_OPT_FILES_ONLY)) ;
			comboBoxType.Items.Add(GetRes(RES_OPT_FOLDERS_ONLY)) ;
			comboBoxType.SelectedIndex = curId < 0 ? 0 : curId ;
			
			curId = comboBoxMatch.SelectedIndex ;
			comboBoxMatch.Items.Clear() ;
			comboBoxMatch.Items.Add(GetRes(RES_MATCH_EXACT)) ;
			comboBoxMatch.Items.Add(GetRes(RES_MATCH_CONTAINS)) ;
			comboBoxMatch.Items.Add(GetRes(RES_MATCH_STARTS)) ;
			comboBoxMatch.Items.Add(GetRes(RES_MATCH_ENDS)) ;
			comboBoxMatch.SelectedIndex = curId < 0 ? 0 : curId ;

			toolStripStatusLabel1.Text = GetRes(RES_STATUS_READY) ;
			toolStripStatusLabel2.Visible = false ;
			toolStripStatusLabel3.Visible = false ;
		}
		
		private void LoadSettings(){
			//load ini params and use them to init sizes, languanges, etc.
			int left ;
			int.TryParse(ini[INI_WND_LEFT], out left) ;
			if(left > 0) this.Left = left ;
			
			int top ;
			int.TryParse(ini[INI_WND_TOP], out top) ;
			if(top > 0) this.Top = top ;
			
			int type ;
			int.TryParse(ini[INI_TYPE], out type) ;
			if(type < 0 || type > 2) type = 0 ;
			comboBoxType.SelectedIndex = type ;
			
			int macth ;
			int.TryParse(ini[INI_MATCH], out macth) ;
			if(macth < 0 || macth > 3) macth = 0 ;
			comboBoxMatch.SelectedIndex = macth ;
		}
		
		private void LoadLangSetting(){
			string lang = ini[INI_LANG] ;
			if(lang != "" && (Array.IndexOf(AVAILABLE_LANGS, lang) >= 0)){
				ci = new CultureInfo(lang) ;
			}else{
				lang = CultureInfo.CurrentCulture.TwoLetterISOLanguageName ;
				if(Array.IndexOf(AVAILABLE_LANGS, lang) > -1){
					ci = CultureInfo.CurrentCulture ;
				}else{
					lang = AVAILABLE_LANGS[LANG_EN] ;
					ci = new CultureInfo(lang) ;
				}
			}
		}
		
		private void StoreSettings(){
			ini[INI_LANG] = this.ci.TwoLetterISOLanguageName ;
			ini[INI_TYPE] = comboBoxType.SelectedIndex.ToString() ;
			ini[INI_MATCH] = comboBoxMatch.SelectedIndex.ToString() ;
			ini[INI_WND_LEFT] = this.Left.ToString() ;
			ini[INI_WND_TOP] = this.Top.ToString() ;
		}
		
		void ButtonSearchClick(object sender, EventArgs e){
			if(comboBoxDrive.SelectedIndex < 0){
				MessageBox.Show(GetRes(RES_MSG_NODRIVE), GetRes(RES_MSG_TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Exclamation) ;
			}else if(textBoxFilter.Text.Length < 1){
				MessageBox.Show(GetRes(RES_MSG_NOFILTER), GetRes(RES_MSG_TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Exclamation) ;
			}else{
				toolStripStatusLabel1.Text = GetRes(RES_STATUS_SEARCHING) ;				
				TimeSpan elapsedTime ;
				DateTime startTime ;
				startTime = DateTime.Now;
				listViewFiles.Items.Clear() ;
				this.Refresh() ;
				listViewFiles.BeginUpdate() ;
				try {
					NtfsUsnJournal.SearchType sType = (NtfsUsnJournal.SearchType)comboBoxType.SelectedIndex ;
					NtfsUsnJournal.ComparisonType cType = (NtfsUsnJournal.ComparisonType)comboBoxMatch.SelectedIndex ;
					UsnJournal.FileSet[] results  ;
					NtfsUsnJournal ntfs = new NtfsUsnJournal(new DriveInfo(comboBoxDrive.SelectedItem.ToString())) ;
					NtfsUsnJournal.UsnJournalReturnCode code = ntfs.GetFilesMatchingName(out results, textBoxFilter.Text, sType, cType) ;
					if(code == NtfsUsnJournal.UsnJournalReturnCode.USN_JOURNAL_SUCCESS){
						int itemsCount = results.Length ;
						ListViewItem[] lvItems = new ListViewItem[itemsCount] ;
						for(int i=0; i<itemsCount; i++){
							lvItems[i] = new ListViewItem(new String[]{results[i].Name, results[i].Path}) ;
						}
						listViewFiles.Items.AddRange(lvItems) ;
						
					}else{
						throw new Win32Exception((int)code);
					}
				}catch(Exception exc) {
					MessageBox.Show(exc.Message, GetRes(RES_MSG_TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error) ;	
				}
				listViewFiles.EndUpdate() ;
				elapsedTime = DateTime.Now - startTime;
				toolStripStatusLabel1.Text = GetRes(RES_STATUS_READY) ;
				toolStripStatusLabel2.Visible = true ;
				toolStripStatusLabel2.Text = GetRes(RES_STATUS_TIME) + elapsedTime.Minutes.ToString("00") + ":" + elapsedTime.Seconds.ToString("00") + "." + elapsedTime.Milliseconds.ToString() ;
				toolStripStatusLabel3.Visible = true ;
				toolStripStatusLabel3.Text = GetRes(RES_STATUS_FOUND) + listViewFiles.Items.Count ;
			}
		}
		
		void ListViewFilesDoubleClick(object sender, EventArgs e){
			string file = listViewFiles.SelectedItems[0].SubItems[1].Text ;
			if(File.Exists(file) || Directory.Exists(file)){
				ProcessStartInfo info = new ProcessStartInfo() ;
				info.FileName = "explorer";
				info.Arguments = string.Format("/e, /select, \"{0}\"", file) ;
				try{
					Process.Start(info) ;
				}catch(Exception exc){
					MessageBox.Show(exc.Message, GetRes(RES_MSG_TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error) ;				
				}
			}else{
				MessageBox.Show(GetRes(RES_MSG_FILENOTFOUND), GetRes(RES_MSG_TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Exclamation) ;
			}
		}
		
		void ExitToolStripMenuItemClick(object sender, EventArgs e){
			Application.Exit() ;
		}
		
		void GreekToolStripMenuItemClick(object sender, EventArgs e){
			ci = new CultureInfo(AVAILABLE_LANGS[LANG_EL]) ;
			LoadRes() ;
		}
		
		void EnglishToolStripMenuItemClick(object sender, EventArgs e){
			ci = new CultureInfo(AVAILABLE_LANGS[LANG_EN]) ;
			LoadRes() ;
		}
		
		void AboutToolStripMenuItemClick(object sender, EventArgs e){
			new AboutForm(ci).ShowDialog() ;
		}
		
		void MainFormFormClosed(object sender, FormClosedEventArgs e){
			StoreSettings() ;
		}
		
		void MainFormLoad(object sender, EventArgs e){
			LoadLangSetting() ;
			Hide() ;
			new SplashForm(ci).ShowDialog() ;
			Show() ;
			LoadNtfsDrives() ;
			LoadRes() ;
			LoadSettings() ;
		}
		
		void RefreshToolStripMenuItemClick(object sender, EventArgs e){
			LoadNtfsDrives() ;
		}
		
		void ToolStripMenuItemMouseLeave(object sender, EventArgs e){
			toolStripStatusLabel4.Text = string.Empty ;
		}
				
		void RefreshToolStripMenuItemMouseEnter(object sender, EventArgs e){
			toolStripStatusLabel4.Text = GetRes(RES_MENU_REFRESH_TIP) ;
		}
		
		void ExitToolStripMenuItemMouseEnter(object sender, EventArgs e){
			toolStripStatusLabel4.Text = GetRes(RES_MENU_EXIT_TIP) ;
		}
		
		void LangToolStripMenuItemMouseEnter(object sender, EventArgs e){
			toolStripStatusLabel4.Text = GetRes(RES_MENU_LANGUAGE_TIP) ;
		}
		
		void AboutToolStripMenuItemMouseEnter(object sender, EventArgs e){
			toolStripStatusLabel4.Text = GetRes(RES_MENU_ABOUT_TIP) ;
		}
	}
}
