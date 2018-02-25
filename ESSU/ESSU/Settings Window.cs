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
using Microsoft.Win32;
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

namespace ESSU
{
    public partial class Settings_Window : Form
    {
        public Settings_Window()
        {
            InitializeComponent();
            combo_lang.SelectedIndex = 0;
            combo_startupWindow.SelectedIndex = 0;
            loadSettings();
            lbl_notice.Text = lbl_notice.Text.Replace("VERSION", "v" + Application.ProductVersion);
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            applySettings();
            this.Close();
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        Point mousePos = new Point(0, 0);
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            mousePos = new Point(e.X, e.Y);
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - mousePos.X;
                this.Top += e.Y - mousePos.Y;

            }
        }

        private void btn_sEXEopen_Click(object sender, EventArgs e)
        {
            openFile.ShowDialog();
            if (openFile.FileName.Length > 0 && openFile.CheckFileExists) txt_steamLocation.Text = openFile.FileName;

        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (list_libraries.SelectedIndex != -1) list_libraries.Items.RemoveAt(list_libraries.SelectedIndex);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            folder.ShowDialog();
            if (Directory.Exists(folder.SelectedPath) && folder.SelectedPath.EndsWith("steamapps") && !list_libraries.Items.Contains(folder.SelectedPath) ) { list_libraries.Items.Add(folder.SelectedPath); }
            else { MessageBox.Show("Directory doesn't exist, invalid, or is already selected."); }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://steamcommunity.com/profiles/76561198373212815");
        }

        private void applySettings()
        {
            string settingsFile = null;
            string end = "\n";
            settingsFile = "lang:" + combo_lang.SelectedItem + end;
            settingsFile = settingsFile + "startwin:" + combo_startupWindow.SelectedItem + end;
            settingsFile = settingsFile + "runboot:" + check_runAtStartup.CheckState + end;
            foreach (string dir in list_libraries.Items)
            {
                settingsFile = settingsFile + "steamappdir:" + dir + end;
            }
            settingsFile = settingsFile + "steamexe:" + txt_steamLocation.Text + end;
            File.WriteAllText(Application.StartupPath + "\\settings.cfg", settingsFile);
            if (check_runAtStartup.Checked)
            {
                try
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                    {
                        key.SetValue("Enhanced Steam Standalone Unoffical", "\"" + Application.ExecutablePath + "\"");
                    }
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                    {
                        key.DeleteValue("Enhanced Steam Standalone Unoffical", false);
                    }
                }
                catch
                {
                    
                }
                
            }
        }

        private void loadSettings()
        {
            Settings.loadSettings();
            check_runAtStartup.CheckState = Settings.runboot;
            txt_steamLocation.Text = Settings.steamEXE;
            combo_lang.SelectedItem = Settings.lang;
            combo_startupWindow.SelectedItem = Settings.startwin;
            list_libraries.Items.Clear();
            foreach (string i in Settings.steamDirectories)
            {
                if (i == null) return;
                list_libraries.Items.Add(i);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.metroforsteam.com/");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.enhancedsteam.com");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://vivaldi.com/");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://metroforsteam.com/");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://steamcommunity.com/profiles/76561198373212815");
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://steamcommunity.com/profiles/76561198373212815");
        }

        private void btn_music_add_Click(object sender, EventArgs e)
        {
            folder.ShowDialog();
            if (Directory.Exists(folder.SelectedPath) && folder.SelectedPath.EndsWith("steamapps") && !list_music.Items.Contains(folder.SelectedPath)) { list_music.Items.Add(folder.SelectedPath); }
            else { MessageBox.Show("Directory doesn't exist, invalid, or is already selected."); }
        }

        private void btn_music_remove_Click(object sender, EventArgs e)
        {
            if (list_music.SelectedIndex != -1) list_music.Items.RemoveAt(list_music.SelectedIndex);
        }
    }
}
