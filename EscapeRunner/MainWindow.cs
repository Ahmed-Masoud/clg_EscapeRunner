using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EscapeRunner
{
    public partial class MainWindow : Form
    {

        public static int UpperBound { get; } = 0;
        public static int LowerBound { get; private set; }
        public static int LeftBound { get; } = 0;
        public static int RightBound { get; private set; }
        public static Player player;

        public MainWindow()
        {
            InitializeComponent();
            this.Shown += MainWindow_Shown;
            this.WindowState = FormWindowState.Maximized;
            this.UpdateBounds();
            player = Player.PlayerInstance;

            Bullet.Dx = 20;
            Bullet.Dy = 20;

            //Hero.motionL.Add(Properties.Resources._1L);
            //Hero.motionL.Add(Properties.Resources._2L);
            //Hero.motionL.Add(Properties.Resources._3L);
            //Hero.motionL.Add(Properties.Resources._4L);
            //Hero.motionL.Add(Properties.Resources._5L);
            //Hero.motionL.Add(Properties.Resources._6L);
            //Hero.motionL.Add(Properties.Resources._7L);
            //Hero.motionL.Add(Properties.Resources._8L);
            //Fart.fartL.Add(Properties.Resources.FL1);
            //Fart.fartL.Add(Properties.Resources.FL2);
            //Fart.fartL.Add(Properties.Resources.FL3);
            //Fart.fartL.Add(Properties.Resources.FL4);
            //Fart.fartL.Add(Properties.Resources.FL5);
            //Fart.fartL.Add(Properties.Resources.FL6);
            //Fart.fartL.Add(Properties.Resources.FL7);
            //Fart.fartL.Add(Properties.Resources.FL8);
            //Fart.fartL.Add(Properties.Resources.FL9);
            //Fart.fartL.Add(Properties.Resources.FL10);
            //Fart.fartL.Add(Properties.Resources.FRL11);
            //Fart.fartR.Add(Properties.Resources.FR1);
            //Fart.fartR.Add(Properties.Resources.FR2);
            //Fart.fartR.Add(Properties.Resources.FR3);
            //Fart.fartR.Add(Properties.Resources.FR4);
            //Fart.fartR.Add(Properties.Resources.FR5);
            //Fart.fartR.Add(Properties.Resources.FR6);
            //Fart.fartR.Add(Properties.Resources.FR7);
            //Fart.fartR.Add(Properties.Resources.FR8);
            //Fart.fartR.Add(Properties.Resources.FR9);
            //Fart.fartR.Add(Properties.Resources.FR10);
            //Fart.fartR.Add(Properties.Resources.FRL11);
            //nowTime = DateTime.Now;
            //gametime = DateTime.Now;
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            LowerBound = this.Height;
            RightBound = this.Width;
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            player.UpdateDrawing(e.Graphics);
            //for (var i = 0; i < bulletObjects.Count; i++)
            //{
            //    if (bulletObjects[i].isShotInAir)
            //    {
            //        bulletObjects[i].Draw(e.Graphics);
            //    }
            //    else bulletObjects.Remove(bulletObjects[i]);
            //}
            //if (fart != null)
            //{
            //    fart.Draw(e.Graphics);
            //}
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down && e.KeyData == Keys.Right)
                player.Move(Directions.DownRight);

            else if (e.KeyData == Keys.Left)
                player.Move(Directions.Left);

            else if (e.KeyData == Keys.Right)
                player.Move(Directions.Right);

            else if (e.KeyData == Keys.Up)
                player.Move(Directions.Up);

            else if (e.KeyData == Keys.Down)
                player.Move(Directions.Down);

            else if (e.KeyData == Keys.Space)
            {
                //if (DateTime.Now.Subtract(nowTime).TotalMilliseconds >= 1000)
                //{
                //    shot = new Bullet();
                //    fart = new Fart();
                //    //shot.Location = Hero.Location;
                //    //shot.BorderRectangle = new Rectangle(Hero.Location.X, Hero.Location.Y, 10, 10);
                //    shot.isShotInAir = true;
                //    fart.shoot();
                //    shot.Move(Hero.direction);
                //    bulletObjects.Add(shot);
                //    nowTime = DateTime.Now;
                //}
            }

            // Finish waiting the proper refresh rate before updating the graphics again
            // Update the graphics only when it's needed
            this.Invoke((MethodInvoker)delegate
            {
                Refresh();
                Thread.Sleep(42);
            });
        }
    }
}
