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

namespace ESSU
{
    public partial class Bookmarks : Form
    {
        int counter = 0;
        string[,] bookmarkarray = new string[2,100];
        public Bookmarks()
        {
            InitializeComponent();
            if (File.Exists(Application.StartupPath + "\\bmark.dat"))
            {
                for (int index = 0; index < File.ReadAllLines(Application.StartupPath + "\\bmark.dat").Length; index++)
                {
                    if (index % 2 == 0)
                    {
                        bookmarkarray[0, counter] = File.ReadLines(Application.StartupPath + "\\bmark.dat").Skip(index).Take(1).First();
                    }
                    else
                    {
                        bookmarkarray[1, counter] = File.ReadLines(Application.StartupPath + "\\bmark.dat").Skip(index).Take(1).First();
                        counter++;
                    }
                }
            }
            loadList();
        }

        void loadList()
        {
            list_bookmarks.Items.Clear();
            for (int x = 0; x < 100; x++)
            {
                if (String.IsNullOrEmpty(bookmarkarray[0, x])) continue;

                list_bookmarks.Items.Add(bookmarkarray[0, x] + " - " + bookmarkarray[1, x]);
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Application.StartupPath + "\\bmark.dat", "");
            for (int x = 0; x < list_bookmarks.Items.Count; x++)
            {
                if (string.IsNullOrEmpty(bookmarkarray[0, x])) continue;
                File.AppendAllText(Application.StartupPath + "\\bmark.dat", bookmarkarray[0,x] + Environment.NewLine + bookmarkarray[1, x] + Environment.NewLine);
            }
            this.Close();
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void list_games_DrawItem(object sender, DrawItemEventArgs e)
        {
            SolidBrush reportsForegroundBrushSelected = new SolidBrush(Color.FromArgb(255, 26, 26, 26));
            SolidBrush reportsForegroundBrush = new SolidBrush(Color.White);
            SolidBrush reportsForegroundBrush2 = new SolidBrush(Color.Gray);
            SolidBrush reportsForegroundBrush3 = new SolidBrush(Color.FromArgb(255, 102, 36, 226));
            SolidBrush reportsBackgroundBrushSelected = new SolidBrush(Color.FromArgb(255, 102, 36, 226));
            SolidBrush reportsBackgroundBrush1 = new SolidBrush(Color.FromArgb(255, 39, 39, 39));
            e.DrawBackground();
            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < list_bookmarks.Items.Count)
            {
                string text = list_bookmarks.Items[index].ToString();
                Graphics g = e.Graphics;

                //background:
                SolidBrush backgroundBrush;
                if (selected)
                    backgroundBrush = reportsBackgroundBrushSelected;
                else
                    backgroundBrush = reportsBackgroundBrush1;



                g.FillRectangle(backgroundBrush, e.Bounds);

                //text:
                SolidBrush foregroundBrush = (selected) ? reportsForegroundBrushSelected : reportsForegroundBrush;
                SolidBrush foregroundBrush2 = (selected) ? reportsForegroundBrushSelected : reportsForegroundBrush2;
                SolidBrush foregroundBrush3 = (selected) ? reportsForegroundBrushSelected : reportsForegroundBrush3;
                g.DrawString(text, e.Font, foregroundBrush2, list_bookmarks.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            bookmarkarray[0, list_bookmarks.Items.Count] = txt_name.Text;
            bookmarkarray[1, list_bookmarks.Items.Count] = txt_url.Text;
            txt_name.Text = "";
            txt_url.Text = "";
            loadList();
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

        private void btn_mvDown_Click(object sender, EventArgs e)
        {
            if (list_bookmarks.SelectedIndex >= list_bookmarks.Items.Count && list_bookmarks.SelectedItem.ToString() == "Games") return;
            string cur = list_bookmarks.Items[list_bookmarks.SelectedIndex].ToString();
            string nww = list_bookmarks.Items[list_bookmarks.SelectedIndex + 1].ToString();
            list_bookmarks.Items[list_bookmarks.SelectedIndex] = nww;
            list_bookmarks.Items[list_bookmarks.SelectedIndex + 1] = cur;
            list_bookmarks.SelectedItem = list_bookmarks.Items[list_bookmarks.SelectedIndex + 1];
        }

        private void list_bookmarks_DoubleClick(object sender, EventArgs e)
        {
            if (list_bookmarks.SelectedIndex == -1) return;
            Settings.bookmarkGOTO = bookmarkarray[1, list_bookmarks.SelectedIndex];
            this.Close();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            
            if (list_bookmarks.SelectedIndex == -1) return;
            bookmarkarray[0, list_bookmarks.SelectedIndex] = null;
            bookmarkarray[1, list_bookmarks.SelectedIndex] = null;
            loadList();
            
        }
    }
}
