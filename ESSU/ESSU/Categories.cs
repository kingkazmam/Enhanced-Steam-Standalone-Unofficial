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
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();

            try
            {
                foreach (string line in File.ReadAllLines(Application.StartupPath + "\\c.dat"))
                {
                    if (!(line.Length > 0)) continue;
                    if (!File.Exists(Application.StartupPath + "\\" + line + ".dat")) File.Create(Application.StartupPath + "\\" + line + ".dat");
                    list_games.Items.Add(line);
                }
                if (list_games.Items.Count > 0) list_games.SelectedIndex = 0;
                list_games.Focus();
            }
            catch { }
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

        private void btn_exit_Click(object sender, EventArgs e)
        {
            string info = "";
            try
            {
                foreach (string item in list_games.Items)
                {
                    info = info + item + Environment.NewLine;
                }
                if (info != "")
                {
                    File.WriteAllText(Application.StartupPath + "\\c.dat", info);
                }
                
            } catch { }
            Settings.tempName = "startRefresh";
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
            if (index >= 0 && index < list_games.Items.Count)
            {
                string text = list_games.Items[index].ToString();
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
                g.DrawString(text, e.Font, foregroundBrush2, list_games.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txt_cat.Text.Length < 1) return;
            if (list_games.Items.Contains(txt_cat.Text)) return;
            if (txt_cat.Text == "Games") return;
            for (int i = 0; i <txt_cat.Text.Length; i++)
            {
                if (txt_cat.Text.Substring(i,1) == " " || txt_cat.Text.Substring(i, 1) == "▶" || txt_cat.Text.Substring(i, 1) == "▼")
                {
                    txt_cat.Text.Remove(i, 1);
                }
                else
                {
                    continue;
                }
            }
            try
            {
                foreach (string item in list_games.Items)
                {
                    if (item == "Games") list_games.Items.Remove(item);
                }

            } catch { }
            
            list_games.Items.Add(txt_cat.Text);
            File.Create(Application.StartupPath + "\\" + txt_cat.Text + ".dat").Close();
            if (!File.Exists(Application.StartupPath + "\\Games.dat"))
            {
                File.Create(Application.StartupPath + "\\Games.dat").Close();
            }
            list_games.Items.Add("Games");
            string s = null;
            foreach (string item in list_games.Items)
            {
                s = s + item + Environment.NewLine;
            }
            File.WriteAllText(Application.StartupPath + "\\c.dat", s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex == -1) return;
            foreach (string line in File.ReadAllLines(Application.StartupPath + "\\c.dat"))
            {
                if (!(line.Length > 0)) continue;
                string[] lines = File.ReadAllLines(Application.StartupPath + "\\" + line + ".dat");
                
                string temp = Path.GetTempFileName();
                File.WriteAllText(temp, "");
                for (int x = 0;  x < lines.Length; x++)
                {
                    if (String.IsNullOrEmpty(lines[x]))
                    {
                        continue;
                    }
                    if (lines[x] != Settings.tempName)
                    {
                        File.AppendAllText(temp, lines[x] + Environment.NewLine);
                    }
                    File.WriteAllText(Application.StartupPath + "\\" + line + ".dat", File.ReadAllText(temp));
                }

            }
            try
            {
                File.AppendAllText(Application.StartupPath + "\\" + list_games.SelectedItem + ".dat", Settings.tempName + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Settings.tempName = "startRefresh";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.Delete(Application.StartupPath + "\\" + list_games.SelectedItem + ".dat");
            if (list_games.SelectedItem.ToString() != "Games") list_games.Items.Remove(list_games.SelectedItem);
            string s = null;
            foreach (string item in list_games.Items)
            {
                s = s + item + Environment.NewLine;
            }
            File.WriteAllText(Application.StartupPath + "\\c.dat", s);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (string line in File.ReadAllLines(Application.StartupPath + "\\c.dat"))
            {
                File.Delete(Application.StartupPath + "\\" + line + ".dat");
            }
            File.Delete(Application.StartupPath + "\\c.dat");
            list_games.Items.Clear();

            
        }

        private void btn_mvUp_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex <= 0 && list_games.SelectedItem.ToString() == "Games") return;
            string cur = list_games.Items[list_games.SelectedIndex].ToString();
            string nww = list_games.Items[list_games.SelectedIndex - 1].ToString();
            list_games.Items[list_games.SelectedIndex] = nww;
            list_games.Items[list_games.SelectedIndex - 1] = cur;
            list_games.SelectedItem = list_games.Items[list_games.SelectedIndex - 1];
        }

        private void btn_mvDown_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex >= list_games.Items.Count && list_games.SelectedItem.ToString() == "Games") return;
            string cur = list_games.Items[list_games.SelectedIndex].ToString();
            string nww = list_games.Items[list_games.SelectedIndex + 1].ToString();
            list_games.Items[list_games.SelectedIndex] = nww;
            list_games.Items[list_games.SelectedIndex + 1] = cur;
            list_games.SelectedItem = list_games.Items[list_games.SelectedIndex + 1];
        }
    }
}
