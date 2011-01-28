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
using System.Xml;
using System.IO;

namespace Tabornok.BotConfig
{
    class Config
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConfigFile"></param>
        public Config(string ConfigFile)
        {
            Log.Debug("Config", "Config fájl betöltése...");

            do
            {
                if (!IsConfig(ConfigFile))
                    Log.Error("Config", "Kérlek tölsd ki a Config.xml fájlt!");
            } while ((IsConfig(ConfigFile)) == false);

            var xmldoc = new XmlDocument();
            xmldoc.Load(@ConfigFile);

            Conf.Server = xmldoc.SelectSingleNode("Tabornok/Irc/Szerver").InnerText;
            Conf.Port = Convert.ToInt32(xmldoc.SelectSingleNode("Tabornok/Irc/Port").InnerText);
            Conf.Nick = xmldoc.SelectSingleNode("Tabornok/Nickserv/Nick").InnerText;
            Conf.Nick2 = xmldoc.SelectSingleNode("Tabornok/Nickserv/Nick2").InnerText;
            Conf.Nick3 = xmldoc.SelectSingleNode("Tabornok/Nickserv/Nick3").InnerText;
            Conf.MainChannel = xmldoc.SelectSingleNode("Tabornok/Chanserv/MainChannel").InnerText;
            Conf.Activate = Convert.ToInt32(xmldoc.SelectSingleNode("Tabornok/Nickserv/Identify/Activate").InnerText);
            Conf.IdentifyPass = xmldoc.SelectSingleNode("Tabornok/Nickserv/Identify/Password").InnerText;

            ActualNick.CurrentNick = Conf.Nick;

            Log.Success("Config", "Config.xml betöltése sikeres");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConfigFile"></param>
        /// <returns></returns>
        private bool IsConfig(string ConfigFile)
        {
            /*if (!File.Exists("Config.xml"))
            {
                Log.Error("Config", "Nincs Config.xml fájl! Elkészítése folyamatban...");
                File.Create("Config.xml");
            }*/

            if (File.Exists(ConfigFile))
            {
                return true;
            }
            else
            {

                XmlTextWriter XMLWriter = null;
                XMLWriter = new XmlTextWriter(ConfigFile, null);

                try
                {
                    XMLWriter.Formatting = Formatting.Indented;
                    XMLWriter.Indentation = 4;
                    XMLWriter.Namespaces = false;

                    XMLWriter.WriteStartDocument();

                    // <Tabornok>
                    XMLWriter.WriteStartElement("", "Tabornok", "");
                    // <Irc>
                    XMLWriter.WriteStartElement("", "Irc", "");
                    // <Szerver>
                    XMLWriter.WriteStartElement("", "Szerver", "");
                    XMLWriter.WriteString("irc.rizon.net");
                    XMLWriter.WriteEndElement();
                    // </Szerver>
                    // <Port>
                    XMLWriter.WriteStartElement("", "Port", "");
                    XMLWriter.WriteString("6667");
                    XMLWriter.WriteEndElement();
                    // </Port>
                    // <Elojel>
                    XMLWriter.WriteStartElement("", "Elojel", "");
                    XMLWriter.WriteString("`");
                    XMLWriter.WriteEndElement();
                    // </Elojel>
                    // </Irc>
                    XMLWriter.WriteEndElement();

                    // <Nickserv>
                    XMLWriter.WriteStartElement("", "Nickserv", "");
                    // <Nick>
                    XMLWriter.WriteStartElement("", "Nick", "");
                    XMLWriter.WriteString("Tabornok");
                    XMLWriter.WriteEndElement();
                    // </Nick>
                    // <Nick2>
                    XMLWriter.WriteStartElement("", "Nick2", "");
                    XMLWriter.WriteString("");
                    XMLWriter.WriteEndElement();
                    // </Nick2>
                    // <Nick2>
                    XMLWriter.WriteStartElement("", "Nick3", "");
                    XMLWriter.WriteString("");
                    XMLWriter.WriteEndElement();
                    // </Nick3>
                    // <Identify>
                    XMLWriter.WriteStartElement("", "Identify", "");
                    // <Activate>
                    XMLWriter.WriteStartElement("", "Activate", "");
                    XMLWriter.WriteString("0");
                    XMLWriter.WriteEndElement();
                    // </Activate>
                    // <Password>
                    XMLWriter.WriteStartElement("", "Password", "");
                    XMLWriter.WriteString("PASSWORD");
                    XMLWriter.WriteEndElement();
                    // </Password>
                    // </Identify>
                    XMLWriter.WriteEndElement();
                    // </Nickserv>
                    XMLWriter.WriteEndElement();

                    // <Chanserv>
                    XMLWriter.WriteStartElement("", "Chanserv", "");
                    // <MainChannel>
                    XMLWriter.WriteStartElement("", "MainChannel", "");
                    XMLWriter.WriteString("#Tabornok");
                    XMLWriter.WriteEndElement();
                    // </MainChannel>
                    // </Chanserv>
                    XMLWriter.WriteEndElement();

                    // </Tabornok>
                    XMLWriter.WriteEndElement();

                    XMLWriter.Flush();
                }
                catch (Exception e)
                {
                    Log.Error("Config XML Error", "Hiba a Config.xml írása során: " + e);
                    return false;
                }
                finally
                {
                    XMLWriter.Close();
                    Log.Success("Config.xml Write", "A Config.xml írása sikeres! Kérlek tölsd ki!");
                }
            }
            return true;
        }
    }
}
