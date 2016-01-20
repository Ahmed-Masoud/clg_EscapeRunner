using EscapeRunner.Animations;
using EscapeRunner.BusinessLogic.GameObjects;
using EscapeRunner.View;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic
{
    internal partial class Controller
    {
        // TODO change this to private
        public static Player player;
        private static List<IDrawable> drawableObjects = new List<IDrawable>();
        private static ProjectilePool projectilePool;
        private static MainWindow window;

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
        }

        public static void WindowRefresh(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            DrawGraphics(e.Graphics);
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