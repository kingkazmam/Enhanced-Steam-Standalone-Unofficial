﻿/*
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
            string viv = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Vivaldi\";
            if (Directory.Exists(viv))
            {
                MessageBox.Show("Warning this will wipe Vivaldi settings. It is recommended that you don't use Vivaldi as your default browser");
                chk_1.Checked = true;
            }
            else
            {
                MessageBox.Show("Vivaldi not installed. Please Install Vivaldi either from their site or with the provided .exe");
                try { Process.Start(Application.StartupPath + @"\Install Vivaldi.exe"); } catch { }
                Application.Exit();
            }
        }
        

        private void CreateShortcut()
        {
            object shDesktop = (object)"Desktop";
            WshShell shell = new WshShell();
            string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\Enhanced Steam Standalone Unofficial.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "Steam Client Enhanced";
            shortcut.WorkingDirectory = @"C:\ESSU\";
            shortcut.TargetPath = @"C:\ESSU\ESSULauncher.exe";
            shortcut.Save();
        }

        private void btn_install_Click(object sender, EventArgs e)
        {
            if (chk_1.Checked == true)
            {
                ZipFile zip = ZipFile.Read(Application.StartupPath + @"\Setup1.zip");
                string viv = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Vivaldi\User Data\";
                zip.ExtractAll(viv, ExtractExistingFileAction.OverwriteSilently);

                ZipFile zip2 = ZipFile.Read(Application.StartupPath + @"\Setup2.zip");
                zip2.ExtractAll(@"C:\", ExtractExistingFileAction.OverwriteSilently);
                MessageBox.Show("Installation Completed");
                CreateShortcut();
            }
        }
    }
}
