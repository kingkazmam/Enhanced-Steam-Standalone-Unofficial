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
namespace ESSU
{
    partial class Settings_Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings_Window));
            this.panel_topbar = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_min = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tab_settings = new System.Windows.Forms.TabControl();
            this.tab_Inferface = new System.Windows.Forms.TabPage();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.check_runAtStartup = new System.Windows.Forms.CheckBox();
            this.combo_startupWindow = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.combo_lang = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tab_Downloads = new System.Windows.Forms.TabPage();
            this.btn_sEXEopen = new System.Windows.Forms.Button();
            this.lbl_steamexe = new System.Windows.Forms.Label();
            this.txt_steamLocation = new System.Windows.Forms.TextBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.list_libraries = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tab_music = new System.Windows.Forms.TabPage();
            this.btn_music_add = new System.Windows.Forms.Button();
            this.btn_music_remove = new System.Windows.Forms.Button();
            this.list_music = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tab_credits = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_notice = new System.Windows.Forms.Label();
            this.linkLabel7 = new System.Windows.Forms.LinkLabel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.folder = new System.Windows.Forms.FolderBrowserDialog();
            this.panel_topbar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tab_settings.SuspendLayout();
            this.tab_Inferface.SuspendLayout();
            this.tab_Downloads.SuspendLayout();
            this.tab_music.SuspendLayout();
            this.tab_credits.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_topbar
            // 
            this.panel_topbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_topbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.panel_topbar.Controls.Add(this.label1);
            this.panel_topbar.Controls.Add(this.btn_min);
            this.panel_topbar.Controls.Add(this.btn_exit);
            this.panel_topbar.Location = new System.Drawing.Point(0, -2);
            this.panel_topbar.Name = "panel_topbar";
            this.panel_topbar.Size = new System.Drawing.Size(392, 40);
            this.panel_topbar.TabIndex = 6;
            this.panel_topbar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.panel_topbar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.18868F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "SETTINGS";
            // 
            // btn_min
            // 
            this.btn_min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_min.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btn_min.BackgroundImage = global::ESSU.Properties.Resources.win32_win_min;
            this.btn_min.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_min.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_min.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(36)))), ((int)(((byte)(226)))));
            this.btn_min.FlatAppearance.BorderSize = 0;
            this.btn_min.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btn_min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_min.Font = new System.Drawing.Font("Webdings", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btn_min.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
            this.btn_min.Location = new System.Drawing.Point(312, -4);
            this.btn_min.Name = "btn_min";
            this.btn_min.Size = new System.Drawing.Size(40, 58);
            this.btn_min.TabIndex = 2;
            this.btn_min.UseVisualStyleBackColor = false;
            this.btn_min.Click += new System.EventHandler(this.btn_min_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btn_exit.BackgroundImage = global::ESSU.Properties.Resources.win32_win_close;
            this.btn_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_exit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(36)))), ((int)(((byte)(226)))));
            this.btn_exit.FlatAppearance.BorderSize = 0;
            this.btn_exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btn_exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_exit.Font = new System.Drawing.Font("Webdings", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btn_exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
            this.btn_exit.Location = new System.Drawing.Point(352, 1);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(40, 40);
            this.btn_exit.TabIndex = 0;
            this.btn_exit.UseVisualStyleBackColor = false;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(392, 414);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panel2.Controls.Add(this.tab_settings);
            this.panel2.Location = new System.Drawing.Point(12, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(368, 400);
            this.panel2.TabIndex = 8;
            // 
            // tab_settings
            // 
            this.tab_settings.Controls.Add(this.tab_Inferface);
            this.tab_settings.Controls.Add(this.tab_Downloads);
            this.tab_settings.Controls.Add(this.tab_music);
            this.tab_settings.Controls.Add(this.tab_credits);
            this.tab_settings.Controls.Add(this.tabPage1);
            this.tab_settings.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_settings.Location = new System.Drawing.Point(-4, 0);
            this.tab_settings.Name = "tab_settings";
            this.tab_settings.SelectedIndex = 0;
            this.tab_settings.Size = new System.Drawing.Size(376, 404);
            this.tab_settings.TabIndex = 0;
            // 
            // tab_Inferface
            // 
            this.tab_Inferface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.tab_Inferface.Controls.Add(this.linkLabel2);
            this.tab_Inferface.Controls.Add(this.check_runAtStartup);
            this.tab_Inferface.Controls.Add(this.combo_startupWindow);
            this.tab_Inferface.Controls.Add(this.label5);
            this.tab_Inferface.Controls.Add(this.linkLabel1);
            this.tab_Inferface.Controls.Add(this.combo_lang);
            this.tab_Inferface.Controls.Add(this.label4);
            this.tab_Inferface.ForeColor = System.Drawing.Color.White;
            this.tab_Inferface.Location = new System.Drawing.Point(4, 24);
            this.tab_Inferface.Name = "tab_Inferface";
            this.tab_Inferface.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Inferface.Size = new System.Drawing.Size(368, 376);
            this.tab_Inferface.TabIndex = 0;
            this.tab_Inferface.Text = "Interface";
            // 
            // linkLabel2
            // 
            this.linkLabel2.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(36)))), ((int)(((byte)(226)))));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
            this.linkLabel2.Location = new System.Drawing.Point(171, 358);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(194, 17);
            this.linkLabel2.TabIndex = 10;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Recommended Skin For Steam";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // check_runAtStartup
            // 
            this.check_runAtStartup.AutoSize = true;
            this.check_runAtStartup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_runAtStartup.Location = new System.Drawing.Point(9, 154);
            this.check_runAtStartup.Name = "check_runAtStartup";
            this.check_runAtStartup.Size = new System.Drawing.Size(176, 21);
            this.check_runAtStartup.TabIndex = 8;
            this.check_runAtStartup.Text = "Run at computer startup";
            this.check_runAtStartup.UseVisualStyleBackColor = true;
            // 
            // combo_startupWindow
            // 
            this.combo_startupWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.combo_startupWindow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_startupWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_startupWindow.ForeColor = System.Drawing.Color.White;
            this.combo_startupWindow.FormattingEnabled = true;
            this.combo_startupWindow.Items.AddRange(new object[] {
            "Store",
            "Library",
            "News",
            "Friends",
            "Friend Activity",
            "Community Home"});
            this.combo_startupWindow.Location = new System.Drawing.Point(9, 117);
            this.combo_startupWindow.Name = "combo_startupWindow";
            this.combo_startupWindow.Size = new System.Drawing.Size(184, 23);
            this.combo_startupWindow.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
            this.label5.Location = new System.Drawing.Point(6, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "Startup Window";
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(36)))), ((int)(((byte)(226)))));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
            this.linkLabel1.Location = new System.Drawing.Point(6, 66);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(301, 17);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Fluent in another language? Help darc translate.";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // combo_lang
            // 
            this.combo_lang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.combo_lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_lang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.combo_lang.ForeColor = System.Drawing.Color.White;
            this.combo_lang.FormattingEnabled = true;
            this.combo_lang.Items.AddRange(new object[] {
            "en_US"});
            this.combo_lang.Location = new System.Drawing.Point(9, 42);
            this.combo_lang.Name = "combo_lang";
            this.combo_lang.Size = new System.Drawing.Size(184, 23);
            this.combo_lang.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.150944F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(256, 34);
            this.label4.TabIndex = 3;
            this.label4.Text = "Select the language you wish Steam to use\r\n(requires ESSU to restart)";
            // 
            // tab_Downloads
            // 
            this.tab_Downloads.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.tab_Downloads.Controls.Add(this.btn_sEXEopen);
            this.tab_Downloads.Controls.Add(this.lbl_steamexe);
            this.tab_Downloads.Controls.Add(this.txt_steamLocation);
            this.tab_Downloads.Controls.Add(this.btn_add);
            this.tab_Downloads.Controls.Add(this.btn_remove);
            this.tab_Downloads.Controls.Add(this.list_libraries);
            this.tab_Downloads.Controls.Add(this.label3);
            this.tab_Downloads.ForeColor = System.Drawing.Color.White;
            this.tab_Downloads.Location = new System.Drawing.Point(4, 24);
            this.tab_Downloads.Name = "tab_Downloads";
            this.tab_Downloads.Size = new System.Drawing.Size(368, 376);
            this.tab_Downloads.TabIndex = 2;
            this.tab_Downloads.Text = "Library";
            // 
            // btn_sEXEopen
            // 
            this.btn_sEXEopen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.btn_sEXEopen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sEXEopen.Location = new System.Drawing.Point(331, 348);
            this.btn_sEXEopen.Name = "btn_sEXEopen";
            this.btn_sEXEopen.Size = new System.Drawing.Size(33, 24);
            this.btn_sEXEopen.TabIndex = 8;
            this.btn_sEXEopen.Text = "...";
            this.btn_sEXEopen.UseVisualStyleBackColor = true;
            // 
            // lbl_steamexe
            // 
            this.lbl_steamexe.AutoSize = true;
            this.lbl_steamexe.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_steamexe.Location = new System.Drawing.Point(4, 353);
            this.lbl_steamexe.Name = "lbl_steamexe";
            this.lbl_steamexe.Size = new System.Drawing.Size(125, 17);
            this.lbl_steamexe.TabIndex = 7;
            this.lbl_steamexe.Text = "Steam.exe Location";
            // 
            // txt_steamLocation
            // 
            this.txt_steamLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.txt_steamLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_steamLocation.Enabled = false;
            this.txt_steamLocation.ForeColor = System.Drawing.Color.White;
            this.txt_steamLocation.Location = new System.Drawing.Point(135, 348);
            this.txt_steamLocation.Name = "txt_steamLocation";
            this.txt_steamLocation.Size = new System.Drawing.Size(190, 24);
            this.txt_steamLocation.TabIndex = 6;
            // 
            // btn_add
            // 
            this.btn_add.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Location = new System.Drawing.Point(207, 134);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(63, 28);
            this.btn_add.TabIndex = 5;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.btn_remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_remove.Location = new System.Drawing.Point(276, 134);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(76, 28);
            this.btn_remove.TabIndex = 4;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // list_libraries
            // 
            this.list_libraries.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.list_libraries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list_libraries.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.list_libraries.Font = new System.Drawing.Font("Segoe UI", 8.150944F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.list_libraries.ForeColor = System.Drawing.Color.White;
            this.list_libraries.FormattingEnabled = true;
            this.list_libraries.IntegralHeight = false;
            this.list_libraries.ItemHeight = 15;
            this.list_libraries.Location = new System.Drawing.Point(18, 35);
            this.list_libraries.Name = "list_libraries";
            this.list_libraries.Size = new System.Drawing.Size(334, 92);
            this.list_libraries.TabIndex = 3;
            this.list_libraries.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.list_games_DrawItem);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Content Libraries";
            // 
            // tab_music
            // 
            this.tab_music.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.tab_music.Controls.Add(this.btn_music_add);
            this.tab_music.Controls.Add(this.btn_music_remove);
            this.tab_music.Controls.Add(this.list_music);
            this.tab_music.Controls.Add(this.label9);
            this.tab_music.ForeColor = System.Drawing.Color.White;
            this.tab_music.Location = new System.Drawing.Point(4, 24);
            this.tab_music.Name = "tab_music";
            this.tab_music.Padding = new System.Windows.Forms.Padding(3);
            this.tab_music.Size = new System.Drawing.Size(368, 376);
            this.tab_music.TabIndex = 1;
            this.tab_music.Text = "Music";
            // 
            // btn_music_add
            // 
            this.btn_music_add.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.btn_music_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_music_add.Location = new System.Drawing.Point(207, 195);
            this.btn_music_add.Name = "btn_music_add";
            this.btn_music_add.Size = new System.Drawing.Size(63, 28);
            this.btn_music_add.TabIndex = 9;
            this.btn_music_add.Text = "Add";
            this.btn_music_add.UseVisualStyleBackColor = true;
            this.btn_music_add.Click += new System.EventHandler(this.btn_music_add_Click);
            // 
            // btn_music_remove
            // 
            this.btn_music_remove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.btn_music_remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_music_remove.Location = new System.Drawing.Point(276, 195);
            this.btn_music_remove.Name = "btn_music_remove";
            this.btn_music_remove.Size = new System.Drawing.Size(76, 28);
            this.btn_music_remove.TabIndex = 8;
            this.btn_music_remove.Text = "Remove";
            this.btn_music_remove.UseVisualStyleBackColor = true;
            this.btn_music_remove.Click += new System.EventHandler(this.btn_music_remove_Click);
            // 
            // list_music
            // 
            this.list_music.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.list_music.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list_music.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.list_music.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_music.ForeColor = System.Drawing.Color.White;
            this.list_music.FormattingEnabled = true;
            this.list_music.IntegralHeight = false;
            this.list_music.ItemHeight = 15;
            this.list_music.Location = new System.Drawing.Point(18, 35);
            this.list_music.Name = "list_music";
            this.list_music.Size = new System.Drawing.Size(334, 154);
            this.list_music.TabIndex = 7;
            this.list_music.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.list_games_DrawItem);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(14, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 25);
            this.label9.TabIndex = 6;
            this.label9.Text = "Music Libraries";
            // 
            // tab_credits
            // 
            this.tab_credits.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.tab_credits.Controls.Add(this.label7);
            this.tab_credits.Controls.Add(this.lbl_notice);
            this.tab_credits.Controls.Add(this.linkLabel7);
            this.tab_credits.Controls.Add(this.linkLabel5);
            this.tab_credits.Controls.Add(this.linkLabel4);
            this.tab_credits.Controls.Add(this.linkLabel3);
            this.tab_credits.Controls.Add(this.label6);
            this.tab_credits.Controls.Add(this.label8);
            this.tab_credits.Location = new System.Drawing.Point(4, 24);
            this.tab_credits.Name = "tab_credits";
            this.tab_credits.Padding = new System.Windows.Forms.Padding(3);
            this.tab_credits.Size = new System.Drawing.Size(368, 376);
            this.tab_credits.TabIndex = 4;
            this.tab_credits.Text = "About";
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(362, 55);
            this.label7.TabIndex = 14;
            this.label7.Text = "ESSU is a program created by Nicholas Santiago (darc phansea) \r\nwith the goal to " +
    "merge Enhanced Steam (created by Jason Shackles)\r\nand the Steam Client (created " +
    "by Valve) together.";
            // 
            // lbl_notice
            // 
            this.lbl_notice.ForeColor = System.Drawing.Color.White;
            this.lbl_notice.Location = new System.Drawing.Point(3, 320);
            this.lbl_notice.Name = "lbl_notice";
            this.lbl_notice.Size = new System.Drawing.Size(362, 55);
            this.lbl_notice.TabIndex = 11;
            this.lbl_notice.Text = "Enhanced Steam Standalone Unofficial VERSION\r\n\r\nCopyright © 2018 Nicholas Santiag" +
    "o\r\nUnder GNU GPL v3 License";
            // 
            // linkLabel7
            // 
            this.linkLabel7.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(36)))), ((int)(((byte)(226)))));
            this.linkLabel7.AutoSize = true;
            this.linkLabel7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel7.ForeColor = System.Drawing.Color.White;
            this.linkLabel7.LinkColor = System.Drawing.Color.White;
            this.linkLabel7.Location = new System.Drawing.Point(6, 243);
            this.linkLabel7.Name = "linkLabel7";
            this.linkLabel7.Size = new System.Drawing.Size(209, 17);
            this.linkLabel7.TabIndex = 9;
            this.linkLabel7.TabStop = true;
            this.linkLabel7.Text = "-darc phansea (Nicholas Santiago)";
            this.linkLabel7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel7_LinkClicked);
            // 
            // linkLabel5
            // 
            this.linkLabel5.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(36)))), ((int)(((byte)(226)))));
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel5.ForeColor = System.Drawing.Color.White;
            this.linkLabel5.LinkColor = System.Drawing.Color.White;
            this.linkLabel5.Location = new System.Drawing.Point(6, 158);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(254, 17);
            this.linkLabel5.TabIndex = 8;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "-metroforsteam (ESSU uses some assests)";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // linkLabel4
            // 
            this.linkLabel4.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(36)))), ((int)(((byte)(226)))));
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel4.ForeColor = System.Drawing.Color.White;
            this.linkLabel4.LinkColor = System.Drawing.Color.White;
            this.linkLabel4.Location = new System.Drawing.Point(6, 136);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(243, 17);
            this.linkLabel4.TabIndex = 7;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "-Vivaldi (Browser used in the ESSU Shell)";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(36)))), ((int)(((byte)(226)))));
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel3.ForeColor = System.Drawing.Color.White;
            this.linkLabel3.LinkColor = System.Drawing.Color.White;
            this.linkLabel3.Location = new System.Drawing.Point(6, 113);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(226, 17);
            this.linkLabel3.TabIndex = 6;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "-Enhanced Steam (Kinda in the name)";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(362, 55);
            this.label6.TabIndex = 12;
            this.label6.Text = "Following are projects that help with the making of ESSU\r\n";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(6, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(359, 55);
            this.label8.TabIndex = 13;
            this.label8.Text = "Main Contributors of ESSU\r\n";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(368, 376);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "Important";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(362, 96);
            this.label2.TabIndex = 15;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // openFile
            // 
            this.openFile.FileName = "Steam.exe";
            // 
            // Settings_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 450);
            this.Controls.Add(this.panel_topbar);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings_Window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.panel_topbar.ResumeLayout(false);
            this.panel_topbar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tab_settings.ResumeLayout(false);
            this.tab_Inferface.ResumeLayout(false);
            this.tab_Inferface.PerformLayout();
            this.tab_Downloads.ResumeLayout(false);
            this.tab_Downloads.PerformLayout();
            this.tab_music.ResumeLayout(false);
            this.tab_music.PerformLayout();
            this.tab_credits.ResumeLayout(false);
            this.tab_credits.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_topbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_min;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.FolderBrowserDialog folder;
        private System.Windows.Forms.TabControl tab_settings;
        private System.Windows.Forms.TabPage tab_Inferface;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.CheckBox check_runAtStartup;
        private System.Windows.Forms.ComboBox combo_startupWindow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ComboBox combo_lang;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tab_Downloads;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.ListBox list_libraries;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tab_music;
        private System.Windows.Forms.TabPage tab_credits;
        private System.Windows.Forms.LinkLabel linkLabel7;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label lbl_notice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_music_add;
        private System.Windows.Forms.Button btn_music_remove;
        private System.Windows.Forms.ListBox list_music;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_sEXEopen;
        private System.Windows.Forms.Label lbl_steamexe;
        private System.Windows.Forms.TextBox txt_steamLocation;
    }
}