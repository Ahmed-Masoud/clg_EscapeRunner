using System;
using System.Windows.Forms;

namespace EscapeRunner.View
{
    internal static class Program
    {
        private static MainWindow mainWindow;
        public static MainWindow MainWindow { get { return mainWindow; } set { mainWindow = value; } }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new SplashScreen();
        }
    }
}