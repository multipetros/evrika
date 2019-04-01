/* Evrika: Program Entry Point
 * (c) 2019, Petros Kyladitis <http://www.multipetros.gr>
 * 
 * This is free software distributed under the GNU GPL 3, for license details see at license.txt 
 * file, distributed with this program source, or see at <http://www.gnu.org/licenses/> 
 */
 
using System;
using System.Windows.Forms;

namespace Evrika{
	internal sealed class Program{		
		/// Program entry point.
		[STAThread]
		private static void Main(string[] args){
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//new SplashForm().ShowDialog() ;
			Application.Run(new MainForm());
		}		
	}
}
