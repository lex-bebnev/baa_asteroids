using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Asteroids.Entities;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace Asteroids_clone
{
    public sealed class MainWindow: Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, 
            int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Player player; 
        private Timer loop;
        
        public MainWindow()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.ClientSize = new Size(640, 480);
            
            DoubleBuffered = true;
            
            this.loop = new Timer((IContainer) new Container());
            this.loop.Interval = 15;
            
            loop.Tick += this.loop_tick;
            loop.Enabled = true;
            player = new Player();
            
            this.MouseDown += MainWindow_MouseDown;
            this.Paint += MainWindow_Paint;          
            this.KeyDown += MainWindow_OnKeyPress;
        }

        private void MainWindow_OnKeyPress(object sender, KeyEventArgs e)
        {
            
        }

        private void loop_tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
        
        private void MainWindow_MouseDown(object sender, 
            System.Windows.Forms.MouseEventArgs e)
        {     
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            player.Draw(ref e);
        }
        
        
    }
}