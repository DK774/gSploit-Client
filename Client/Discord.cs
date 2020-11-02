using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gSploit_Client.Client
{
    public class Discord
    {
        public class RPC
        {
            public static DiscordRpcClient client;
            public async static void Init()
            {

                client = new DiscordRpcClient(GlobalVar.Discord.ClientID);
                client.OnReady += (sender, e) =>
                {
                    // MessageBox.Show("Received Ready from user {0}", e.User.Username);
                    // Passes data onto the globalvar string values so the client can utilize it
                    GlobalVar.Discord.Username = e.User.Username;
                    GlobalVar.Discord.UserID = e.User.ID;
                    //User.AvatarSize size = new User.AvatarSize();
                    //GlobalVar.Data.Discord.avatar_url = e.User.GetAvatarURL(User.AvatarFormat.PNG, size);
                };


                client.OnPresenceUpdate += (sender, e) =>
                {

                };

                client.Initialize();

                await Task.Delay(2000);

                if (client.IsInitialized) client.SetPresence(new RichPresence()
                {
                    Details = GlobalVar.Discord.Details,
                    State = GlobalVar.Discord.State,
                    Assets = new Assets()
                    {
                        LargeImageKey = "gsploit",
                        LargeImageText = $"gSploit Client V{Application.ProductVersion}",
                        SmallImageKey = "team774",
                        SmallImageText = "Developed by Team 774"
                    }
                });
            }
        }
    }
}
