using EscapeRunner.BusinessLogic;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

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
        System.Media.SoundPlayer player;

        public SplashScreen()
        {

            loadFromFile();

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

            //this.ClientSize = this.BackgroundImage.Size;
            splashScreenThread = new Thread(ShowForm);
            splashScreenThread.IsBackground = true;
            splashScreenThread.Start();

            window = new MainWindow();
            window.Opacity = 0.01;
            window.Show();
            //window.Hide();
            window.Opacity = 0;
            Program.MainWindow = window;
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

        static public async void ShowForm()
        {
            await Model.InitializeModelAsync();
            Controller.InitializeController();

#if !DEBUG
            await Task.Run(() => Thread.Sleep(2500));
#endif
            doneLoading = true;
        }
        private void LoadResources()
        {
            string resPath = Path.GetDirectoryName(
                          Path.GetDirectoryName(
                              Directory.GetCurrentDirectory())) + "\\Res";

            string imagePath = resPath + "\\SplashScreen.jpg";
            string soundPath = resPath + "\\Terrorist.wav";

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

        private void loadFromFile()
        {
            string path = Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Directory.GetCurrentDirectory())) + "\\";
            path = Path.Combine(path, Path.Combine("Res", "Levels"));
            string[] levelFiles = Directory.GetFiles(path, "*.game");

            //Don't open file dialog every time the application is launched
            if (levelFiles.Length == 1)
            {
                // A single level exists, load it
                ReadLevelFile(levelFiles[0]);
            }
            else
            {
                OpenFileDialog openLevelDialog = new OpenFileDialog();
                openLevelDialog.Title = "Open Level";
                openLevelDialog.Filter = "GAME Files (*.game) | *.game";
                openLevelDialog.DefaultExt = "game";
                openLevelDialog.InitialDirectory = path;

                DialogResult openGame = openLevelDialog.ShowDialog();
                if (openGame == DialogResult.OK)
                {
                    string fileName = openLevelDialog.FileName;
                    ReadLevelFile(fileName);
                }
            }
        }

        private void ReadLevelFile(string folderPath)
        {
            int rows = -1;
            int cols = -1;
            int[][] level;
            Point[] playerPositions;
            int monstersCount = -1;
            EscapeRunner.View.SplashScreen.Monster[] monsters;

            StreamReader reader = new StreamReader(folderPath);

            // read player position
            string line = reader.ReadLine();

            if (line != null)
            {
                int[] points = Array.ConvertAll(line.Substring(24, (line.Length - 26)).Split(','), s => int.Parse(s));

                playerPositions = new Point[2];
                playerPositions[0] = new Point(points[0], points[1]);
                playerPositions[1] = new Point(points[2], points[3]);
            }
            line = reader.ReadLine();

            // read monsters count
            line = reader.ReadLine();

            if (line != null)
            {
                monstersCount = Convert.ToInt32(line.Substring(20, (line.Length - 21)));
            }
            line = reader.ReadLine();

            // read monsters locations
            if (monstersCount > 0)
            {
                line = reader.ReadLine();

                monsters = new EscapeRunner.View.SplashScreen.Monster[monstersCount];
                int counter = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    if (counter < monstersCount)
                    {
                        line = line.Substring(2, (line.Length - 3));

                        if (counter != (monstersCount - 1))
                            line = line.Substring(0, (line.Length - 2));

                        int[] points = Array.ConvertAll(line.Split(','), s => int.Parse(s));

                        Point start = new Point(points[0], points[1]);
                        Point end = new Point(points[2], points[3]);
                        monsters[counter] = new Monster(start, end);
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            line = reader.ReadLine();
            
            // read map dimensions
            line = reader.ReadLine();

            if (line != null)
            {
                cols = Convert.ToInt32(line.Substring(10, (line.Length - 11)));
            }

            line = reader.ReadLine();

            if (line != null)
            {
                rows = Convert.ToInt32(line.Substring(10, (line.Length - 11)));
            }

            // read level
            if (rows > 0 && cols > 0)
            {
                line = reader.ReadLine();
                line = reader.ReadLine();

                level = new int[rows][];
                int counter = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    if (counter < rows)
                    {
                        line = line.Substring(2, (line.Length - 3));

                        if (counter != (rows - 1))
                            line = line.Substring(0, (line.Length - 2));

                        level[counter++] = Array.ConvertAll(line.Split(','), s => int.Parse(s));
                    }
                    else
                    {
                        break;
                    }
                }

            }
        }

        public struct Monster
        {
            Point startPoint;
            Point endPoint;

            public Point StartPoint
            {
                get { return startPoint; }
                set { startPoint = value; }
            }

            public Point EndPoint
            {
                get { return endPoint; }
                set { endPoint = value; }
            }

            public Monster(Point p1, Point p2)
            {
                this.startPoint = p1;
                this.endPoint = p2;
            }
        }
    }
}