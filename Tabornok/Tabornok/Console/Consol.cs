/*
 <Tabornok - IRC bot>
 Copyright (C) <2011>  <Jackneill>
 Copyright (C) <2011>  <Megaxxx>

 This file is part of Tabornok.
 
 This program is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see <http://www.gnu.org/licenses/>.

 Ez a fájl Tabornok része.
 
 Ez a program szabad szoftver; terjeszthető illetve módosítható a 
 Free Software Foundation által kiadott GNU General Public License
 dokumentumában leírtak; akár a licenc 3-as, akár (tetszőleges) későbbi 
 változata szerint.

 Ez a program abban a reményben kerül közreadásra, hogy hasznos lesz, 
 de minden egyéb GARANCIA NÉLKÜL, az ELADHATÓSÁGRA vagy VALAMELY CÉLRA 
 VALÓ ALKALMAZHATÓSÁGRA való származtatott garanciát is beleértve. 
 További részleteket a GNU General Public License tartalmaz.

 A felhasználónak a programmal együtt meg kell kapnia a GNU General 
 Public License egy példányát; ha mégsem kapta meg, akkor
 tekintse meg a <http://www.gnu.org/licenses/> oldalon.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using Tabornok.Irc;
using Tabornok.IrcHandler;

namespace Tabornok.Consol
{
    class BotConsol
    {
        private static bool _ConsoleLog = false;
        public static bool ConsoleLog
        {
            get { return _ConsoleLog; }
            set { _ConsoleLog = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public BotConsol()
        {
            Thread ConsoleThread = new Thread(new ThreadStart(ReadConsole));
            ConsoleThread.Start();

            Log.Success("Console", "Console elindult");
        }

        /// <summary>
        /// 
        /// </summary>
        ~BotConsol()
        {
            Log.Debug("Console", "~Consol()");
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReadConsole()
        {
            try
            {
                string command;

                while (true)
                {
                    command = Console.ReadLine();

                    if (Cmd(command))
                        continue;
                }
            }
            catch (Exception e)
            {
                Log.Error("Console", "Nem tudom olvasni a console-t: " + e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd">Console Parancs</param>
        /// <returns>True: van ilyen parancs, és meg is csinálja azt
        ///     False: nincs ilyen parancs</returns>
        private bool Cmd(string cmd)
        {
            string[] cmdString = cmd.Split(' ');
            string parancs = cmdString[0].ToLower();

            if (parancs == "exit")
            {
                Irc.IRC.IrcWriter.WriteLine("QUIT :Kilépés Console parancsra.");
                Environment.Exit(0);
                return true;
            }
            if (parancs == "info")
            {
                Log.Msg("Info", String.Format("Felhasznált memória: {0} MB", 
                    Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024));
                Log.Msg("Info", String.Format("Felhasznált processzor: {0}", 
                    System.Diagnostics.Process.GetCurrentProcess()));
                Log.Msg("Info", String.Format("Threadek száma: {0}", Process.GetCurrentProcess().Threads.Count));
                Log.Msg("Info", String.Format("Operációs rendszer: {0}", Environment.OSVersion.ToString()));
                Log.Msg("Info", String.Format("Bot verzió: {0}", BotConfig.BotVersion.TabornokVersion));

                return true;
            }
            if (parancs == "consolelog")
            {
                if (cmdString[1] == "be")
                {
                    ConsoleLog = true;
                    Log.Debug("Console", "Console logolás bekapcsolva");
                }
                else if (cmdString[1] == "ki")
                {
                    Log.Debug("Console", "Console logolás kikapcsolva");
                    ConsoleLog = false;
                }
                else
                {
                    Log.Debug("Console", "Parancs használata: 'ConsoleLog <be|ki>'");
                }
            }
            if (parancs == "test")
            {
                IrcHandler.IrcMessage.SendMessage(IrcMessage.IType.PRIVMSG, "#Tabornok", "test");
            }

            return false;
        }
    }
}
