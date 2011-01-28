/*
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
using System.Reflection;

namespace Tabornok.BotConfig
{
    class Conf
    {
        private static string _Server;
        private static int _Port = 6667;
        private static string _Nick;
        private static string _Nick2;
        private static string _Nick3;
        private static string _MainChannel;
        private static string _Elojel;
        private static int _Activate;
        private static string _IdentifyPass;

        public static string Server
        {
            get { return _Server; }
            set { _Server = value; }
        }

        public static int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        public static string Nick
        {
            get { return _Nick; }
            set { _Nick = value; }
        }

        public static string Nick2
        {
            get { return _Nick2; }
            set { _Nick2 = value; }
        }

        public static string Nick3
        {
            get { return _Nick3; }
            set { _Nick3 = value; }
        }

        public static string MainChannel
        {
            get { return _MainChannel; }
            set { _MainChannel = value; }
        }

        public static string Elojel
        {
            get { return _Elojel; }
            set { _Elojel = value; }
        }

        public static int Activate
        {
            get { return _Activate; }
            set { _Activate = value; }
        }

        public static string IdentifyPass
        {
            get { return _IdentifyPass; }
            set { _IdentifyPass = value; }
        }
    }

    class BotVersion
    {
        public static string TabornokVersion
        {
            get { return (Assembly.GetExecutingAssembly().GetName().Version.ToString()); }
        }
    }
}
