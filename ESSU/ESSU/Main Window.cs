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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using CefSharp;
using CefSharp.WinForms;
using System.Drawing.Imaging;
using System.Net.Sockets;

namespace ESSU
{
    public partial class Main_Window : Form
    {

        //Imports DLLs to bound the Vivaldi Browser into the ESSU application don't know what they particularly do...but it works
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr Handle, int x, int y, int w, int h, bool repaint);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);

        //Declaring Variables that will be used throughout the application
        Process vivaldi;

        int orgindex = 0;
        int indexGame = 0;
        int lastSelected = 0;
        public static int maxGames = 100000;
        static readonly int GWL_STYLE = -16;
        static readonly int WS_VISIBLE = 0x10000000;
        bool Installed = false;
        Point mousePos = new Point(0, 0);
        Form frm_friends = new Friends_Window();
        Form frm_settings = new Settings_Window();
        Form frm_categories = new Categories();
        Color cLightBlue = Color.FromArgb(255, 0, 188, 212);
        public string[,] gameArray = new string[maxGames, 6]; //Game Array of users installed games
        string[,] webGameArray = new string[maxGames, 4]; //Game Array of users uninstalled games

        //-------------------------------------------------------------------------------------------------------------------------------------------------------   

        public Main_Window() //Initial Startup of the main window
        {
            InitializeComponent(); //<<Thats needed
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);//Styling support
           
            //custom styles that I have to code in
            list_games.DrawMode = DrawMode.OwnerDrawFixed; 
            context_menu.Renderer = new MyRenderer();
            context_notification.Renderer = new MyRenderer();
            context_community.Renderer = new MyRenderer();
            context_gameMenu.Renderer = new MyRenderer();
            context_profile.Renderer = new MyRenderer();
            context_store.Renderer = new MyRenderer();
            context_tray.Renderer = new MyRenderer();

            switch (Settings.startwin) //What window is defaulted to open
            {
                case "Store":
                    launchVivaldi("http://store.steampowered.com/");
                    setActiveFromPanel(btn_Store, btn_library, btn_commuity, btn_user);
                    break;
                case "Library":
                    setActiveFromPanel(btn_library, btn_Store, btn_commuity, btn_user);
                    panel_browser.Left = panel_Libary.Width + 10000;
                    break;
                case "News":
                    setActiveFromPanel(btn_Store, btn_library, btn_commuity, btn_user);
                    launchVivaldi("http://store.steampowered.com/news");
                    break;
                case "Friends":
                    setActiveFromPanel(btn_commuity, btn_library, btn_Store, btn_user);
                    launchVivaldi("http://steamcommunity.com/id/darcphansea/friends/");
                    break;
                case "Friend Activity":
                    setActiveFromPanel(btn_commuity, btn_library, btn_Store, btn_user);
                    launchVivaldi("http://steamcommunity.com/my/home/");
                    break;
                case "Community Home":
                    setActiveFromPanel(btn_commuity, btn_library, btn_Store, btn_user);
                    launchVivaldi("http://steamcommunity.com/home");
                    break;
                default:
                    setActiveFromPanel(btn_Store, btn_library, btn_community, btn_user);
                    launchVivaldi("http://store.steampowered.com/");
                    break;
            }

            Settings.init(); //Loads any settings, such as Steam game locations, steam.exe location, and shit

            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea; //maximized doesnt cover taskbar

            steamGameListUpdate(); //Starts does magic to get your games.

            if (list_games.Items.Count > 0) //Sets selected game to first index and load game image
            {
                list_games.SelectedIndex = 0;
                loadImage(gameArray[list_games.SelectedIndex, 0]);
            }
            
        }
        private void frm_main_Load_1(object sender, EventArgs e) //More Startup shit
        {
            try
            {
                foreach (string line in File.ReadAllLines(Application.StartupPath + "\\library.txt"))
                {
                    if (line.Contains("<title>"))
                    {
                        string l = line;
                        l = l.Replace("<title>Steam Community :: ", string.Empty);
                        l = l.Replace(" :: Games</title>", string.Empty);
                        if (l.Length >= 18) { l = l.Substring(0, 15) + "..."; }
                        btn_user.Text = l;
                    }
                }
            }
            catch { }
            this.BringToFront();
            this.Size = this.MinimumSize;
            picture_game_preview.Width = 494;
        }
        private void Main_Window_FormClosing(object sender, FormClosingEventArgs e)//When app closes...it closes mostly everything i need it to
        {
            if (!vivaldi.HasExited) vivaldi.Kill();
            if (Cef.IsInitialized) Cef.Shutdown();

            switch (e.CloseReason)
            {
                case CloseReason.ApplicationExitCall:
                    notifyMe.Visible = false;
                    break;
                case CloseReason.TaskManagerClosing:
                    notifyMe.Visible = false;
                    break;
                case CloseReason.UserClosing:
                    notifyMe.Visible = false;
                    break;
                case CloseReason.WindowsShutDown:
                    notifyMe.Visible = false;
                    break;

            }
        }

        private Bitmap ChangeAlpha(Bitmap image, int alpha) //Makes image transparent
        {
            Bitmap output = new Bitmap(image);
            for (int x = 0; x < output.Width; x++)
            {
                for (int y = 0; y < output.Height; y++)
                {
                    Color color = output.GetPixel(x, y);
                    output.SetPixel(x, y, Color.FromArgb(alpha, color.R, color.G, color.B));
                }
            }
            return output;
        }
        void loadImage(string appid) //tries to load game image if any, if not it sets the image to null
        {
            try
            {
                var request = WebRequest.Create("http://cdn.akamai.steamstatic.com/steam/apps/" + appid + "/header.jpg");
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        Image b = Bitmap.FromStream(stream);

                        picture_game_preview.Image = ChangeAlpha(b as Bitmap, 100);
                    }
                }
            }
            catch
            {
                picture_game_preview.Image = null;
            }

        }
        
        //Custom Styles
        private class MyRenderer : ToolStripProfessionalRenderer 
        {
            public MyRenderer() : base(new MyColors()) { }
        }
        private class MyColors : ProfessionalColorTable 
        {
            public override Color MenuItemSelected
            {
                get { return Color.FromArgb(255, 102, 36, 226); }
            }
            public override Color ImageMarginGradientBegin
            {
                get { return Color.FromArgb(255, 26, 26, 26); }
            }
            public override Color ImageMarginGradientMiddle
            {
                get { return Color.FromArgb(255, 26, 26, 26); }
            }
            public override Color ImageMarginGradientEnd
            {
                get { return Color.FromArgb(255, 26, 26, 26); }
            }
        }
        private void list_games_DrawItem(object sender, DrawItemEventArgs e) //More Custom Styles
        {
            //list_games.ItemHeight = 22;
            SolidBrush reportsForegroundBrushSelected = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
            SolidBrush reportsForegroundBrush = new SolidBrush(Color.White);
            SolidBrush reportsForegroundBrush2 = new SolidBrush(Color.Gray);
            SolidBrush reportsForegroundBrush3 = new SolidBrush(Color.FromArgb(255, 102, 36, 226));
            SolidBrush reportsBackgroundBrushSelected = new SolidBrush(Color.FromArgb(255, 102, 36, 226));
            SolidBrush reportsBackgroundBrush1 = new SolidBrush(Color.FromArgb(255, 39, 39, 39));
            e.DrawBackground();
            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            bool isInstalled = false;

            if (index >= 0 && index < list_games.Items.Count)
            {
                string text = list_games.Items[index].ToString();
                for (int x = 0; x < maxGames; x++)
                {
                    if (String.IsNullOrEmpty(gameArray[x, 0])) continue;
                    if (list_games.Items[index].ToString() == gameArray[x, 0])
                    {
                        isInstalled = gameArray[x, 4] != null;
                    }
                }
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
                if (list_games.Items[index].ToString().StartsWith("▶ ") || list_games.Items[index].ToString().StartsWith("▼ "))
                {
                    g.DrawString(text, e.Font, foregroundBrush3, e.Bounds);
                }
                else
                {
                    if (isInstalled)
                    {
                        g.DrawString(text, e.Font, foregroundBrush, e.Bounds);
                    }
                    else
                    {
                        g.DrawString(text, e.Font, foregroundBrush2, e.Bounds);
                    }
                }
            }
            e.DrawFocusRectangle();
        }

        private string getHTMLContents(string URL) //Reads the HTML contents of a give website
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(URL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();
            return result;
        }
        private void launchVivaldi(string url) //Launches Browser to a given URL sadly browser needs to restart everytime for now
        {
            Process[] myProcesses = Process.GetProcessesByName("vivaldi");
            if (myProcesses.Count() > 0)
            {
                foreach (Process proc in myProcesses)                         //Kills The Vivaldi Process if any
                {
                    try { proc.Kill(); }
                    catch { }
                }
            }
            panel_browser.Left = 11;
            panel_browser.BringToFront();
            vivaldi = Process.Start("vivaldi.exe", "--force-renderer-accessibility " + url); //Starts Vivaldi to the URL and with force-render-accessibility (dunno why i did it but i did)
            while (vivaldi.MainWindowHandle == (IntPtr)0) { }; //waits until the browser window exist
            SetParent(vivaldi.MainWindowHandle, panel_browser.Handle); //embeds the browser to the application in a panel called "panel_browser"
            SetWindowLong(vivaldi.MainWindowHandle, GWL_STYLE, WS_VISIBLE); //I honestly dont know what this does but its needed i think
        }

        public void steamGameListUpdate() //Here we go...This adds all the necessary info i need about the games you have
        {
            orgindex = 0; //just an index counter
            list_games.Items.Clear(); //clears your current library list
            for (int i = 0; i < Settings.steamDirectories.Length; i++) //checking all steam directories you have added to the app
            {
                if (String.IsNullOrEmpty(Settings.steamDirectories[i])) continue; //if directory is blank, skip it
                if (!Directory.Exists(Settings.steamDirectories[i])) continue; //if directory doesnt exist, skip it
                string steamApp = Settings.steamDirectories[i] + "\\"; //steamapp directory
                DirectoryInfo directory = new DirectoryInfo(steamApp); 

                foreach (var file in directory.GetFiles("*.acf", SearchOption.TopDirectoryOnly)) //gets all the appmanifest_##.acf files from the directory
                {
                    string appManifest = steamApp + file.Name;
                    string appid = "0";
                    string name = null;
                    string installdir = null;

                    for (int z = 0; z < File.ReadLines(appManifest).Count(); z++)
                    {
                        appid = file.Name.Substring(12, file.Name.Length - 16);
                        string line = File.ReadLines(appManifest).Skip(z).Take(1).First();
                        if (line.Contains("name"))
                        {
                            name = line.Substring(10, line.Length - 11); //Game name
                        }
                        if (line.Contains("installdir"))
                        {
                            installdir = steamApp + "common\\" + line.Substring(16, line.Length - 17); //where game is installed
                        }
                    }
                    string Sname = name; //Game list display name
                    if (Sname.Length > 35) { Sname = name.Substring(0, 35) + "..."; } //Shortens name if too long
                    gameArray[orgindex, 0] = Sname; //Sname = shorten name
                    gameArray[orgindex, 1] = appid; // the app id of the game
                    gameArray[orgindex, 2] = installdir; //install directory of game
                    gameArray[orgindex, 3] = name; //the full name of game
                    gameArray[orgindex, 4] = steamApp; //the steamapp folder it is contained it (when you have multiple steam app folders, dont think its really needed but idc)

                    try
                    {
                        foreach (string line in File.ReadAllLines(Application.StartupPath + "\\c.dat")) //c.dat is the data file for you categories list if you have set one up
                        {
                            foreach (string l in File.ReadAllLines(Application.StartupPath + "\\" + line + ".dat"))
                            {
                                if (l == gameArray[orgindex, 3])
                                {
                                    gameArray[orgindex, 5] = line; //Sets the games category to the correct one
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(gameArray[orgindex, 5])) gameArray[orgindex, 5] = "Games"; //incase game doesnt have a category
                    }
                    catch
                    {
                        gameArray[orgindex, 5] = "Games"; //If you dont have categories setup yet
                    }
                    orgindex++; //NEXT FILE NOW
                }
            }

            loadLib(); //load any games you dont have install [See loadlib function for more details]

            try
            {
                foreach (string line in File.ReadAllLines(Application.StartupPath + "\\c.dat"))
                {
                    if (!(line.Length > 0)) continue;
                    list_games.Items.Add("▶ " + line);
                }   
            }
            catch                                                                                       //This pissed me off so much, but it helps make the categories feature work
            {
                for (int i = 0; i < maxGames; i++)
                {
                    if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                    list_games.Items.Add(gameArray[i, 0]);
                }
            }

            try { if (list_games.Items.Count >= 0) list_games.SelectedIndex = 0; } catch { } //tries to select the first game (tries, because you might not have any game)
        }

        void loadLib() //Some fun shit
        {
            indexGame = 0; //index counter
            try
            {
                foreach (string line in File.ReadLines(Application.StartupPath + "\\library.txt")) //gets the full library you have from a .txt file created when you first login at the login window
                {
                    if (line.Contains("http://steamcommunity.com/app/")) //looks for a valid game
                    {
                        string l = line.Replace("<a href=\"http://steamcommunity.com/app/", string.Empty);
                        l = l.Replace("\">", string.Empty);
                        l = l.Replace("	", string.Empty);
                        webGameArray[indexGame, 0] = l;
                    }
                    //Checking for tags that shows its a game
                    if (line.Contains("<div class=\"gameListRowItemName ellipsis \">") || line.Contains("<div class=\"gameListRowItemName ellipsis color_uninstalled\">") || line.Contains("<div class=\"gameListRowItemName ellipsis color_disabled\">"))
                    {
                        if (line.Contains("<div class=\"gameListRowItemName ellipsis color_disabled\">"))
                        {
                            webGameArray[indexGame, 0] = null; //if game is not a game you can play (dlc and shit) skip it
                            webGameArray[indexGame, 1] = null;
                            continue;
                        }
                        for (int i = 0; i < indexGame; i++)
                        {
                            if (webGameArray[i, 0] == webGameArray[indexGame, 0]) //no duplicates, i think, i cant remember
                            {
                                webGameArray[indexGame, 0] = null;
                                webGameArray[indexGame, 1] = null;
                                continue;
                            }
                        }
                        string l = line;
                        l = l.Replace("<div class=\"gameListRowItemName ellipsis color_uninstalled\">", string.Empty); //removes tags so only title is left
                        l = l.Replace("<div class=\"gameListRowItemName ellipsis \">", string.Empty);
                        l = l.Replace("</div>", string.Empty);
                        l = l.Replace("	", string.Empty);
                        l = l.Replace("™", string.Empty);
                        webGameArray[indexGame, 1] = l; //add the web game list
                        indexGame++; //NEXT GAME
                    }
                }
            }
            catch { }

            foreach (var item in gameArray)
            {
                for (int y = 0; y < indexGame; y++)
                {
                    if (String.IsNullOrEmpty(webGameArray[y, 0])) continue;
                    if (String.IsNullOrEmpty(item)) continue;

                    if (item.ToString().Contains(webGameArray[y, 0]))   //no duplicates to the installed games list
                    {  
                        webGameArray[y, 0] = null;
                        webGameArray[y, 1] = null;
                    }
                }
            }

            for (int y = 0; y < indexGame; y++)
            {
                if (String.IsNullOrEmpty(webGameArray[y, 0])) continue;

                string Sname = webGameArray[y, 1];
                if (Sname.Length > 35) Sname = webGameArray[y, 1].Substring(0, 35) + "...";

                gameArray[orgindex, 0] = Sname;
                gameArray[orgindex, 1] = webGameArray[y, 0];
                gameArray[orgindex, 3] = webGameArray[y, 1];
                try
                {
                    foreach (string line in File.ReadAllLines(Application.StartupPath + "\\c.dat"))
                    {
                        foreach (string l in File.ReadAllLines(Application.StartupPath + "\\" + line + ".dat"))   //Checks for categories again but this time for unistalled games
                        {
                            if (l == gameArray[orgindex, 3])
                            {
                                gameArray[orgindex, 5] = line;
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(gameArray[orgindex, 5])) gameArray[orgindex, 5] = "Games";
                }
                catch
                {
                    gameArray[orgindex, 5] = "Games";
                }
                orgindex++; //NEXT
            }
        }

        void showCate() //Category Magic
        {

            string[] listarray = new string[10000];

            for (int i = 0; i < list_games.Items.Count; i++)
            {
                if (list_games.Items[i].ToString().StartsWith("▼ ")) listarray[i] = list_games.Items[i].ToString();
                if (list_games.Items[i].ToString().StartsWith("▶ ")) listarray[i] = list_games.Items[i].ToString();

            }

            list_games.Items.Clear();
            foreach (string cat in listarray)
            {
                if (String.IsNullOrEmpty(cat)) continue;
                list_games.Items.Add(cat);
                if (cat.StartsWith("▼ "))
                {
                    for (int x = 0; x < 10000; x++)
                    {
                        if (gameArray[x, 5] == cat.Substring(2))
                        {

                            list_games.Items.Add(gameArray[x, 0]);
                        }
                    }
                }
            }
            list_games.SelectedIndex = -1;
        }

        private void startGame(string appid) //launches a steam game
        {
            try
            {
                Process Steam = new Process();
                Steam.StartInfo.FileName = Settings.steamEXE;
                Steam.StartInfo.Arguments = "-applaunch " + appid;  //yes it that simple
                Steam.StartInfo.UseShellExecute = false;
                Steam.StartInfo.CreateNoWindow = false; //so the steam client dont appear
                Steam.Start();
            }
            catch
            {
                MessageBox.Show(Settings.steamEXE);
            }
        }

        private void refresh_Tick(object sender, EventArgs e) //Thats a timer that runs 100 times a second, or at least thats what it says it does
        {
            try
            {
                if (list_games.SelectedIndex == -1) list_games.SelectedIndex = lastSelected; //always have atleast 1 game selected
            } catch { }
            
            int[] index = new int[list_games.Items.Count];

            if (Settings.tempName == "startRefresh") //Refresh the library
            {
                steamGameListUpdate();
                for (int x = 0; x < list_games.Items.Count; x++)
                {
                    if (list_games.Items[x].ToString().StartsWith("▶ "))
                    {
                        list_games.Items[x] = list_games.Items[x].ToString().Replace("▶ ", "▼ "); 
                        showCate();
                    }
                }
                Settings.tempName = null;
            }

            if (Settings.bookmarkGOTO != string.Empty) //Goes to a bookmark you want to launch 
            {
                try
                {
                    launchVivaldi(Settings.bookmarkGOTO); 
                } catch { }
                Settings.bookmarkGOTO = string.Empty;
            }

            if (this.Width == this.MinimumSize.Width && picture_game_preview.Width != 619) picture_game_preview.Width = 619; //Game preview is always correct size on window load 
            //(for some reason there is a strange bug where it loaded WAY to large, this is an ugly fix)

            if (list_games.SelectedIndex == -1) picture_game_preview.Image = null; //if by anycase there is no game selected, remove the game preview image

            //correct image aspect ratio
            if (picture_game_preview.Height != Math.Round(picture_game_preview.Width * 0.4673913043478261)) picture_game_preview.Height = (int)Math.Round(picture_game_preview.Width * 0.4673913043478261);

            //Ugly fix to prevent more than 1 ESSU from opening
            if (File.Exists(Application.StartupPath + "\\es.su")) { File.Delete(Application.StartupPath + "\\es.su"); this.Show(); this.WindowState = FormWindowState.Normal; }

            //always center the browser window
            try { MoveWindow(vivaldi.MainWindowHandle, 0, -28, panel_browser.Width, panel_browser.Height + 25, true); } catch { }
        }


        //Window Control Size and Shit--------------------------------------------------------------------------
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            mousePos = new Point(e.X, e.Y);
        } 
        private void resize_MouseDown(object sender, MouseEventArgs e)
        {
            mousePos = new Point(e.X, e.Y);
        }
        private void btn_exit_Click(object sender, EventArgs e)
        { 
            WindowState = FormWindowState.Minimized;
            Hide();
        }
        private void btn_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }                                                                                    
        private void btn_Max_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                btn_Max.BackgroundImage = ESSU.Properties.Resources.win32_win_restore;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                btn_Max.BackgroundImage = ESSU.Properties.Resources.win32_win_max;
                WindowState = FormWindowState.Normal;
                this.Size = new Size(1011, 680);
            }
        }
        private void panel_topbar_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                btn_Max.BackgroundImage = ESSU.Properties.Resources.win32_win_restore;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                btn_Max.BackgroundImage = ESSU.Properties.Resources.win32_win_max;
                WindowState = FormWindowState.Normal;
            }
        }
        private void frm_main_Resize(object sender, EventArgs e)
        {
            this.Update();
        }
        private void frm_main_Resize_1(object sender, EventArgs e)
        {
            this.Update();
        }
        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top += e.Y - mousePos.Y;
                this.Left += e.X - mousePos.X;

                if (WindowState == FormWindowState.Maximized)
                {
                    btn_Max.BackgroundImage = ESSU.Properties.Resources.win32_win_max;
                    WindowState = FormWindowState.Normal;
                }
            }
        }
        private void resize_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Width += e.X - mousePos.X;
                this.Height += e.Y - mousePos.Y;
            }
        }
        private void lbl_resizeB_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Height += e.Y - mousePos.Y;
            }
        }
        private void label10_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((this.Width != this.MinimumSize.Width) || e.X - mousePos.X < 0)
                {
                    this.Left += e.X - mousePos.X;
                    this.Width -= e.X - mousePos.X;

                }
                if ((this.Height != this.MinimumSize.Height) || e.X - mousePos.X < 0)
                {
                    this.Height += e.Y - mousePos.Y;
                }
            }
        }
        private void label8_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((this.Width != this.MinimumSize.Width) || e.X - mousePos.X < 0)
                {
                    this.Left += e.X - mousePos.X;
                    this.Width -= e.X - mousePos.X;
                }
            }
        }
        private void resizeL_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (this.Width != this.MinimumSize.Width || e.X - mousePos.X < 0))
            {
                this.Left += e.X - mousePos.X;
                this.Width -= e.X - mousePos.X;
            }
        }
        private void resizeBL_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.Width != this.MinimumSize.Width || e.X - mousePos.X < 0)
                {
                    this.Left += e.X - mousePos.X;
                    this.Width -= e.X - mousePos.X;

                }
                if (this.Height != this.MinimumSize.Height || e.Y - mousePos.Y > 0)
                {
                    this.Height += e.Y - mousePos.Y;
                }

            }
        }
        private void resizeB_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Height += e.Y - mousePos.Y;
            }
        }
        private void resizeR_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Width += e.X - mousePos.X;
            }
        }

        //The Top Bar Buttons (From left to right)--------------------------------------------------------------
        private void btn_menu_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (!context_menu.Visible) context_menu.Show(c, new Point(2, c.Height + 2));
        }
        private void btn_Store_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_Store, btn_library, btn_commuity, btn_user);
            launchVivaldi("store.steampowered.com");
        }
        private void btn_library_Click(object sender, EventArgs e)
        {
            steamGameListUpdate();
            setActiveFromPanel(btn_library, btn_Store, btn_commuity, btn_user);
            panel_browser.Left = panel_Libary.Width + 100;
            list_games.Focus();
            //txt_searchForGame.Focus();
        }
        private void btn_commuity_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_commuity, btn_Store, btn_library, btn_user);
            launchVivaldi("www.steamcommunity.com/");
        }
        private void btn_user_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_user, btn_Store, btn_library, btn_commuity);
            launchVivaldi("steamcommunity.com/my");
        }
        private void btn_account_Click(object sender, EventArgs e)
        {
            launchVivaldi("https://store.steampowered.com/account/store_transactions/");
        }
        private void btn_notifications_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (!context_notification.Visible) context_notification.Show(c, new Point(c.Width - context_notification.Width, c.Height));
            else context_notification.Hide();
        }
        private void btn_friends_Click(object sender, EventArgs e)
        {
            if (!frm_friends.Visible)
            {
                frm_friends = new Friends_Window();
                frm_friends.Show();
            }
            else
            {
                frm_friends.WindowState = FormWindowState.Normal;
                frm_friends.BringToFront();
            }

        }
        private void setActiveFromPanel(Control now, Control a, Control b, Control c) //Makes glowy button glow (and ik it shit but it temp fix for now)
        {
            now.ForeColor = Color.White;
            a.ForeColor = Color.FromArgb(255, 137, 137, 137);
            b.ForeColor = a.ForeColor;
            c.ForeColor = a.ForeColor;
        }

        //Library game button controls--------------------------------------------------------------------------
        private void btn_playGame_Click(object sender, EventArgs e)
        {
            try
            {
                if (list_games.SelectedIndex == -1) return;
                for (int i = 0; i < maxGames; i++)
                {
                    if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                    if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                    {
                        startGame(gameArray[i, 1]);
                    }
                }
            } catch { }
        }
        private void btn_storePage_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex == -1) return;
            setActiveFromPanel(btn_Store, btn_library, btn_commuity, btn_user);
            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    launchVivaldi("store.steampowered.com/app/" + gameArray[i, 1]);
                }
            }        
        }
        private void news_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex == -1) return;
            setActiveFromPanel(btn_Store, btn_library, btn_commuity, btn_user);
            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    launchVivaldi("http://store.steampowered.com/news/?appids=" + gameArray[i, 1]);
                }
            }    
        }
        private void btn_community_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex == -1) return;
            setActiveFromPanel(btn_commuity, btn_library, btn_Store, btn_user);
            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    launchVivaldi("http://www.steamcommunity.com/app/" + gameArray[i, 1]);
                }
            } 
        }
        private void btn_LocalFiles_Click(object sender, EventArgs e)
        {
            try
            {
                if (list_games.SelectedIndex == -1) return;
                for (int i = 0; i < maxGames; i++)
                {
                    if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                    if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                    {
                        Process.Start(@gameArray[i, 2]);
                    }
                }
            } catch { }
            
        }
        private void btn_friendsThatPlay_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex == -1) return;
            setActiveFromPanel(btn_commuity, btn_Store, btn_library, btn_user);

            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    launchVivaldi("http://steamcommunity.com/id/darcphansea/friendsthatplay/" + gameArray[i, 1]);
                }
            }
        }
        private void btn_discussions_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex == -1) return;
            setActiveFromPanel(btn_commuity, btn_library, btn_Store, btn_user);
            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    launchVivaldi("http://steamcommunity.com/app/" + gameArray[i, 1] + "/discussions/");
                }
            }          
        }
        private void btn_relatedG_Click(object sender, EventArgs e)
        {

            if (list_games.SelectedIndex == -1) return;
            setActiveFromPanel(btn_commuity, btn_library, btn_Store, btn_user);
            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    launchVivaldi("http://steamcommunity.com/search/groups/?text=" + gameArray[i, 3].Replace(" ", "+"));
                }
            }           
        }
        private void btn_dlc_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex == -1) return;
            setActiveFromPanel(btn_Store, btn_library, btn_commuity, btn_user);
            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    launchVivaldi("http://store.steampowered.com/dlc/" + gameArray[i, 1]);
                }
            } 
        }
        private void btn_guides_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex == -1) return;
            setActiveFromPanel(btn_commuity, btn_library, btn_Store, btn_user);
            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    launchVivaldi("http://steamcommunity.com/app/" + gameArray[i, 1] + "/guides/");
                }
            }    
        }
        private void btn_support_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex == -1) return;
            setActiveFromPanel(btn_Store, btn_library, btn_commuity, btn_user);
            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    launchVivaldi("https://help.steampowered.com/en/wizard/HelpWithGame/?appid=" + gameArray[i, 1]);
                }
            }
            
        }
        private void btn_review_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex == -1) return;
            setActiveFromPanel(btn_Store, btn_library, btn_commuity, btn_user);
            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    launchVivaldi("http://store.steampowered.com/recommended/recommendgame/" + gameArray[i, 1]);
                }
            }
            
        }
        private void btn_achieve_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex == -1) return;
            setActiveFromPanel(btn_commuity, btn_Store, btn_library, btn_user);
            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    launchVivaldi("http://steamcommunity.com/my/stats/appid/" + gameArray[i, 1] + "/achievements");
                }
            }
        }
        private void btn_workshop_Click(object sender, EventArgs e)
        {
            if (list_games.SelectedIndex == -1) return;
            setActiveFromPanel(btn_commuity, btn_Store, btn_library, btn_user);
            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    launchVivaldi("http://steamcommunity.com/app/" + gameArray[i, 1] + "/workshop");
                }
            }
        }
        private void btn_delete_Click(object sender, EventArgs e) //delete game (still in the works)
        {   
            for (int i = 0; i < maxGames; i++)
            {
                if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    string prt1 = "This will delete all " + gameArray[i, 3] + " game content from this computer" + Environment.NewLine + Environment.NewLine;
                    string prt2 = "This game will remain in your Game Library, but to play it in the future you'll have to first re-download its content";
                    DialogResult dialogResult = MessageBox.Show(prt1 + prt2, "Uninstall " + gameArray[i, 3], MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        string delAppMani = gameArray[i, 4] + "appmanifest_" + gameArray[i, 1] + ".acf";
                        //MessageBox.Show(delAppMani);
                        File.Delete(delAppMani);
                        Directory.Delete(gameArray[i, 2], true);
                    }
                }
            }
            steamGameListUpdate();
        }

        //GAME LIST---------------------------------------------------------------------------------------------
        private void list_games_SelectedIndexChanged(object sender, EventArgs e)
        {

            try { if (list_games.SelectedIndex != -1) lastSelected = list_games.SelectedIndex; } catch { }
            if (list_games.SelectedIndex == -1)
            {
                picture_game_preview.Image = null;                                                                           //Show Image and Game Title
                lbl_gameTitle.Text = string.Empty;
                return;
            }
            try
            {
                //NEWS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                for (int i = 0; i < gameArray.GetLongLength(0); i++)
                {
                    if (String.IsNullOrEmpty(gameArray[i, 0])) continue;
                    if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                    {
                        loadImage(gameArray[i, 1]);
                        lbl_gameTitle.Text = gameArray[i, 3];

                        string title = getHTMLContents("http://store.steampowered.com/news/?appids=" + gameArray[i, 1]);
                        title = Regex.Replace(title, "<.*?>", String.Empty);
                        title = Regex.Replace(title, @"<a\b[^>]+>([^<]*(?:(?!</a)<[^<]*)*)</a>", "$1");
                        title = Regex.Replace(title, "Share:", String.Empty);
                        title = title.Substring(title.IndexOf("Headlines") + 105);
                        title = Regex.Replace(title, "#.*?;", String.Empty);            //Remove Html shit
                        title = Regex.Replace(title, "nbsp;", " ");
                        title = Regex.Replace(title, "apos;", "'");
                        title = Regex.Replace(title, "amp;", "&");
                        title = Regex.Replace(title, "quot;", "\"");
                        title = title.Replace("(more&hellip;)", "[Read More]");

                        title = title.Substring(0, title.IndexOf(Environment.NewLine + "						&"));
                        if (title.Contains("<a href="))
                        {
                            string rep = title.Substring(title.IndexOf("<a href="), title.IndexOf("\">") - 47);
                            title = title.Replace(rep, "");
                        }
                        if (title.Length > 1000) title = title.Substring(0, 1000) + "...\n\n";
                        lbl_newsParagraph.Text = title;
                        panel_library_details.AutoScrollMargin = new Size(0, lbl_newsParagraph.Height); //Change scroll height for game details page

                        if (!string.IsNullOrEmpty(gameArray[i, 2])) { btn_playGame.BackgroundImage = ESSU.Properties.Resources.play; Installed = true; label8.Text = "\n               Play Game"; } //installed
                        else { btn_playGame.BackgroundImage = ESSU.Properties.Resources.install; Installed = false; label8.Text = "\n               Install Game"; } //not installed
                        return;
                    }
                    else
                    {
                        picture_game_preview.Image = null;
                        lbl_gameTitle.Text = string.Empty;
                        continue;
                    }
                }

            }
            catch
            {
                
            }

        }
        private void list_games_KeyUp(object sender, KeyEventArgs e) //using arrow keys and enter
        {
            if (list_games.SelectedIndex == -1) return;
            var item = list_games.SelectedItem;
            int ind = list_games.SelectedIndex;
            if (e.KeyCode == Keys.Enter)
            {
                if (list_games.SelectedItem.ToString().StartsWith("▼ ") || list_games.SelectedItem.ToString().StartsWith("▶ "))
                {
                    try
                    {
                        if (list_games.SelectedItem.ToString().StartsWith("▶ "))
                        {
                            list_games.Items[list_games.SelectedIndex] = list_games.SelectedItem.ToString().Replace("▶ ", "▼ ");
                            showCate();
                            list_games.SelectedIndex = ind;
                        }
                        else if (list_games.SelectedItem.ToString().StartsWith("▼ "))
                        {
                            list_games.Items[list_games.SelectedIndex] = list_games.SelectedItem.ToString().Replace("▼ ", "▶ ");
                            showCate();
                            list_games.SelectedIndex = ind;
                        }

                    }
                    catch { }

                }
                else
                {
                    btn_playGame.PerformClick();
                }
            }
        }
        private void list_games_Click(object sender, EventArgs e)
        {
            try
            {
                if (list_games.SelectedItem.ToString().StartsWith("▶ "))
                {
                    list_games.Items[list_games.SelectedIndex] = list_games.SelectedItem.ToString().Replace("▶ ", "▼ ");
                    showCate();
                }
                else if (list_games.SelectedItem.ToString().StartsWith("▼ "))
                {
                    list_games.Items[list_games.SelectedIndex] = list_games.SelectedItem.ToString().Replace("▼ ", "▶ ");
                    showCate();
                }
            }
            catch { }
        }

        //Search for game in library----------------------------------------------------------------------------
        private void txt_searchForGame_Enter(object sender, EventArgs e)
        {

            for (int x = 0; x < list_games.Items.Count; x++)
            {
                if (list_games.Items[x].ToString().StartsWith("▶ "))
                {
                    list_games.Items[x] = list_games.Items[x].ToString().Replace("▶ ", "▼ ");
                    showCate();
                }
            }

        }
        private void txt_searchForGame_Leave(object sender, EventArgs e)
        {
            txt_searchForGame.Text = string.Empty;
            for (int x = 0; x < list_games.Items.Count; x++)
            {
                if (list_games.Items[x].ToString().StartsWith("▼ "))
                {
                    list_games.Items[x] = list_games.Items[x].ToString().Replace("▼", "▶ ");
                    showCate();
                }
            }

        }
        private void txt_searchForGame_KeyUp(object sender, KeyEventArgs e) //My "Super Hi-Tech" Search algorithm
        {
            if (txt_searchForGame.Text != string.Empty) btn_erase_Text.Visible = true;
            else btn_erase_Text.Visible = false;
            string cat = "▼".ToLower();
            foreach (var item in list_games.Items)
            {
                string i = item.ToString().ToLower();
                string s = txt_searchForGame.Text.ToLower();
                i = i.Replace("-", string.Empty);
                s = s.Replace("-", string.Empty);
                i = i.Replace(":", string.Empty);
                s = s.Replace(":", string.Empty);
                i = i.Replace(" ", string.Empty);
                s = s.Replace(" ", string.Empty);

                #region replaces numbers
                i = i.Replace("1", "one");
                s = s.Replace("1", "one");
                i = i.Replace("2", "two");
                s = s.Replace("2", "two");
                i = i.Replace("3", "three");
                s = s.Replace("3", "three");
                i = i.Replace("4", "four");
                s = s.Replace("4", "four");
                i = i.Replace("5", "five");
                s = s.Replace("5", "five");
                i = i.Replace("6", "six");
                s = s.Replace("6", "six");
                i = i.Replace("7", "seven");
                s = s.Replace("7", "seven");
                i = i.Replace("8", "eight");
                s = s.Replace("8", "eight");
                i = i.Replace("9", "nine");
                s = s.Replace("9", "nine");
                i = i.Replace("0", "zero");
                s = s.Replace("0", "zero");
                #endregion


                if (i.Contains(s) && !i.Contains(cat))
                {
                    //Supposed To Check If The Selected Item Is A Better Target Such As 
                    //"Half Life" Selecting Half Life 2 Instead Of Half Life
                    foreach (var item2 in list_games.Items)
                    {
                        string i2 = item2.ToString().ToLower();
                        i2 = i2.Replace("-", string.Empty);
                        i2 = i2.Replace(":", string.Empty);
                        i2 = i2.Replace(" ", string.Empty);
                        string s2 = txt_searchForGame.Text.ToLower();
                        s2 = s2.Replace("-", string.Empty);
                        s2 = s2.Replace(":", string.Empty);
                        s2 = s2.Replace(" ", string.Empty);

                        #region replaces numbers
                        i2 = i2.Replace("1", "one");
                        s2 = s2.Replace("1", "one");
                        i2 = i2.Replace("2", "two");
                        s2 = s2.Replace("2", "two");
                        i2 = i2.Replace("3", "three");
                        s2 = s2.Replace("3", "three");
                        i2 = i2.Replace("4", "four");
                        s2 = s2.Replace("4", "four");
                        i2 = i2.Replace("5", "five");
                        s2 = s2.Replace("5", "five");
                        i2 = i2.Replace("6", "six");
                        s2 = s2.Replace("6", "six");
                        i2 = i2.Replace("7", "seven");
                        s2 = s2.Replace("7", "seven");
                        i2 = i2.Replace("8", "eight");
                        s2 = s2.Replace("8", "eight");
                        i2 = i2.Replace("9", "nine");
                        s2 = s2.Replace("9", "nine");
                        i2 = i2.Replace("0", "zero");
                        s2 = s2.Replace("0", "zero");
                        #endregion

                        if (i2 == s2 || i2.EndsWith(s2) && !i2.Contains(cat))
                        {
                            list_games.SelectedItem = item2;
                            return;
                        }

                    }
                    list_games.SelectedItem = item;
                    return;
                }
                continue;
            }
        }
        
        //Toolstrip buttons... To Lazy To Organize-------------------------------------------------------------
        private void restartBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_Store, btn_library, btn_commuity, btn_user);
            panel_browser.Left = panel_Libary.Width + 10000;
            launchVivaldi("store.steampowered.com");
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            launchVivaldi("steamcommunity.com/my/commentnotifications");
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            launchVivaldi("steamcommunity.com/my/inventory");
        }
        private void newInvitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchVivaldi("steamcommunity.com/my/home/invites/");
        }
        private void newGiftsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchVivaldi("steamcommunity.com/my/inventory/#pending_gifts");
        }
        private void vivaldiExtensionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchVivaldi("vivaldi://extensions");
        }
        private void signInToSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchVivaldi("https://store.steampowered.com//login/");
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void logoutOfSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchVivaldi("https://store.steampowered.com/logout/");
        }
        private void enhancedSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchVivaldi("http://www.enhancedsteam.com/");
        }
        private void discussionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_commuity, btn_Store, btn_library, btn_user);
            launchVivaldi("http://steamcommunity.com/discussions/");
        }
        private void workshopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_commuity, btn_Store, btn_library, btn_user);
            launchVivaldi("http://steamcommunity.com/workshop/");
        }
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_commuity, btn_Store, btn_library, btn_user);
            launchVivaldi("http://steamcommunity.com/market/");
        }
        private void broadcastsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_commuity, btn_Store, btn_library, btn_user);
            launchVivaldi("http://steamcommunity.com/?subsection=broadcasts");
        }
        private void storeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.Visible) this.Show();
            btn_Store_Click(sender, e);
        }
        private void libraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.Visible) this.Show();
            this.WindowState = FormWindowState.Normal;
            picture_game_preview.Width = 494;
            btn_library_Click(sender, e);
        }
        private void communityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.Visible) this.Show();
            btn_commuity_Click(sender, e);
        }
        private void friendsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.Visible) this.Show();
            btn_friends_Click(sender, e);
        }
        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!this.Visible) this.Show();
            settingsToolStripMenuItem_Click(sender, e);
        }
        private void profileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!this.Visible) this.Show();
            btn_user_Click(sender, e);
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!frm_settings.Visible)
            {
                frm_settings = new Settings_Window();
                frm_settings.Show();
            }
            else
            {
                frm_settings.WindowState = FormWindowState.Normal;
                frm_settings.BringToFront();
            }
        }
        private void browserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to sign out?", "Sign Out", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    if (!vivaldi.HasExited) { vivaldi.Kill(); };
                }
                catch { }

                try
                {
                    if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ESS\\Cache\\")))
                    {
                        Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ESS\\Cache\\"));
                    }
                }
                catch
                {

                }
                try
                {
                    DirectoryInfo directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Vivaldi\\User Data\\Default\\Cache");
                    foreach (var file in directory.GetFiles("Login Data*", SearchOption.TopDirectoryOnly))
                    {
                        File.Delete(directory + "\\" + file);
                    }
                }
                catch
                {

                }
                Application.Exit();
            }
        }
        private void activityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_user, btn_Store, btn_library, btn_commuity);
            launchVivaldi("http://steamcommunity.com/my/home/");
        }
        private void friendsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_user, btn_Store, btn_library, btn_commuity);
            launchVivaldi("http://steamcommunity.com/my/friends/");
        }
        private void groupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_user, btn_Store, btn_library, btn_commuity);
            launchVivaldi("http://steamcommunity.com/my/groups/");
        }
        private void contentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_user, btn_Store, btn_library, btn_commuity);
            launchVivaldi("http://steamcommunity.com/my/screenshots/");
        }
        private void badgesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_user, btn_Store, btn_library, btn_commuity);
            launchVivaldi("http://steamcommunity.com/my/badges/");
        }
        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_user, btn_Store, btn_library, btn_commuity);
            launchVivaldi("http://steamcommunity.com/my/inventory/");
        }
        private void vivaldiSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchVivaldi("vivaldi://settings");
        }
        private void exploreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchVivaldi("http://store.steampowered.com/explore/");

        }
        private void curatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchVivaldi("http://store.steampowered.com/curators/");
        }
        private void wishlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchVivaldi("http://steamcommunity.com/my/wishlist");
        }
        private void newsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchVivaldi("http://store.steampowered.com/news");
        }
        private void statsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchVivaldi("http://store.steampowered.com/stats");
        }
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void addToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm_categories.Visible)
            {
                for (int i = 0; i < gameArray.GetLongLength(0); i++)
                {
                    if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                    {
                        Settings.tempName = gameArray[i, 3];
                        frm_categories.BringToFront();
                        return;
                    }
                }
            }

            for (int i = 0; i < gameArray.GetLongLength(0); i++)
            {
                if (list_games.SelectedItem.ToString() == gameArray[i, 0])
                {
                    Settings.tempName = gameArray[i, 3];
                    frm_categories = new Categories();
                    frm_categories.Show();
                    return;
                }
            }
        }
        private void screenshotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_user, btn_Store, btn_library, btn_commuity);
            launchVivaldi("http://steamcommunity.com/my/screenshots/");
        }
        private void playersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_user, btn_Store, btn_library, btn_commuity);
            launchVivaldi("http://steamcommunity.com/my/friends/coplay/");
        }
        private void redeemASteamWalletCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_Store, btn_user, btn_library, btn_commuity);
            launchVivaldi("http://store.steampowered.com/account/redeemwalletcode/");
        }
        private void steamSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_Store, btn_user, btn_library, btn_commuity);
            launchVivaldi("https://help.steampowered.com/en/");
        }
        private void reviewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActiveFromPanel(btn_user, btn_Store, btn_library, btn_commuity);
            launchVivaldi("http://steamcommunity.com/my/recommended");
        }
        private void bookmarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm_bookmarks = new Bookmarks();
            if (!frm_bookmarks.Visible) frm_bookmarks.Show();
        }

        //DropDowmMenu Hovers-----------------------------------------------------------------------------------
        private void btn_Store_MouseEnter(object sender, EventArgs e)
        {
            Control c = sender as Control;
            context_store.Show(c, new Point(0, c.Height));
        }
        private void btn_Store_MouseLeave(object sender, EventArgs e)
        {
            if (!context_store.Bounds.Contains(Cursor.Position))
            {
                context_store.Hide();
            }
        }
        private void btn_commuity_MouseEnter(object sender, EventArgs e)
        {
            Control c = sender as Control;
            context_community.Show(c, new Point(0, c.Height));
        }
        private void btn_commuity_MouseLeave(object sender, EventArgs e)
        {
            if (!context_community.Bounds.Contains(Cursor.Position))
            {
                context_community.Hide();
            }
        }
        private void btn_user_MouseEnter(object sender, EventArgs e)
        {
            Control c = sender as Control;
            context_profile.Show(c, new Point(0, c.Height));
        }
        private void btn_user_MouseLeave(object sender, EventArgs e)
        {
            if (!context_profile.Bounds.Contains(Cursor.Position))
            {
                context_profile.Hide();
            }
        }

        //Tray Icon Controls and Shit---------------------------------------------------------------------------
        private void notifyMe_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void notifyMe_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                context_tray.Show(new Point(Cursor.Position.X, Cursor.Position.Y - context_tray.Height));
            }
            else
            {
                context_tray.Hide();
            }
        }
        private void context_tray_MouseLeave(object sender, EventArgs e)
        {
            context_tray.Hide();
        }

        

        //-Button Image changes based on hover click or normal---------------------------------------------------------------------------
        private void btn_playGame_MouseEnter(object sender, EventArgs e)
        {
            if (!Installed)
            {
                btn_playGame.BackgroundImage = ESSU.Properties.Resources.install_h;
            }
            else 
            {
                btn_playGame.BackgroundImage = ESSU.Properties.Resources.play_h;
            }
        }
        private void btn_playGame_MouseLeave(object sender, EventArgs e)
        {
            if (!Installed)
            {
                btn_playGame.BackgroundImage = ESSU.Properties.Resources.install;
            }
            else
            {
                btn_playGame.BackgroundImage = ESSU.Properties.Resources.play;
            }
        }
        private void btn_playGame_MouseDown(object sender, MouseEventArgs e)
        {
            if (Installed) btn_playGame.BackgroundImage = ESSU.Properties.Resources.play_p;
            else btn_playGame.BackgroundImage = ESSU.Properties.Resources.install_p;
        }
        private void btn_playGame_MouseUp(object sender, MouseEventArgs e)
        {
            if (Installed) btn_playGame.BackgroundImage = ESSU.Properties.Resources.play;
            else btn_playGame.BackgroundImage = ESSU.Properties.Resources.install;
        }
        private void btn_erase_Text_Click(object sender, EventArgs e)
        {
            txt_searchForGame.Text = "";
            btn_erase_Text.Visible = false;
        }
        private void btn_friendsThatPlay_MouseEnter(object sender, EventArgs e)
        {
            btn_friendsThatPlay.BackgroundImage = ESSU.Properties.Resources.friends_h;
        }
        private void btn_friendsThatPlay_MouseLeave(object sender, EventArgs e)
        {
            btn_friendsThatPlay.BackgroundImage = ESSU.Properties.Resources.friends;
        }
        private void btn_friendsThatPlay_MouseDown(object sender, MouseEventArgs e)
        {
            btn_friendsThatPlay.BackgroundImage = ESSU.Properties.Resources.friends_p;
        }
        private void btn_friendsThatPlay_MouseUp(object sender, MouseEventArgs e)
        {
            btn_friendsThatPlay.BackgroundImage = ESSU.Properties.Resources.friends;
        }
        private void btn_news_MouseDown(object sender, MouseEventArgs e)
        {
            btn_news.BackgroundImage = ESSU.Properties.Resources.news_p;
        }
        private void btn_news_MouseEnter(object sender, EventArgs e)
        {
            btn_news.BackgroundImage = ESSU.Properties.Resources.news_h;
        }
        private void btn_news_MouseLeave(object sender, EventArgs e)
        {
            btn_news.BackgroundImage = ESSU.Properties.Resources.news;
        }
        private void btn_news_MouseUp(object sender, MouseEventArgs e)
        {
            btn_news.BackgroundImage = ESSU.Properties.Resources.news;
        }
        private void btn_workshop_MouseDown(object sender, MouseEventArgs e)
        {
            btn_workshop.BackgroundImage = ESSU.Properties.Resources.workshop_p;
        }
        private void btn_workshop_MouseEnter(object sender, EventArgs e)
        {
            btn_workshop.BackgroundImage = ESSU.Properties.Resources.workshop_h;
        }
        private void btn_workshop_MouseLeave(object sender, EventArgs e)
        {
            btn_workshop.BackgroundImage = ESSU.Properties.Resources.workshop;
        }
        private void btn_workshop_MouseUp(object sender, MouseEventArgs e)
        {
            btn_workshop.BackgroundImage = ESSU.Properties.Resources.workshop;
        }
        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            btn_workshop.BackgroundImage = ESSU.Properties.Resources.workshop;
        }
        private void label4_MouseEnter(object sender, EventArgs e)
        {
            btn_workshop.BackgroundImage = ESSU.Properties.Resources.workshop;
        }
        private void label4_MouseLeave(object sender, EventArgs e)
        {
            btn_workshop.BackgroundImage = ESSU.Properties.Resources.workshop;
        }
        private void label4_MouseUp(object sender, MouseEventArgs e)
        {
            btn_workshop.BackgroundImage = ESSU.Properties.Resources.workshop;
        }
        private void btn_achieve_MouseDown(object sender, MouseEventArgs e)
        {
            btn_achieve.BackgroundImage = ESSU.Properties.Resources.achievements_p;
        }
        private void btn_achieve_MouseEnter(object sender, EventArgs e)
        {
            btn_achieve.BackgroundImage = ESSU.Properties.Resources.achievements_h;
        }
        private void btn_achieve_MouseLeave(object sender, EventArgs e)
        {
            btn_achieve.BackgroundImage = ESSU.Properties.Resources.achievements;
        }
        private void btn_achieve_MouseUp(object sender, MouseEventArgs e)
        {
            btn_achieve.BackgroundImage = ESSU.Properties.Resources.achievements;
        }
        private void list_games_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                list_games.SelectedIndex = list_games.IndexFromPoint(e.X, e.Y);
                if (list_games.SelectedIndex == -1) return;
                if (e.Button == MouseButtons.Right && !list_games.SelectedItem.ToString().StartsWith("▼ ") && !list_games.SelectedItem.ToString().StartsWith("▶ "))
                {
                    context_gameMenu.Show(Cursor.Position);
                }
                try
                {
                    if (list_games.SelectedItem.ToString().StartsWith("▼ ") || list_games.SelectedItem.ToString().StartsWith("▶ "))
                    {
                        try
                        {
                            if (list_games.SelectedItem.ToString().StartsWith("▶ "))
                            {
                                list_games.Items[list_games.SelectedIndex] = list_games.SelectedItem.ToString().Replace("▶ ", "▼ ");
                                showCate();
                            }
                            else if (list_games.SelectedItem.ToString().StartsWith("▼ "))
                            {
                                list_games.Items[list_games.SelectedIndex] = list_games.SelectedItem.ToString().Replace("▼ ", "▶ ");
                                showCate();
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }



        }
    }
}
