using EscapeRunner.Animations;
using EscapeRunner.BusinessLogic.GameObjects;
using EscapeRunner.View;
using System;
using System.Collections.Generic;
using System.Drawing;

using System.Threading.Tasks;
using System.Timers;

namespace EscapeRunner.BusinessLogic
{
    public partial class Controller
    {
        private delegate void DrawingOperations(Graphics g);
        private static DrawingOperations drawGraphics;

        private static Player player;
        private static List<IDrawable> drawableObjects = new List<IDrawable>();
        private static ProjectilePool projectilePool;
        private static MainWindow window;

        static Timer backgroundIllusionTimer = new Timer();
        private Controller() { }

        public static void InitializeController()
        {
            window = Program.MainWindow;
            // Subscribe to the notify event
            window.ViewNotification += Window_ViewNotification;
            player = Player.PlayerInstance;
            // Lazy initialization of projectile pool
            projectilePool = ProjectilePool.Instance;
            Monster mon = new Monster();

            drawableObjects.Add(mon);
            backgroundIllusionTimer.Interval = 100;
            backgroundIllusionTimer.Elapsed += BackgroundIllusionTimer_Elapsed;
            backgroundIllusionTimer.Enabled = true;

#if !DEBUG
            drawGraphics += MapLoader.DrawGameFlares;
#endif
            drawGraphics += MapLoader.DrawLevelFloor;
            drawGraphics += MapLoader.DrawLevelObstacles;
            drawGraphics += player.UpdateGraphics;
            drawGraphics += UpdateTiles;
            drawGraphics += DrawShots;

        }

        private static void BackgroundIllusionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Next();
        }

        private static void FireBullet()
        {
            try
            {
                // Create a new explosion and add it to the drawable list
                IWeapon projectile = projectilePool.Acquire(Player.Position, false);
                drawableObjects.Add((ProjectileClassAlpha)projectile);

                // TODO play sound
            }
            catch (InvalidOperationException)
            {
                // Out of shots, don't fire
            }
        }

        private static void Window_ViewNotification(ViewNotificationEventArgs e)
        {
            switch (e.Notification)
            {
                case Notifing.Space:
                    FireBullet();
                    break;

                case Notifing.Right:
                    player.StartMoving(Directions.Right);
                    break;

                case Notifing.Left:
                    player.StartMoving(Directions.Left);
                    break;

                case Notifing.Down:
                    player.StartMoving(Directions.Down);
                    break;

                case Notifing.Up:
                    player.StartMoving(Directions.Up);
                    break;

                default:
                    break;
            }
        }
    }
}