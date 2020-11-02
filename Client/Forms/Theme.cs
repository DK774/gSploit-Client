using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSploit_Client.Client.Forms
{
    public class Theme
    {
        public class Game
        {
            public class Classic
            {
                public static Color Color = Color.FromArgb(128, 255, 128);
            }

            public class Era
            {
                public static Color Color = Color.FromArgb(255, 128, 128);
            }

            public class Zone
            {
                public static Color Color = Color.FromArgb(128, 255, 255);
            }

            public class OlWest
            {
                public static Color Color = Color.FromArgb(255, 255, 128);
            }
        }

        public class Controls
        {
            public static Color selectedBtnFore = Color.Gray;
            public static Color selectedBtnBack = Color.FromArgb(47, 49, 54);

            public static Color normalBtnFore = Color.White;
            public static Color normalBtnBack = Color.FromArgb(64, 68, 75);
        }
    }
}
