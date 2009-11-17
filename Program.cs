using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IdeBridge
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainPanel = new MainPanel();

            new SharpDevBackend();

            Application.Run(mainPanel);
        }
    }
}
