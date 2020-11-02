using gSploit_Client.Client.Adb;
using gSploit_Client.Client.Device;
using gSploit_Client.Client.Forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gSploit_Client.Client
{
    public class gSploit
    {
        public static class Client
        {
            public async static void Init()
            {
                /* Summary: Checks the users enviornmental table for adb's path so scripts can be successfuly initialized, if not present the following code adds it and double checks to make sure it was 
                succssfully added. */

                if (!Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine).Contains($@"{Application.StartupPath}\Dependencies\Adb\"))
                {
                    DialogResult Dialog = MessageBox.Show("There was some essential dependencies path variables missing from your systems path table, gSploit cannot operate without them. Would you like gSploit to add them now?", Application.ProductName, MessageBoxButtons.YesNo);
                    switch (Dialog)
                    {
                        case DialogResult.Yes:
                            var Value = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine) + $@";{Application.StartupPath}\Dependencies\Adb\";
                            Environment.SetEnvironmentVariable("PATH", Value.ToString(), EnvironmentVariableTarget.Machine);

                            if (Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine).Contains($@"{Application.StartupPath}\Dependencies\Adb\")) MessageBox.Show("Dependency successfully added and configured!", Application.ProductName);
                            else MessageBox.Show("There was an issue with configuring gsploit for your machiene!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                }

                /* Summary: */
                if (GlobalVar._clientFm.noxCheck.Checked)
                {
                    if (Properties.Settings.Default["NoxLocation"].ToString() == string.Empty)
                    {
                        DialogResult Dialog2 = MessageBox.Show("Nox support is enabled but gsploit was unable to find nox's path would you like to set it now, if this was a mistake please select no and disable nox support in the settings section.", Application.ProductName, MessageBoxButtons.YesNo);
                        if (Dialog2 == DialogResult.Yes)
                        {
                            Thread T = new Thread(Handler.Emulator.Nox.SetPath);
                            T.SetApartmentState(ApartmentState.STA);
                            T.Start();
                        }
                        else return;
                    }

                    new Thread(Handler.Emulator.Nox.CheckAdb).Start();
                    await Task.Delay(1000);
                    if (!GlobalVar.Client.Flags.Emulator.Nox.adbOutated) new Thread(Bridge.Install.Frida).Start();
                }
                else
                {
                    DialogResult Dialog = MessageBox.Show("If you are currently using nox emulator I would urge you to enable 'Nox Support' in the settings before continuing!", Application.ProductName, MessageBoxButtons.OKCancel);
                    switch (Dialog)
                    {
                        case DialogResult.Yes:
                            new Thread(Bridge.Install.Frida).Start();
                            break;
                    }

                }
            }

            public static Process clientCmd = new Process();
            public static void Run(string server)
            {
                GlobalVar._clientFm.Invoke(new MethodInvoker(async delegate
                {
                    GlobalVar.Discord.Details = $"User: {GlobalVar.Discord.Username} ({GlobalVar.Auth.ActType}) | Script: {GlobalVar.Client.Settings.SelectedScript}";
                    GlobalVar.Discord.State = $@"Relaying Graal {server} Server Data";
                    Discord.RPC.client.Deinitialize();
                    Discord.RPC.Init();

                    if (GlobalVar._clientFm.userconsoleCheck.Checked && GlobalVar._clientFm.logclientCheck.Enabled)
                    {
                        //GlobalVar.Client.Paths.Device = Bridge.device.Name;

                        clientCmd.StartInfo.FileName = "cmd.exe";
                        clientCmd.StartInfo.Verb = "runas";
                        clientCmd.StartInfo.UseShellExecute = true;
                        if (GlobalVar._clientFm.serverDrop.selectedIndex == 3) server = "OlWest";
                        if (GlobalVar._clientFm.directshellCheck.Checked) clientCmd.StartInfo.Arguments = $@"/c @echo off&title gSploit Console ({GlobalVar.Client.Settings.SelectedScript})&cls&adb -s {GlobalVar.Client.Paths.Device} shell ""chmod 755/data/local/tmp/server""&adb -s {GlobalVar.Client.Paths.Device} shell ""/data/local/tmp/server &""& cd {Application.StartupPath}\Dependencies\gScripts&frida -U com.quattroplay.Graal{server} -l ./{GlobalVar.Client.Settings.SelectedScript} --eval '%resume' --eval '%reload'&echo.&echo Frida server stopped unexpectedly.&echo.&pause";
                        else clientCmd.StartInfo.Arguments = $@"/c @echo off&title gSploit Console ({GlobalVar.Client.Settings.SelectedScript})&cls&adb shell ""chmod 755 /data/local/tmp/server""&adb shell ""/data/local/tmp/server &""& cd {Application.StartupPath}\Dependencies\gScripts&frida -U com.quattroplay.Graal{server} -l ./{GlobalVar.Client.Settings.SelectedScript} --eval '%resume' --eval '%reload'&echo.&echo Frida server stopped unexpectedly.&echo.&pause";
                        try
                        {
                            if (!clientCmd.Responding) clientCmd.Start();
                        }
                        catch (Exception)
                        {
                            if (GlobalVar._clientFm.multiscriptCheck.Checked) clientCmd.Start();
                            else
                            {
                                try
                                {
                                    clientCmd.CloseMainWindow();
                                }
                                catch (Exception) { }
                                clientCmd.Start();
                            }

                        }
                    }

                    if (GlobalVar._clientFm.logclientCheck.Checked && GlobalVar._clientFm.logclientCheck.Enabled) new Thread(() => Bridge.Logcat.Log("Client")).Start();
                    if (GlobalVar._clientFm.logallCheck.Checked && GlobalVar._clientFm.logallCheck.Enabled) new Thread(() => Bridge.Logcat.Log("qPlay")).Start();
                    if (GlobalVar._clientFm.lognpcCheck.Checked && GlobalVar._clientFm.lognpcCheck.Enabled) new Thread(() => Bridge.Logcat.Log("ServerNpc")).Start();
                    if (GlobalVar._clientFm.logserverCheck.Checked && GlobalVar._clientFm.logserverCheck.Enabled) new Thread(() => Bridge.Logcat.Log("Server")).Start();
                    GlobalVar._clientFm.connectBtn.Text = "Connecting!";
                    GlobalVar.Client.Settings.FridaAlive = true;
                    await Task.Delay(2000);
                    GlobalVar._clientFm.connectBtn.Text = "Connected";
                    await Task.Delay(2000);
                    GlobalVar._clientFm.connectBtn.Text = "Connect";
                }));
            }

        }
        public static class Database
        {
            public static void Authorize(ulong _DiscordID, string _DiscordUser)
            {
                bool _ = false;
                if (_)
                {
                    GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                    {
                        GlobalVar.Discord.Details = $"User: {GlobalVar.Discord.Username} ({GlobalVar.Auth.ActType})";
                        GlobalVar.Discord.State = "Currently Idle";
                        Discord.RPC.client.Deinitialize();
                        Discord.RPC.Init();

                        // Default clientFm properties 
                        GlobalVar._clientFm.Size = new Size(716, 465);
                        GlobalVar._clientFm.homePnl.BringToFront();
                        GlobalVar._clientFm.barPnl.Show();
                        clientFm.loadPic.Dispose();
                        clientFm.loadLbl.Dispose();
                        clientFm.loadPnl.Dispose();
                        GlobalVar._clientFm.tagLbl.Text = GlobalVar.Discord.Details;
                    }));
                }
                else
                {
                    MySqlConnection Connection = new MySqlConnection(GlobalVar.Auth.ConnectionString);
                    Connection.Open();
                    MySqlDataReader ConnectionData = new MySqlCommand($"SELECT * FROM accounts WHERE accounts.id = {_DiscordID}", Connection).ExecuteReader();
                    while (ConnectionData.Read()) GlobalVar.Auth.ActLvl = ConnectionData.GetInt64(1);
                    GlobalVar.Auth.DiscordUser = _DiscordUser;
                    GlobalVar.Auth.DiscordID = _DiscordID;
                    GlobalVar.Auth.ActType = GlobalVar.Auth.ActLvl.ToString().Replace("-1", "None").Replace("0", "Guest").Replace("1", "Member");
                    if (GlobalVar.Auth.ActLvl >= 0 && GlobalVar.Auth.ActType != string.Empty)
                    {
                        GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                        {
                            GlobalVar.Discord.Details = $"User: {GlobalVar.Discord.Username} ({GlobalVar.Auth.ActType})";
                            GlobalVar.Discord.State = "Currently Idle";
                            Discord.RPC.client.Deinitialize();
                            Discord.RPC.Init();

                            // Default clientFm properties 
                            GlobalVar._clientFm.Size = new Size(716, 465);
                            GlobalVar._clientFm.homePnl.BringToFront();
                            GlobalVar._clientFm.barPnl.Show();
                            clientFm.loadPic.Dispose();
                            clientFm.loadLbl.Dispose();
                            clientFm.loadPnl.Dispose();
                            GlobalVar._clientFm.tagLbl.Text = GlobalVar.Discord.Details;
                            GlobalVar._clientFm.StartPosition = FormStartPosition.CenterScreen;
                        }));

                        if (!Directory.Exists(GlobalVar.Client.Paths.ScriptsPath)) Directory.CreateDirectory(GlobalVar.Client.Paths.ScriptsPath);
                        new Thread(Handler.Client.Scan).Start(); 
                    }
                    else
                    {
                        GlobalVar._clientFm.Invoke(new MethodInvoker(async delegate
                        {
                            string loadString = "You don\'t have permission\r\n    to access gSploit ";
                            clientFm.loadLbl.Text = loadString + "(5)";
                            await Task.Delay(1000);
                            clientFm.loadLbl.Text = loadString + "(4)";
                            await Task.Delay(1000);
                            clientFm.loadLbl.Text = loadString + "(3)";
                            await Task.Delay(1000);
                            clientFm.loadLbl.Text = loadString + "(2)";
                            await Task.Delay(1000);
                            clientFm.loadLbl.Text = loadString + "(1)";
                            await Task.Delay(1000);
                            Environment.Exit(0);
                        }));
                    }
                }

            }
        }
            
    }
}
