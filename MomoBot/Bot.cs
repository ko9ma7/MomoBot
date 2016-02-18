using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MomoBot
{
    static class Bot
    {
        [STAThread]
        static void Main()
        {
            Settings.load();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static void Start()
        {

        }

        public static void Stop()
        {

        }
    }
}
