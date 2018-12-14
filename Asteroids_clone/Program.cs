using System;
using System.Windows.Forms;

namespace Asteroids_clone
{

    public class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.DoEvents();
            Application.Run(new MainWindow());
        }                
    }
}