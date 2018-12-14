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
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run((Form) new MainWindow());
        }                
    }
}