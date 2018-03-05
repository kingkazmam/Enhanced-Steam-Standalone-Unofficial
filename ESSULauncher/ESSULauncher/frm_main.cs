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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace ESSULauncher
{
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
            initbrowser();
            
        }

        ChromiumWebBrowser libUpdater = new ChromiumWebBrowser("http://steamcommunity.com/my/games/?tab=all");
        private void initbrowser()
        {
            this.Controls.Add(libUpdater);
            libUpdater.Parent = panel1;
            libUpdater.Dock = DockStyle.Fill;
            libUpdater.FrameLoadEnd += WebBrowserFrameLoadEnded;
        }

        private void WebBrowserFrameLoadEnded(object sender, FrameLoadEndEventArgs e)
        {
            libUpdater.GetSourceAsync().ContinueWith(taskHtml =>
            {
                string html = taskHtml.Result;
                if (!html.Contains("mainLoginPanel"))
                {  
                    try
                    {
                        File.WriteAllText(Application.StartupPath + "\\library.txt", html);
                        if (!File.Exists(Application.StartupPath + "\\es.su") && !File.Exists(Application.StartupPath + "\\slient.starup")) Process.Start(Application.StartupPath + "\\Enhanced Steam Standalone Unofficial.exe");
                        if (File.Exists(Application.StartupPath + "\\slient.starup")) File.Delete(Application.StartupPath + "\\slient.starup");
                    }
                    catch { }
                    Application.Exit();
                }
                else
                {
                    panel2.Hide();
                }
            });
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "\\es.su"))
            {
                this.SendToBack();
                this.ShowInTaskbar = false;
            }
            

        }
    }
}
