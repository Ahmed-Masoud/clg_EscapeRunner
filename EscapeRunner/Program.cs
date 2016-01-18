using EscapeRunner.BusinessLogic;
using System;
using System.Windows.Forms;

namespace EscapeRunner
{
    internal static class Program
    {
        public static MainWindow MainWindow { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow = new MainWindow();

            // ?
            Controller d = new Controller();

            Application.Run(MainWindow);
        }
    }
}