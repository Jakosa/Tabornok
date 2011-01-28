using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tabornok.Consol;
using Tabornok.Irc;
using Tabornok.BotConfig;

namespace Tabornok
{
    class Tabornok
    {
        /// <summary>
        /// Itt indul el a bot
        /// </summary>
        public Tabornok()
        {
            // Config system indítása
            new Config(@"Config.xml");
            // Consol system indítása
            new BotConsol();
            // Irc system indítása
            new IRC();
        }

        /// <summary>
        /// Leáll a bot
        /// </summary>
        ~Tabornok()
        {
            Log.Debug("Tabornok", "~Tabornok()");
        }
    }

    class ActualNick
    {
        private static string _CurrentNick = BotConfig.Conf.Nick;
        public static string CurrentNick
        {
            get { return _CurrentNick; }
            private set { _CurrentNick = value; }
        }
    }
}
