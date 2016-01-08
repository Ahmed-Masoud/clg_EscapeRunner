using EscapeRunner.Animations;
using EscapeRunner.GameObjects;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EscapeRunner
{
    public partial class MainWindow : Form
    {
        #region Public Fields

        public static Player player;
        public ProjectilePool projectilePool;

        #endregion

        #region Private Fields

        private static List<IDrawable> drawableObjects = new List<IDrawable>();
        private Timer refreshTimer = new Timer();

        #endregion

        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();
            this.Shown += MainWindow_Shown;
            this.WindowState = FormWindowState.Maximized;
            this.UpdateBounds();
            player = Player.PlayerInstance;

            refreshTimer.Enabled = true;
            refreshTimer.Interval = 20;
            refreshTimer.Tick += new EventHandler(this.refreshTimer_Tick);

            //drawableObjects.Add(player);
        }

        #endregion

        #region Public Properties

        public static int LeftBound { get; } = 0;
        public static int LowerBound { get; private set; }
        public static int RightBound { get; private set; }
        public static int UpperBound { get; } = 0;

        #endregion

        #region Private Methods

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

            if (e.KeyData == Keys.Space)
                try
                {
                    // Create a new explosion and add it to the drawable list
                    IWeapon projectile = projectilePool.Acquire(Player.Position, false);
                    drawableObjects.Add((ProjectileClassA)projectile);

                    // TODO play sound
                }
                catch (InvalidOperationException)
                {
                    // Out of shots, don't fire
                }
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

                    // Delete the shot directly if it finished animation
                    if (((IWeapon)temp).Used == false)
                    {
                        // Release bullet resources
                        drawableObjects.RemoveAt(i);
                        i--;
                        if (drawableObjects.Count == 0)
                            break;
                    }
                }
            }
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            LowerBound = this.Height;
            RightBound = this.Width;

            // Lazy initialization of projectile pool
            projectilePool = ProjectilePool.Instance;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        #endregion
    }
}