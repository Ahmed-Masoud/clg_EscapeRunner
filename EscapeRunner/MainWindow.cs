using EscapeRunner.Animations;
using EscapeRunner.GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public ProjectilePool projectilePool;
        private static List<IDrawable> drawableObjects = new List<IDrawable>();
        private bool superWeapon = false;

        private int tickCounter = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.Shown += MainWindow_Shown;
            this.WindowState = FormWindowState.Maximized;
            this.UpdateBounds();
            player = Player.PlayerInstance;

            //drawableObjects.Add(player);
            Bullet.Dx = 20;
            Bullet.Dy = 20;
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            LowerBound = this.Height;
            RightBound = this.Width;

            // Lazy initialization of projectile pool
            projectilePool = ProjectilePool.Instance;
        }

        // Called on Refresh()
        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            player.UpdateGraphics(e.Graphics);
            if (drawableObjects.Count > 0)
            {
                //temp.UpdateGraphics(e.Graphics);
                for (int i = 0; i < drawableObjects.Count; i++)
                {
                    var temp = drawableObjects[i];
                    if (((IWeapon)temp).Used == true)
                    {
                        temp.UpdateGraphics(e.Graphics);
                    }
                    if (((IWeapon)temp).Used == false)
                    {
                        projectilePool.Dispose((IWeapon)temp);
                        drawableObjects.RemoveAt(i);
                        i--;
                        if (drawableObjects.Count == 0)
                            break;
                    }
                }
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
                // TODO remove dummy try/catch
                try
                {
                    IWeapon projectile = projectilePool.Acquire(Player.Position, false);
                    drawableObjects.Add((ProjectileClassA)projectile);
                }
                catch (InvalidOperationException)
                {
                    // Don't fire
                }
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}