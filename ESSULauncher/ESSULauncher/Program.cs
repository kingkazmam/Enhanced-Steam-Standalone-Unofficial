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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;

namespace ESSULauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Process.Start(Application.StartupPath + "\\Library Updater.exe");


            try
            {
                Process[] myProcesses = Process.GetProcessesByName("Enhanced Steam Standalone Unofficial");
                if (myProcesses.Count() > 0)
                {
                    File.WriteAllText(Application.StartupPath + "\\es.su", "");
                }
                else
                {
                    Cef.EnableHighDPISupport();
                    var settings = new CefSettings()
                    {
                        CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ESS\\Cache")
                    };
                    Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new frm_main());
                }
                

            } catch { }

        }
    }
}
