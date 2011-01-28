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
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Tabornok.BotConfig;
using Tabornok.IrcHandler;

namespace Tabornok.Irc
{
    class IRC
    {
        /// <summary>
        /// 
        /// </summary>
        private TcpClient IrcClient { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private NetworkStream stream { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private StreamReader IrcReader { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static StreamWriter IrcWriter { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static bool IrcStatus = false;

        /// <summary>
        /// 
        /// </summary>
        private static string Args { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string[] interpretArgs { get; private set; }

        private static string _HostMask;
        private static string _UserHost;
        private static string _UserNick;
        private static string _Channel;
        private static string _IrcMsg;
        private static string _MessageType;

        public static string HostMask
        {
            get { return _HostMask; }
            private set { _HostMask = value; }
        }
        public static string UserHost
        {
            get { return _UserHost; }
            private set { _UserHost = value; }
        }
        public static string UserNick
        {
            get { return _UserNick; }
            private set { _UserNick = value; }
        }
        public static string Channel
        {
            get { return _Channel; }
            private set { _Channel = value; }
        }
        public static string IrcMsg
        {
            get { return _IrcMsg; }
            private set { _IrcMsg = value; }
        }
        public static string MessageType
        {
            get { return _MessageType; }
            private set { _MessageType = value; }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public IRC()
        {
            Connect(Conf.Server, Conf.Port);
        }

        /// <summary>
        /// Destruktor
        /// </summary>
        ~IRC()
        {
            Log.Debug("IRC Destruktor", "~IRC()");
        }

        /// <summary>
        /// Itt indul el a kapcsolódás az IRC szerverhez
        /// </summary>
        /// <param name="server">IRC szerver</param>
        /// <param name="port">IRC port</param>
        public void Connect(string server, int port)
        {
            Log.Debug("IRC Connect", "Kapcsolódás az IRC szerverhez...");

            IrcClient = new TcpClient(server, port);
            stream = IrcClient.GetStream();

            IrcReader = new StreamReader(stream);
            IrcWriter = new StreamWriter(stream);
            IrcWriter.AutoFlush = true;

            IrcStatus = true;

            Thread PingThread = new Thread(new ThreadStart(PingIrc));
            PingThread.Start();

            Thread IrcReaderThread = new Thread(new ThreadStart(ReadIrc));
            IrcReaderThread.Start();

            IrcWriter.WriteLine("USER " + Conf.Nick + " 8 * :" + Conf.Nick);
            IrcWriter.WriteLine("NICK " + Conf.Nick);

            new IHandler();

            Log.Success("IRC Connect", "Sikeres IRC kapcsolódás");
            Console.Title += String.Format(" [Szerver:Port - {0}:{1} - Nick: {2}]", 
                Conf.Server, Conf.Port, Conf.Nick);

            ReConnect();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reConnect"></param>
        public void ReConnect()
        {
            if (!IrcStatus)
            {
                Log.Debug("ReConnect", "Újrakapcsolódás az IRC szerverhez megindult...");
                Connect(Conf.Server, Conf.Port);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void DisConnect()
        {
            IrcClient.Close();
            IrcWriter.Close();
            IrcReader.Close();
            stream.Close();
            IrcStatus = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IrcReader"></param>
        private void ReadIrc()
        {
            try
            {
                while (true)
                {
                    if ((Args = IrcReader.ReadLine()) == null)
                        break;

                    interpretArgs = Args.Split(' ');

                    if (interpretArgs[0].Substring(0, 1) == ":")
                        interpretArgs[0] = interpretArgs[0].Remove(0, 1);

                    HostMask = interpretArgs[0];
                    string[] HM = HostMask.Split('!');

                    UserNick = HM[0];

                    HM = HostMask.Split('@');
                    UserHost = HM[0];

                    MessageType = interpretArgs[1];
                    Channel = interpretArgs[2];

                    new InterpretArgs(interpretArgs);

                    if (Consol.BotConsol.ConsoleLog)
                    {
                        Log.Debug("Args", Args);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("IRC Reader", "Nem tudok olvasni adatot az Irc felől: " + e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void PingIrc()
        {
            try
            {
                while (true)
                {
                    if (IrcStatus)
                    {
                        IrcWriter.WriteLine(String.Format("PING :{0}", Conf.Server));
                        Thread.Sleep(15000);
                    }
                    else
                    {
                        IrcStatus = false;
                        Thread.Sleep(15000);

                        ReConnect();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("PingIrc", "Nem tudtam pingelni az Irc szervert: " + e);
                PingIrc();
                Thread.Sleep(15000);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void PongIrc()
        {

        }
    }
}
