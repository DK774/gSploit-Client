using Microsoft.VisualBasic.FileIO;
using SharpAdbClient;
using SharpAdbClient.DeviceCommands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gSploit_Client.Client.Adb
{
    public class Bridge
    {
        public static DeviceData device = AdbClient.Instance.GetDevices().First();
        public static ConsoleOutputReceiver receiver = new ConsoleOutputReceiver();

        public class Logcat
        {
            public static Process clientCmd = new Process();
            public static Process serverCmd = new Process();
            public static Process npcCmd = new Process();
            public static Process servernpcCmd = new Process();
            public static Process qplayCmd = new Process();
            public static Process customCmd = new Process();

            public static void Log(string type)
            {     
                GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                {
                    switch (type)
                    {
                        case "Server":
                            try { serverCmd.CloseMainWindow(); }
                            catch (Exception) { }
                            serverCmd.StartInfo.FileName = "cmd.exe";
                            serverCmd.StartInfo.Arguments = $@"/c @echo off&title gSploit {type} Console&cls&adb shell ""logcat | grep 'serverside,'""&echo.&pause";

                            if (GlobalVar.Client.Settings.ClientTerminal)
                            {

                                serverCmd.StartInfo.UseShellExecute = false;
                                serverCmd.StartInfo.CreateNoWindow = true;

                                serverCmd.OutputDataReceived += serverCmd_OnOutputDataReceived;
                                serverCmd.StartInfo.RedirectStandardOutput = true;           
                                serverCmd.Start();

                                

                                try
                                {
                                    serverCmd.BeginOutputReadLine();
                                }
                                catch(Exception) { }

                            }
                            else
                            {
                                serverCmd.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                                serverCmd.StartInfo.UseShellExecute = true;
                                serverCmd.StartInfo.CreateNoWindow = false;
                                serverCmd.StartInfo.RedirectStandardOutput = false;
                                serverCmd.Start();
                            }

                            break;
                        case "Client":
                            try { clientCmd.CloseMainWindow(); }
                            catch (Exception) { }
                            clientCmd.StartInfo.FileName = "cmd.exe";
                            clientCmd.StartInfo.Arguments = $@"/c @echo off&title gSploit {type} Console&cls&adb shell ""logcat | grep 'clientside,'""&echo.&pause";

                            if (GlobalVar.Client.Settings.ClientTerminal)
                            {

                                clientCmd.StartInfo.UseShellExecute = false;
                                clientCmd.StartInfo.CreateNoWindow = true;

                                clientCmd.OutputDataReceived += clientCmd_OnOutputDataReceived;
                                clientCmd.StartInfo.RedirectStandardOutput = true;
                                clientCmd.Start();



                                try
                                {
                                    clientCmd.BeginOutputReadLine();
                                }
                                catch (Exception) { }

                            }
                            else
                            {
                                clientCmd.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                                clientCmd.StartInfo.UseShellExecute = true;
                                clientCmd.StartInfo.CreateNoWindow = false;
                                clientCmd.StartInfo.RedirectStandardOutput = false;
                                clientCmd.Start();
                            }
                            break;
                        case "qPlay":
                            try { qplayCmd.CloseMainWindow(); }
                            catch (Exception) { }
                            qplayCmd.StartInfo.FileName = "cmd.exe";
                            qplayCmd.StartInfo.Arguments = $@"/c @echo off&title gSploit {type} Console&cls&adb shell ""logcat | grep 'all,'""&echo.&pause";

                            if (GlobalVar.Client.Settings.ClientTerminal)
                            {

                                qplayCmd.StartInfo.UseShellExecute = false;
                                qplayCmd.StartInfo.CreateNoWindow = true;

                                qplayCmd.OutputDataReceived += qplayCmd_OnOutputDataReceived;
                                qplayCmd.StartInfo.RedirectStandardOutput = true;
                                qplayCmd.Start();



                                try
                                {
                                   qplayCmd.BeginOutputReadLine();
                                }
                                catch (Exception) { }

                            }
                            else
                            {
                                qplayCmd.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                                qplayCmd.StartInfo.UseShellExecute = true;
                                qplayCmd.StartInfo.CreateNoWindow = false;
                                qplayCmd.StartInfo.RedirectStandardOutput = false;
                                qplayCmd.Start();
                            }
                            break;
                        case "ServerNpc":
                            try { npcCmd.CloseMainWindow(); }
                            catch (Exception) { }
                            npcCmd.StartInfo.FileName = "cmd.exe";
                            npcCmd.StartInfo.Arguments = $@"/c @echo off&title gSploit {type} Console&cls&adb shell ""logcat | grep 'npc,'""&echo.&pause";

                            if (GlobalVar.Client.Settings.ClientTerminal)
                            {

                                npcCmd.StartInfo.UseShellExecute = false;
                                npcCmd.StartInfo.CreateNoWindow = true;

                                npcCmd.OutputDataReceived += npcCmd_OnOutputDataReceived;
                                npcCmd.StartInfo.RedirectStandardOutput = true;
                                npcCmd.Start();

                                try
                                {
                                   npcCmd.BeginOutputReadLine();
                                }
                                catch (Exception) { }

                            }
                            else
                            {
                                npcCmd.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                                npcCmd.StartInfo.UseShellExecute = true;
                                npcCmd.StartInfo.CreateNoWindow = false;
                                npcCmd.StartInfo.RedirectStandardOutput = false;
                                npcCmd.Start();
                            }
                            break;
                    }
                }));
            }

            private static void qplayCmd_OnOutputDataReceived(object sender, DataReceivedEventArgs e)
            {
              
            }

            private static void serverCmd_OnOutputDataReceived(object sender, DataReceivedEventArgs e)
            {
                GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                {
                    if (GlobalVar.Client.Settings.ClientTerminal)
                    {
                        var con = GlobalVar._clientFm.serverRich;
                        con.AppendText(e.Data + Environment.NewLine);
                        con.Text = con.Text.Replace("I/qplay", "[gSploit Logger]:");
                        con.Text = con.Text.Replace("( 2955):      ", string.Empty);
                        con.SelectionStart = con.Text.Length;
                        con.ScrollToCaret();
                    }
                }));
            }

            public static void clientCmd_OnOutputDataReceived(object sender, DataReceivedEventArgs e)
            {
                GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                {
                    if (GlobalVar.Client.Settings.ClientTerminal)
                    {
                        var con = GlobalVar._clientFm.clientRich;
                        con.AppendText(e.Data + Environment.NewLine);
                        con.Text = con.Text.Replace("I/qplay", "[gSploit Logger]:");
                        con.Text = con.Text.Replace("( 2955):      ", string.Empty);
                        con.SelectionStart = con.Text.Length;
                        con.ScrollToCaret();
                    }
                }));
            }

            public static void npcCmd_OnOutputDataReceived(object sender, DataReceivedEventArgs e)
            {
                GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                {
                    if (GlobalVar.Client.Settings.ClientTerminal)
                    {
                        var con = GlobalVar._clientFm.npcRich;
                        con.AppendText(e.Data + Environment.NewLine);
                        con.Text = con.Text.Replace("I/qplay", "[gSploit Logger]:");
                        con.Text = con.Text.Replace("( 2955):      ", string.Empty);
                        con.SelectionStart = con.Text.Length;
                        con.ScrollToCaret();
                    }
                }));
            }


        }

        public class Install
        {
            public static void Ida()  
            {

                AdbClient.Instance.ExecuteRemoteCommand("find 'data/local/tmp/server'", device, receiver);
                var Result = receiver.ToString();
                if (!Result.Contains("such file"))
                {
                    GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                    {
                        gSploit.Client.Run(GlobalVar._clientFm.serverDrop.selectedValue);
                    }));
                }
                else
                {
                    try
                    {
                        DialogResult firstrunDialog = MessageBox.Show("An essential dependency is not installed on your device that is required for gSploit to run, Would you like to install it now?", Application.ProductName, MessageBoxButtons.YesNo);
                        if (firstrunDialog == DialogResult.Yes)
                        {
                            using (SyncService service = new SyncService(new AdbSocket(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)), device))
                            using (Stream stream = File.OpenRead($@"{Application.StartupPath}\Dependencies\Transfers\server"))
                            {
                                service.Push(stream, "data/local/tmp/server", 444, DateTime.Now, null, CancellationToken.None);
                            }

                            new Thread(Frida).Start();
                        }
                        else return;
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show($"There was an issue with connecting to your device, Please make sure the device is running and is unlocked before continuing. \n\n Error: {Ex} ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }



            public static void Graal(string gServer) // Installs graal to the android device/emu
            {
                PackageManager manager = new PackageManager(device);
                try
                {
                    switch (gServer.ToLower())
                    {
                        case "classic":
                            manager.InstallPackage($"{Application.StartupPath}/Dependencies/Transfers/Classic.apk", reinstall: false);
                            break;

                        case "era":
                            manager.InstallPackage($"{Application.StartupPath}/Dependencies/Transfers/Era.apk", reinstall: false);
                            break;

                        case "zone":
                            manager.InstallPackage($"{Application.StartupPath}/Dependencies/Transfers/Zone.apk", reinstall: false);
                            break;

                        case "olwest":
                            manager.InstallPackage($"{Application.StartupPath}/Dependencies/Transfers/OlWest.apk", reinstall: false);
                            break;

                    }
                }
                catch (Exception Ex)
                {
                    if (Ex.ToString().Contains("INSTALL_FAILED_ALREADY_EXISTS")) MessageBox.Show("Instillation failed, Application already installed!", "gSploit Client");
                    else MessageBox.Show($"Fatel Error: {Ex} ", "gSploit Client");
                    return;
                }

                MessageBox.Show("Application Installed!", Application.ProductName);
            }

            public class GrabFile
            {
                public static void Lib(string gServer) // Transfers libqplay from the android device/emu to your machiene
                {
                    using (SyncService service = new SyncService(new AdbSocket(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)), device))
                    using (Stream stream = File.OpenWrite(GlobalVar.Client.Paths.TransfersPath))
                    {

                        switch (gServer.ToLower())
                        {
                            case "classic":
                                service.Pull("/data/data/com.quattroplay.GraalClassic/lib/libqplay.so", stream, null, CancellationToken.None);
                                break;

                            case "era":
                                service.Pull("/data/data/com.quattroplay.GraalClassic/lib/libqplay.so", stream, null, CancellationToken.None);
                                break;

                            case "zone":
                                service.Pull("/data/data/com.quattroplay.GraalClassic/lib/libqplay.so", stream, null, CancellationToken.None);
                                break;

                            case "olwest":
                                service.Pull("/data/data/com.quattroplay.GraalClassic/lib/libqplay.so", stream, null, CancellationToken.None);
                                break;
                        }
                    }
                }

                public static void CreationTime(string Server)
                {
                    using (SyncService service = new SyncService(new AdbSocket(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)), device))
                    using (Stream stream = File.OpenWrite(GlobalVar.Client.Paths.TransfersPath))
                    {
                        service.Pull($"/data/data/com.quattroplay.Graal{Server}/files/creationtime.dat", stream, null, CancellationToken.None);
                        DateTime Date = DateTime.Now;
                        FileSystem.RenameFile($"{GlobalVar.Client.Paths.TransfersPath}creationtime.so", $"CreationTime({Date.Day}-{Date.Month}-{Date.Year})({Date.Hour}-{Date.Minute}).so");
                    }
                }

            }

            public class DeleteFile
            {
                public static void CreationTime(string Server)
                {
                    AdbClient.Instance.ExecuteRemoteCommand($"adb shell rm -f /data/data/com.quattroplay.Graal{Server}/files/creationtime.dat", device, receiver);
                }
            }

        }

    }

}
