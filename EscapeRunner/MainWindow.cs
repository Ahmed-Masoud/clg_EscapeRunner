using EscapeRunner.Animations;
using EscapeRunner.GameObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        private static List<IDrawable> drawableObjects = new List<IDrawable>();
        private bool superWeapon;

        public MainWindow()
        {
            InitializeComponent();
            this.Shown += MainWindow_Shown;
            this.WindowState = FormWindowState.Maximized;
            this.UpdateBounds();
            player = Player.PlayerInstance;
            drawableObjects.Add(player);
            Bullet.Dx = 20;
            Bullet.Dy = 20;
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            LowerBound = this.Height;
            RightBound = this.Width;
        }

        // Called on Refresh()
        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {

            foreach (IDrawable item in drawableObjects)
            {
                if (item != null)
                    Task.Run(() => item.UpdateGraphics(e.Graphics));
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left)
                player.Move(Directions.Left);
            else if (e.KeyData == Keys.Right)
                player.Move(Directions.Right);
            else if (e.KeyData == Keys.Up)
                player.Move(Directions.Up);
            else if (e.KeyData == Keys.Down)
                player.Move(Directions.Down);
            else if (e.KeyData == Keys.Space)
            {
                // Create a new explosion and add it to the drawable list
                if (!superWeapon)
                    ProjectileClassA proj = new ProjectileClassA(player.Position);
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