﻿/*
 <Tabornok - IRC bot>
 Copyright (C) <2011>  <Jackneill>

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
using Tabornok.BotConfig;
using Tabornok.Irc;

namespace Tabornok.IrcHandler.IrcCommands
{
    class InitCommands
    {
        public InitCommands()
        {

        }

        ~InitCommands()
        {
            Log.Debug("InitCommands", "~InitCommands()");
        }

		public void TesztCommand(string args)
		{
			string[] Args = args.Split(' ');

			if(Args[0].Substring(0, 1) != "`")
				return;

			Args[0] = Args[0].Remove(0, 1);

			Console.WriteLine(Args[0]);

            if(Args[0] == "ido")
            {
                LocDateTime.LocalDateTime();
            }

            if(Args[0] == "info")
            {
                SysInfo.BotInfo();
            }

			if(Args[0] == "teszt")
				IRC.IrcWriter.WriteLine("PRIVMSG #schumix :Megy!!!!");
		}
    }
}
