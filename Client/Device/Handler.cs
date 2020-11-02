using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gSploit_Client.Client.Device
{
    public class Handler
    {
        public class Client
        {
            public static void Scan()
            {
                GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                {
                    GlobalVar._clientFm.scriptDrop.Clear();
                    DirectoryInfo ScriptsPath = new DirectoryInfo(GlobalVar.Client.Paths.ScriptsPath);
                    FileInfo[] Scripts = ScriptsPath.GetFiles("*.js");
                    foreach (FileInfo Script in Scripts)
                    {
                        GlobalVar.Client.Settings.Scripts = ", " + Script.Name;
                        GlobalVar._clientFm.scriptDrop.AddItem(Script.Name);
                    }
                }));
            }
        }

        public class Emulator
        {
            public class Nox
            {
                public static void SetPath()
                {

                    if (Properties.Settings.Default["NoxLocation"].ToString() == string.Empty)
                    {
                        FolderBrowserDialog diag = new FolderBrowserDialog();
                        diag.Description = "Select a folder in which to save your workspace...";
                        diag.SelectedPath = Application.StartupPath;

                        if (DialogResult.OK == diag.ShowDialog())
                        {
                            Properties.Settings.Default["NoxLocation"] = diag.SelectedPath.ToString(); ;
                            Properties.Settings.Default.Save();
                        }
                    }
                }


                /* Summary: Silently or promptly sets the default detected location of Nox Player */
                public static void CheckDefaultPath(bool Silent)
                {
                    if (Properties.Settings.Default["NoxLocation"].ToString() == string.Empty)
                    {
                        // I should probably have it swing through and check an array of locations on maybe possible different drives, if more than one path exists with the necessary files a return gets
                        // called meaning the user would have to manually choose the path
                        var DefaultPath = "";
                        if (File.Exists(DefaultPath))
                        {

                            switch (Silent)
                            {
                                // Sets the default nox location with a user prompt allowing or denying the supposed default path
                                case true:
                                    DialogResult Dialog = MessageBox.Show("gSploit has found an existing Nox Player path, would you like to set it as the default path?", Application.ProductName, MessageBoxButtons.YesNo);
                                    if (Dialog == DialogResult.Yes)
                                    {
                                        Properties.Settings.Default["NoxLocation"] = DefaultPath;
                                        Properties.Settings.Default.Save();
                                    }
                                    else return;
                                    break;
                                // Sets the default nox path silently not prompting the user, this can be redefined/changed in the settings tab
                                case false:
                                    Properties.Settings.Default["NoxLocation"] = DefaultPath;
                                    Properties.Settings.Default.Save();
                                    break;
                            }

                        }
                    }
                    else return; // just in case
                }

                public static void UpdateAdb()
                {

                    if (Properties.Settings.Default["NoxLocation"].ToString() != string.Empty)
                    {
                        try
                        {
                            if (File.Exists($@"{Properties.Settings.Default["NoxLocation"]}\bin\adb.exe")) File.Delete($@"{Properties.Settings.Default["NoxLocation"]}\bin\adb.exe");
                            if (File.Exists($@"{Properties.Settings.Default["NoxLocation"]}\bin\nox_adb.exe")) File.Delete($@"{Properties.Settings.Default["NoxLocation"]}\bin\nox_adb.exe");
                            File.Copy($@"{Application.StartupPath}\Dependencies\Adb\adb.exe", $@"{Properties.Settings.Default["NoxLocation"]}\bin\adb.exe");
                            File.Copy($@"{Application.StartupPath}\Dependencies\Adb\adb.exe", $@"{Properties.Settings.Default["NoxLocation"]}\bin\nox_adb.exe");

                            long length = new FileInfo($@"{Properties.Settings.Default["NoxLocation"]}\bin\adb.exe").Length;
                            if (length == 4686848)
                            {
                                GlobalVar.Client.Flags.Emulator.Nox.adbOutated = false;
                                MessageBox.Show("Nox's adb was updated successfully!", Application.ProductName);
                                return;
                            }

                        }
                        catch (Exception Ex)
                        {
                            if (Ex.ToString().Contains("is denied"))
                            {
                                DialogResult Dialog = MessageBox.Show("There was an issue updating adb please make sure nox is closed before continuing, when you're ready please select ok!", Application.ProductName, MessageBoxButtons.OKCancel);
                                switch (Dialog)
                                {
                                    case DialogResult.OK:
                                        GlobalVar.Client.Flags.Emulator.Nox.adbOutated = true;
                                        new Thread(UpdateAdb).Start();
                                        break;
                                }
                            }
                            else
                            {
                                MessageBox.Show($"There was an issue updating nox! \n\n Error: {Ex}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                }

                /* Summary: Checks the adb file size to ensure its the latest version as the latest has the bigger file size, if not copy the adb from gsploit which is the latest version */
                public static void CheckAdb()  
                {
                    try
                    {
                        long length = new FileInfo($@"{Properties.Settings.Default["NoxLocation"]}\bin\adb.exe").Length;
                        long _length = new FileInfo($@"{Properties.Settings.Default["NoxLocation"]}\bin\nox_adb.exe").Length;
                        if (length < 4686848 && _length < 4686848)
                        {
                            GlobalVar.Client.Flags.Emulator.Nox.adbOutated = true;
                            DialogResult Dialog = MessageBox.Show("The current version of Adb in nox is currently outdated, an updated version is required for gSploit to function properly would you like to update it?", "gSploit Client", MessageBoxButtons.YesNo);
                            switch (Dialog)
                            {
                                case DialogResult.Yes:
                                    GlobalVar.Client.Flags.Emulator.Nox.adbOutated = true;
                                    new Thread(UpdateAdb).Start();
                                    break;
                                case DialogResult.No:
                                    GlobalVar.Client.Flags.Emulator.Nox.adbOutated = false;
                                    break;
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        if (Ex.ToString().Contains("Could not find file"))
                        {
                            DialogResult Dialog = MessageBox.Show("Adb was not detected in nox, would you like to install it?", Application.ProductName, MessageBoxButtons.YesNo);
                            switch (Dialog)
                            {
                                case DialogResult.Yes:
                                    GlobalVar.Client.Flags.Emulator.Nox.adbOutated = true;
                                    new Thread(UpdateAdb).Start(); 
                                    break;
                            }
                        }
                        else MessageBox.Show($"An unexpected error occured \n\n Error: {Ex}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }



                }
            }
        }
    }
}
