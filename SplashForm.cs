/* Evrika: Splash form
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
using System.Collections.Generic;
using System.IO;
using PInvoke;
using UsnJournal;

namespace Evrika{
	public partial class SplashForm : Form{
		//constant strings representing resources keys
		private const string RES_LBL_TITLE = "LBL_TITLE" ;
		private const string RES_LBL_JOB = "LBL_JOB" ;

		//init vars
		private ResourceManager resmgr ;
		private CultureInfo ci ;
		
		public SplashForm(CultureInfo ci){
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			
			resmgr = new ResourceManager(typeof(SplashForm)) ;
			this.ci = ci ;
			LoadRes() ;
		}
		
		private string GetRes(string key){
			return resmgr.GetString(key, ci) ;
		}
		
		private void LoadRes(){			
			labelJob.Text = GetRes(RES_LBL_JOB) ;
			labelTitle.Text = GetRes(RES_LBL_TITLE) ;
		}
		
		void SplashFormLoad(object sender, EventArgs e){
			backgroundWorkerPreload.RunWorkerAsync() ;
		}
		
		void BackgroundWorkerPreloadDoWork(object sender, System.ComponentModel.DoWorkEventArgs e){			
			string appExe = Path.GetFileName(Application.ExecutablePath) ;
			string drive = null ;			
			try{
			foreach(DriveInfo dInf in DriveInfo.GetDrives()){
				if(dInf.IsReady && dInf.DriveFormat == "NTFS"){
					drive = dInf.Name.Substring(0, 1) ;
					break ;
				}
			}
				//List<NtfsWinApi.UsnEntry> usnList  = new List<NtfsWinApi.UsnEntry>() ;
				UsnJournal.FileSet[] results  ;
				NtfsUsnJournal ntfs = new NtfsUsnJournal(new DriveInfo(drive)) ;
				//NtfsUsnJournal.UsnJournalReturnCode code = ntfs.GetFilesMatchingName(out results, appExe, NtfsUsnJournal.SearchType.FILES_ONLY, true) ;
				NtfsUsnJournal.UsnJournalReturnCode code = ntfs.GetFilesMatchingName(out results, appExe, NtfsUsnJournal.SearchType.FILES_ONLY, NtfsUsnJournal.ComparisonType.EXACT_MATCH) ;
			}catch(Exception){}
		}
		
		void BackgroundWorkerPreloadRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e){			
			Close() ;
		}
	}
}
