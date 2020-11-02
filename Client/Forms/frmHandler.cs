using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gSploit_Client.Client.Forms
{
    public class frmHandler
    {
        public static void Drag()
        {
            if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
            {
                Win32.ReleaseCapture();
                Win32.SendMessage(GlobalVar._clientFm.Handle, Win32.WM_NCLBUTTONDOWN, Win32.HT_CAPTION, 0);
            }
        }
    }
}
