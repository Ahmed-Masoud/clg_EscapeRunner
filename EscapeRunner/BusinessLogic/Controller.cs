using EscapeRunner.Animations;
using EscapeRunner.BusinessLogic.GameObjects;
using EscapeRunner.View;
using System;
using System.Collections.Generic;

namespace EscapeRunner.BusinessLogic
{
    internal partial class Controller
    {
        public static Player player;
        private static List<IDrawable> drawableObjects = new List<IDrawable>();
        private static ProjectilePool projectilePool;
        private static MainWindow window;

        public Controller()
        {
            window = Program.MainWindow;
            // Subscribe to the notify event
            window.ViewNotification += Window_ViewNotification;
            player = Player.PlayerInstance;

            // Lazy initialization of projectile pool
            projectilePool = ProjectilePool.Instance;
        }

        public static void WindowRefresh(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            MapLoader.drawLevel(e.Graphics);
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

        private void FireBullet()
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

        private void Window_ViewNotification(ViewEventArgs e)
        {
            switch (e.PressedKey)
            {
                case ViewKey.Space:
                    FireBullet();
                    break;

                case ViewKey.Right:
                    player.Move(Directions.Right);
                    break;

                case ViewKey.Left:
                    player.Move(Directions.Left);
                    break;

                case ViewKey.Down:
                    player.Move(Directions.Down);
                    break;

                case ViewKey.Up:
                    player.Move(Directions.Up);
                    break;

                default:
                    break;
            }
        }
    }
}