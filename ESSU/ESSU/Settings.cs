/*
    This file is part of Enhanced Steam Standalone Unofficial.

    Enhanced Steam Standalone Unofficial is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Enhanced Steam Standalone Unofficial is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Enhanced Steam Standalone Unofficial.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESSU
{
    public class Settings
    {
        /*
         gameArray[index, 0] = Shorthand Name;
         gameArray[index, 1] = appid;
         gameArray[index, 2] = Installed Directory;
         gameArray[index, 3] = Full Name;
         gameArray[index, 4] = Steamapp Folder;
        */
        public static string tempName = "";

        public static string[] steamDirectories = new string[10000];
        
        public static string steamEXE = "C:\\Program Files (x86)\\Steam\\Steam.exe";
        public static string lang = "English";
        public static string startwin = "Store";
        public static CheckState runboot = CheckState.Unchecked;


        public static void init()
        {
            
            if (!File.Exists(Application.StartupPath + "\\settings.cfg"))
            {
                Form f = new Settings_Window();
                MessageBox.Show("No settings.cfg file found, setting everything to default");
                f.Show();
                f.BringToFront();
                return;
            }
            loadSettings();
        }
        public static void loadSettings()
        {
            int sDirCounter = 0;
            steamDirectories[sDirCounter] = "C:\\Program Files (x86)\\Steam\\steamapps";
            try
            {
                foreach (string line in File.ReadLines(Application.StartupPath + "\\settings.cfg"))
                {
                    string language = "lang:";
                    string startwi = "startwin:";
                    string steamappdir = "steamappdir:";
                    string steame = "steamexe:";
                    string runbo = "runboot:";

                    if (line.Contains(runbo))
                    {
                        if (line.Substring(runbo.Length) == "Checked")
                        {
                            runboot = CheckState.Checked;
                        }
                        else
                        {
                            runboot = CheckState.Unchecked;
                        }
                        
                    }
                    if (line.Contains(language)) lang = line.Substring(language.Length);
                    if (line.Contains(startwi)) startwin = line.Substring(startwi.Length);
                    if (line.Contains(steamappdir)) { steamDirectories[sDirCounter] = line.Substring(steamappdir.Length); sDirCounter++; }
                    if (line.Contains(steame)) steamEXE = line.Substring(steame.Length);
                }
                return;
            }
            catch
            {
            }
        }

    }
}
