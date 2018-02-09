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
using Ionic.Zip;
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
using IWshRuntimeLibrary;

namespace Setup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Warning This Will Wipe Vivaldi Settings. It is recommended that you don't use vivaldi as your default browser");
            try
            {
                Process.Start("vivaldi");
                Process[] myProcesses = Process.GetProcessesByName("vivaldi");
                if (myProcesses.Count() > 0)
                {
                    foreach (Process proc in myProcesses)
                    {
                        try { proc.Kill(); }
                        catch { }
                    }
                }
                chk_1.Checked = true;
            }
            catch
            {

            }
            
        }
        

        private void CreateShortcut()
        {
            object shDesktop = (object)"Desktop";
            WshShell shell = new WshShell();
            string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\Enhanced Steam Standalone Unofficial.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "Steam Client Enhanced";
            shortcut.WorkingDirectory = @"C:\Program Files (x86)\ESSU\";
            shortcut.TargetPath = @"C:\Program Files (x86)\ESSU\ESSULauncher.exe";
            shortcut.Save();
        }

        private void btn_install_Click(object sender, EventArgs e)
        {
            if (chk_1.Checked == false)
            {
                MessageBox.Show("Vivaldi is not installed, we will start the installer. Once finished relaunch the installer");
                Process.Start(Application.StartupPath + @"\Install Vivaldi.exe");
                Application.Exit();
            }
            else
            {
                ZipFile zip = ZipFile.Read(Application.StartupPath + @"\Setup1.zip");
                string viv = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Vivaldi\User Data\";
                zip.ExtractAll(viv, ExtractExistingFileAction.OverwriteSilently);

                ZipFile zip2 = ZipFile.Read(Application.StartupPath + @"\Setup2.zip");
                zip2.ExtractAll(@"C:\Program Files (x86)\", ExtractExistingFileAction.OverwriteSilently);
                MessageBox.Show("Installation Completed");
                CreateShortcut();
            }
        }
    }
}
