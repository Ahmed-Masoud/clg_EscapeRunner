using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EscapeRunner.View
{
    public sealed partial class SplashScreen : Form
    {
        private Thread splashScreenThread;
        private MainWindow window;

        private static double opcaityDecrement = 0.05;
        private static double opacityIncrement = 0.08;
        private static int timerInterval = 50;
        private static System.Windows.Forms.Timer opacityTimer = new System.Windows.Forms.Timer();
        private static bool doneLoading = false;
        private System.Media.SoundPlayer player;

        public SplashScreen()
        {
            this.Opacity = 0;
            InitializeComponent();
            opacityTimer.Interval = timerInterval;
            opacityTimer.Tick += OpacityTimer_Tick;
            opacityTimer.Enabled = true;
            LoadResources();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
#if !DEBUG
            LoadResources();
            PlaySound();
#endif
            //this.Opacity = 0.5;
            window = new MainWindow();
            window.Opacity = 0.01;
            window.Show();
            //window.Hide();
            window.Opacity = 0;
            Program.MainWindow = window;

            //this.ClientSize = this.BackgroundImage.Size;
            splashScreenThread = new Thread(InitializeGameWindow);
            splashScreenThread.IsBackground = true;
            splashScreenThread.Start();

            Application.Run(this);
        }

        private void OpacityTimer_Tick(object sender, EventArgs e)
        {
            // Positive increment value
            if (opacityIncrement > 0)
            {
                // Fade in
                if (this.Opacity < 1)
                    this.Opacity += opacityIncrement;
            }
            // Negative increment value
            else if (opacityIncrement < 0)
            {
                // Fade out
                if (this.Opacity > 0)
                    this.Opacity += opacityIncrement;
                else
                {
                    window.Opacity = 1;
                    this.Hide();
                    window.Show();

                    // Stop the ticking timer
                    opacityTimer.Enabled = false;
                }
            }
            if (doneLoading)
            {
                CloseForm();
                doneLoading = false;
            }
        }

        private void CloseForm()
        {
            if (this != null)
            {
                // Set the increment to a negative value
                opacityIncrement = -opcaityDecrement;
            }
        }

        public async void InitializeGameWindow()
        {
#if !DEBUG
            // Make the splash screen wait
            await Task.Run(() => Thread.Sleep(2500));
#endif
            await window.InitializeStaticClasses();
            doneLoading = true;
        }

        private void LoadResources()
        {
            string resPath = Path.GetDirectoryName(
                          Path.GetDirectoryName(
                              Directory.GetCurrentDirectory())) + "\\Res";

            string imagePath = resPath + "\\SplashScreen.jpg";
            string soundPath = resPath + "\\Sounds\\03_Terrorist.wav";

            if (File.Exists(imagePath) && File.Exists(soundPath))
            {
                this.BackgroundImage = Image.FromFile(imagePath);
                player = new System.Media.SoundPlayer(soundPath);
            }
            else
                throw new FileNotFoundException();
        }

        private void PlaySound()
        {
            if (player != null)
                player.Play();
        }
    }
}