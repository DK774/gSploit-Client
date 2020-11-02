using gSploit_Client.Client.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gSploit_Client.Client
{
    public class GlobalVar
    {
        /* Summary: A reference to static active forms */
        public static clientFm _clientFm = null;
        //public static loginFm _loginFm = null;
        public GlobalVar(clientFm _CF/*, loginFm _LF*/)
        {
            _clientFm = _CF;
            //_loginFm = _LF;
        }


        public class Client
        {
            public class Flags
            {               
                public  class Emulator
                {
                    public class Nox
                    {
                        public static bool adbOutated;
                    } 
                }
            }
            public class Paths
            {
                public static string TransfersPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Documents\gSploit\Device Transfers";
                public static string NoxPath = $"";
                public static string ScriptsPath = $@"{Application.StartupPath}\Dependencies\gScripts\";
                public static string AdbPath = $@"{Application.StartupPath}\Dependencies\Adb";
                public static string Device = "127.0.0.1:62001";
            }

            public class Settings
            {
                public static string SelectedScript;
                public static bool FridaAlive;
                public static string Scripts;
                public static bool ClientTerminal = true;
            }

        }
        public class Discord
        {
            public static string Username;
            public static string ClientID = "762771976518631424";
            public static string State;
            public static ulong UserID;
            public static string Details;
        }
        public class Auth
        {
            public static string ConnectionString = "server=db4free.net;userid=gsprite;password=076f3bd6;database=gsprite;old guids=true;connection timeout=60;";
            public static long ActLvl;
            public static ulong DiscordID;
            public static string DiscordUser;
            public static string ActType;
        }
    }
}
