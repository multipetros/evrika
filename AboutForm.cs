/* Evrika: About form
 * (c) 2019, Petros Kyladitis <http://www.multipetros.gr>
 * 
 * This is free software distributed under the GNU GPL 3, for license details see at license.txt 
 * file, distributed with this program source, or see at <http://www.gnu.org/licenses/> 
 */
 
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using System.Diagnostics;

namespace Evrika{
	public partial class AboutForm : Form{
		//constant strings representing resources keys
		private const string RES_FRM_TITLE = "FRM_TITLE" ;
		private const string RES_LABEL_DESCRIPT = "LABEL_DESCRIPT" ;
		private const string RES_TEXT_LICENSE = "TEXT_LICENSE" ;
		private const string RES_BTN_SHOWLIC = "BTN_SHOWLIC" ;
		private const string RES_MSG_TITLE_ERROR = "MSG_TITLE_ERROR" ;
		private const string RES_MSG_FILENOTFOUND = "MSG_FILENOTFOUND" ;
		
		//init vars
		private ResourceManager resmgr ;
		private CultureInfo ci ;
		
		public AboutForm(CultureInfo ci){
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			
			resmgr = new ResourceManager(typeof(AboutForm)) ;
			this.ci = ci ;
			LoadRes() ;
			
			LinkLabel.Link mySite = new LinkLabel.Link();
	  	 	mySite.LinkData = "http://www.multipetros.gr/";
	  		linkLabel1.Links.Add(mySite);
		}		
		
		private string GetRes(string key){
			return resmgr.GetString(key, ci) ;
		}
		
		private void LoadRes(){
			this.Text = GetRes(RES_FRM_TITLE) ;
			labelDescription.Text = GetRes(RES_LABEL_DESCRIPT) ;
			textBoxLicense.Text = GetRes(RES_TEXT_LICENSE) ;
			buttonShowLic.Text = GetRes(RES_BTN_SHOWLIC) ;
		}
		
		void ButtonOkClick(object sender, EventArgs e){
			this.DialogResult = DialogResult.OK ;
			this.Close() ;
		}
		
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
			Process.Start(e.Link.LinkData as string);
		}
		
		void ButtonShowLicClick(object sender, EventArgs e){
			string file = "license.txt" ;
			if(System.IO.File.Exists(file) || System.IO.Directory.Exists(file)){
				ProcessStartInfo info = new ProcessStartInfo() ;
				info.FileName = "notepad";
				info.Arguments = file ;
				try{
					Process.Start(info) ;
				}catch(Exception exc){
					MessageBox.Show(exc.Message, GetRes(RES_MSG_TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error) ;				
				}
			}else{
				if(MessageBox.Show(GetRes(RES_MSG_FILENOTFOUND), GetRes(RES_MSG_TITLE_ERROR), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes){
					try{
						Process.Start("http://www.gnu.org/licenses/gpl-3.0.html") ;
					}catch(Exception exc){
						MessageBox.Show(exc.Message, GetRes(RES_MSG_TITLE_ERROR), MessageBoxButtons.OK, MessageBoxIcon.Error) ;				
					}
				}
			}
		}
	}
}
