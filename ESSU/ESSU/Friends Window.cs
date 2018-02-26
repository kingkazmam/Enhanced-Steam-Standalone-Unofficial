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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace ESSU
{
    public partial class Friends_Window : Form
    {
        public Friends_Window()
        {
            InitializeComponent();
            initbrowser();
        }
        

        ChromiumWebBrowser friendsChat = new ChromiumWebBrowser("https://steamcommunity.com//chat/");
        private void initbrowser()
        {
            
            this.Controls.Add(friendsChat);
            friendsChat.Parent = panel_body;
            friendsChat.Dock = DockStyle.Fill;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void resize_MouseDown(object sender, MouseEventArgs e)
        {
            mousePos = new Point(e.X, e.Y);
        }

        private void resize_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Width += e.X - mousePos.X;
                this.Height += e.Y - mousePos.Y;
            }
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
