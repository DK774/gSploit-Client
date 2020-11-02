using gSploit_Client.Client;
using gSploit_Client.Client.Adb;
using gSploit_Client.Client.Device;
using gSploit_Client.Client.Forms;
using gSploit_Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace gSploit_Client
{
    public partial class clientFm : Form
    {
        public clientFm()
        {
            InitializeComponent();        
            new GlobalVar(this);
        }

        public static PictureBox loadPic = new PictureBox();
        public static Panel loadPnl = new Panel();
        public static Label loadLbl = new Label();
        private async void clientFm_Load(object sender, EventArgs e)
        {
            Size = new Size(334, 350);
            StartPosition = FormStartPosition.CenterScreen;
            loadPic.Image = (Image)Resources.ResourceManager.GetObject("discordload");
            loadPic.Location = new Point(44, 84);
            loadPic.Size = new Size(246, 156);
            loadPic.SizeMode = PictureBoxSizeMode.Zoom;
            loadPic.BackColor = Color.FromArgb(33, 35, 40);

            loadLbl.Location = new Point(65, 292);
            loadLbl.Size = new Size(191, 32);
            loadLbl.ForeColor = Theme.Game.Era.Color;
            loadLbl.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);


            loadPnl.Dock = DockStyle.Fill;
            loadPnl.BackColor = Color.FromArgb(33, 35, 40);        
            
            loadPnl.Controls.Add(loadPic);
            loadPnl.Controls.Add(loadLbl);
            Controls.Add(loadPnl);

            barPnl.Hide();
            loadPnl.BringToFront();

            await Task.Delay(3000);
            new Thread(() => gSploit.Database.Authorize(GlobalVar.Discord.UserID, GlobalVar.Discord.Username)).Start();
            Handler.Client.Scan();
        }

        private void pinBtn_Click(object sender, EventArgs e)
        {
            if (!TopMost) TopMost = true;
            else if (TopMost) TopMost = false;
        }

        private void miniBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void barPnl_MouseDown(object sender, MouseEventArgs e)
        {
            frmHandler.Drag();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            homePnl.BringToFront();
            settingsPnl.SendToBack();

            homeBtn.ForeColor = Theme.Controls.selectedBtnFore;
            homeBtn.BackColor = Theme.Controls.selectedBtnBack;

            terminalBtn.ForeColor = Theme.Controls.normalBtnFore;
            terminalBtn.BackColor = Theme.Controls.normalBtnBack;

            transferBtn.ForeColor = Theme.Controls.normalBtnFore;
            transferBtn.BackColor = Theme.Controls.normalBtnBack;

            viewerBtn.ForeColor = Theme.Controls.normalBtnFore;
            viewerBtn.BackColor = Theme.Controls.normalBtnBack;

            settingsBtn.ForeColor = Theme.Controls.normalBtnFore;
            settingsBtn.BackColor = Theme.Controls.normalBtnBack;
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            if (userconsoleCheck.Checked || logclientCheck.Checked || logserverCheck.Checked || lognpcCheck.Checked || logallCheck.Checked)
            {
                if (serverDrop.selectedIndex >= 0 && scriptDrop.selectedIndex >= 0)
                {
                    GlobalVar.Client.Settings.SelectedScript = scriptDrop.selectedValue;
                    gSploit.Client.Init();
                }
            }
            else MessageBox.Show("Please select a log type before continuing!", Application.ProductName, MessageBoxButtons.OK);
        }

        private void serverDrop_onItemSelected(object sender, EventArgs e)
        {
            switch (serverDrop.selectedIndex)
            {
                case 0:
                    serverDrop.NomalColor = Theme.Game.Classic.Color;
                    serverDrop.onHoverColor = Theme.Game.Classic.Color;
                    logserverCheck.ForeColor = Theme.Game.Classic.Color;
                    logclientCheck.ForeColor = Theme.Game.Classic.Color;
                    logallCheck.ForeColor = Theme.Game.Classic.Color;
                    lognpcCheck.ForeColor = Theme.Game.Classic.Color;
                    userconsoleCheck.ForeColor = Theme.Game.Classic.Color;
                    break;
                case 1:
                    serverDrop.NomalColor = Theme.Game.Era.Color;
                    serverDrop.onHoverColor = Theme.Game.Era.Color;
                    logserverCheck.ForeColor = Theme.Game.Era.Color;
                    logclientCheck.ForeColor = Theme.Game.Era.Color;
                    logallCheck.ForeColor = Theme.Game.Era.Color;
                    lognpcCheck.ForeColor = Theme.Game.Era.Color;
                    userconsoleCheck.ForeColor = Theme.Game.Era.Color;
                    break;
                case 2:
                    serverDrop.NomalColor = Theme.Game.Zone.Color;
                    serverDrop.onHoverColor = Theme.Game.Zone.Color;
                    logserverCheck.ForeColor = Theme.Game.Zone.Color;
                    logclientCheck.ForeColor = Theme.Game.Zone.Color;
                    logallCheck.ForeColor = Theme.Game.Zone.Color;
                    lognpcCheck.ForeColor = Theme.Game.Zone.Color;
                    userconsoleCheck.ForeColor = Theme.Game.Zone.Color;
                    break;
                case 3:
                    serverDrop.NomalColor = Theme.Game.OlWest.Color;
                    serverDrop.onHoverColor = Theme.Game.OlWest.Color;
                    logserverCheck.ForeColor = Theme.Game.OlWest.Color;
                    logclientCheck.ForeColor = Theme.Game.OlWest.Color;
                    logallCheck.ForeColor = Theme.Game.OlWest.Color;
                    lognpcCheck.ForeColor = Theme.Game.OlWest.Color;
                    userconsoleCheck.ForeColor = Theme.Game.OlWest.Color;
                    break;
            }
        }

        private void scriptDrop_onItemSelected(object sender, EventArgs e)
        {
            if (scriptDrop.selectedIndex >= 0)
            {
                scriptDrop.NomalColor = Color.MediumPurple;
                scriptDrop.onHoverColor = Color.MediumPurple;
            }

            if (serverDrop.selectedIndex >= 0)
            {          
                userconsoleCheck.Cursor = Cursors.Hand;
                userconsoleCheck.Enabled = true;
                logclientCheck.Enabled = true;
                logclientCheck.Cursor = Cursors.Hand;
                logserverCheck.Enabled = true;
                logserverCheck.Cursor = Cursors.Hand;
                logallCheck.Enabled = true;
                logallCheck.Cursor = Cursors.Hand;
                lognpcCheck.Enabled = true;
                lognpcCheck.Cursor = Cursors.Hand;
                connectBtn.ForeColor = Color.White;
                connectBtn.Enabled = true;
                connectBtn.Cursor = Cursors.Hand;
            }
            else
            {
                userconsoleCheck.Cursor = Cursors.Arrow;
                userconsoleCheck.Enabled = false;
                logclientCheck.Enabled = false;
                logclientCheck.Cursor = Cursors.Arrow;
                logserverCheck.Enabled = false;
                logserverCheck.Cursor = Cursors.Arrow;
                logallCheck.Enabled = false;
                logallCheck.Cursor = Cursors.Arrow;
                lognpcCheck.Enabled = false;
                lognpcCheck.Cursor = Cursors.Arrow;
                connectBtn.ForeColor = Color.Gray;
                connectBtn.Cursor = Cursors.Arrow;
            }
        }

        private void noxCheck_OnChange(object sender, EventArgs e)
        {

        }

        private void directshellCheck_OnChange(object sender, EventArgs e)
        {

        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            homePnl.SendToBack();
            settingsPnl.BringToFront();

            homeBtn.ForeColor = Theme.Controls.normalBtnFore;
            homeBtn.BackColor = Theme.Controls.normalBtnBack;

            terminalBtn.ForeColor = Theme.Controls.normalBtnFore;
            terminalBtn.BackColor = Theme.Controls.normalBtnBack;

            transferBtn.ForeColor = Theme.Controls.normalBtnFore;
            transferBtn.BackColor = Theme.Controls.normalBtnBack;

            viewerBtn.ForeColor = Theme.Controls.normalBtnFore;
            viewerBtn.BackColor = Theme.Controls.normalBtnBack;

            settingsBtn.ForeColor = Theme.Controls.selectedBtnFore;
            settingsBtn.BackColor = Theme.Controls.selectedBtnBack;

        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            DialogResult Dialog = MessageBox.Show("Are you sure you want to reset gsploit's settings?", Application.ProductName, MessageBoxButtons.YesNo);
            if (Dialog == DialogResult.Yes && Settings.Default["NoxLocation"].ToString() != string.Empty)
            {
                MessageBox.Show("Client successfully reset!", Application.ProductName, MessageBoxButtons.OK);
                Settings.Default.Reset();
            }
        }

        private void homePnl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rescanBtn_Click(object sender, EventArgs e)
        {
            Client.Device.Handler.Client.Scan();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client.Frida.Bridge.Test();
        }

        private void transferBtn_Click(object sender, EventArgs e)
        {
            transferPnl.BringToFront();

            homeBtn.ForeColor = Theme.Controls.normalBtnFore;
            homeBtn.BackColor = Theme.Controls.normalBtnBack;

            terminalBtn.ForeColor = Theme.Controls.normalBtnFore;
            terminalBtn.BackColor = Theme.Controls.normalBtnBack;

            transferBtn.ForeColor = Theme.Controls.selectedBtnFore;
            transferBtn.BackColor = Theme.Controls.selectedBtnBack;

            viewerBtn.ForeColor = Theme.Controls.normalBtnFore;
            viewerBtn.BackColor = Theme.Controls.normalBtnBack;

            settingsBtn.ForeColor = Theme.Controls.normalBtnFore;
            settingsBtn.BackColor = Theme.Controls.normalBtnBack;
        }

        private void terminalBtn_Click(object sender, EventArgs e)
        {
            terminalPnl.BringToFront();

            homeBtn.ForeColor = Theme.Controls.normalBtnFore;
            homeBtn.BackColor = Theme.Controls.normalBtnBack;

            terminalBtn.ForeColor = Theme.Controls.selectedBtnFore;
            terminalBtn.BackColor = Theme.Controls.selectedBtnBack;

            transferBtn.ForeColor = Theme.Controls.normalBtnFore;
            transferBtn.BackColor = Theme.Controls.normalBtnBack;

            viewerBtn.ForeColor = Theme.Controls.normalBtnFore;
            viewerBtn.BackColor = Theme.Controls.normalBtnBack;

            settingsBtn.ForeColor = Theme.Controls.normalBtnFore;
            settingsBtn.BackColor = Theme.Controls.normalBtnBack;

        }

        private void clientFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                gSploit.Client.clientCmd.CloseMainWindow();
                Bridge.Logcat.clientCmd.CloseMainWindow();
                Bridge.Logcat.serverCmd.CloseMainWindow();
                Bridge.Logcat.npcCmd.CloseMainWindow();
                Bridge.Logcat.qplayCmd.CloseMainWindow();
                Bridge.Logcat.npcCmd.CloseMainWindow();

                Discord.RPC.client.Deinitialize();
                Discord.RPC.client.Dispose();
            }
            catch (Exception) { } //errors are expected if the user doesn't have cmd2 open, no point in adding code to detect either
        }

        private void NpcTBtn_Click(object sender, EventArgs e)
        {
            npcTPnl.BringToFront();

            npcTBtn.ForeColor = Theme.Controls.selectedBtnFore;
            npcTBtn.BackColor = Theme.Controls.selectedBtnBack;

            serverTBtn.ForeColor = Theme.Controls.normalBtnFore;
            serverTBtn.BackColor = Theme.Controls.normalBtnBack;

            clientTBtn.ForeColor = Theme.Controls.normalBtnFore;
            clientTBtn.BackColor = Theme.Controls.normalBtnBack;

        }

        private void serverTBtn_Click(object sender, EventArgs e)
        {
            serverTPnl.BringToFront();

            serverTBtn.ForeColor = Theme.Controls.selectedBtnFore;
            serverTBtn.BackColor = Theme.Controls.selectedBtnBack;

            npcTBtn.ForeColor = Theme.Controls.normalBtnFore;
            npcTBtn.BackColor = Theme.Controls.normalBtnBack;

            clientTBtn.ForeColor = Theme.Controls.normalBtnFore;
            clientTBtn.BackColor = Theme.Controls.normalBtnBack;

        }

        private void clientPic_MouseDown(object sender, MouseEventArgs e)
        {
            frmHandler.Drag();
        }

        private void clientTBtn_Click(object sender, EventArgs e)
        {
            clientTPnl.BringToFront();

            clientTBtn.ForeColor = Theme.Controls.selectedBtnFore;
            clientTBtn.BackColor = Theme.Controls.selectedBtnBack;

            npcTBtn.ForeColor = Theme.Controls.normalBtnFore;
            npcTBtn.BackColor = Theme.Controls.normalBtnBack;

            serverTBtn.ForeColor = Theme.Controls.normalBtnFore;
            serverTBtn.BackColor = Theme.Controls.normalBtnBack;

        }

        private void installclassicBtn_Click(object sender, EventArgs e)
        {
            new Thread(() => Bridge.Install.Graal("classic")).Start();
        }

        private void installeraBtn_Click(object sender, EventArgs e)
        {
            new Thread(() => Bridge.Install.Graal("era")).Start();
        }

        private void installzoneBtn_Click(object sender, EventArgs e)
        {
            new Thread(() => Bridge.Install.Graal("zone")).Start();
        }

        private void installolwestBtn_Click(object sender, EventArgs e)
        {
            new Thread(() => Bridge.Install.Graal("olwest")).Start();
        }

        private void clientterminalCheck_OnChange(object sender, EventArgs e)
        {
            if (clientterminalCheck.Checked) GlobalVar.Client.Settings.ClientTerminal = true;
            else GlobalVar.Client.Settings.ClientTerminal = false;
        }

        public void CheckKeyword(string word, Color color, int startIndex, string type)
        {
            // Checks for a specific string inside the console and changes the strings color to the given

            RichTextBox NullRich = new RichTextBox();
            var Rich = NullRich;
            switch (type)
            {
                case "server":
                  Rich = serverRich;
                break;
                case "client":
                  Rich = clientRich;
                break;
                case "npc":
                  Rich = npcRich; 
                break;
            }
       

            if (Rich.Text.Contains(word) && Rich != NullRich)
            {
                int index = -1;
                int selectStart =Rich.SelectionStart;

                while ((index = Rich.Text.IndexOf(word, (index + 1))) != -1)
                {
                    Rich.Select((index + startIndex), word.Length);
                    Rich.SelectionColor = color;
                    Rich.Select(selectStart, 0);
                    Rich.SelectionColor = Color.Black;
                }
            }

        }

        private void serverRich_TextChanged(object sender, EventArgs e)
        {
            CheckKeyword("gSploit Logger", Color.MediumPurple, 0, "server");
        }

        private void npcRich_TextChanged(object sender, EventArgs e)
        {
            CheckKeyword("gSploit Logger", Color.MediumPurple, 0, "npc");
        }

        private void clientRich_TextChanged(object sender, EventArgs e)
        {
            CheckKeyword("gSploit Logger", Color.MediumPurple, 0, "client");
        }

        private void savctimeClassicBtn_Click(object sender, EventArgs e)
        {        
            new Thread(() => Bridge.Install.GrabFile.CreationTime("classic")).Start();
            MessageBox.Show("Transfer Complete!", Application.ProductName, MessageBoxButtons.OK);
        }

        private void savectimeEraBtn_Click(object sender, EventArgs e)
        {
            new Thread(() => Bridge.Install.GrabFile.CreationTime("era")).Start();
            MessageBox.Show("Transfer Complete!", Application.ProductName, MessageBoxButtons.OK);
        }

        private void savctimeZoneBtn_Click(object sender, EventArgs e)
        {
            new Thread(() => Bridge.Install.GrabFile.CreationTime("zone")).Start();
            MessageBox.Show("Transfer Complete!", Application.ProductName, MessageBoxButtons.OK);
        }

        private void savctimeWestBtn_Click(object sender, EventArgs e)
        {
            new Thread(() => Bridge.Install.GrabFile.CreationTime("ol'west")).Start();
            MessageBox.Show("Transfer Complete!", Application.ProductName, MessageBoxButtons.OK);
        }
    }
}
