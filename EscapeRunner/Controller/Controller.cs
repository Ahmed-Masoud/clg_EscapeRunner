using System.Collections.Generic;
using EscapeRunner.GameObjects;
using EscapeRunner.Animations;
using System;
using EscapeRunner.View;

namespace EscapeRunner
{
    class Controller
    {
        private static MainWindow window;

        public static Player player;
        private static ProjectilePool projectilePool;

        private static List<IDrawable> drawableObjects = new List<IDrawable>();

        public Controller()
        {
            window = Program.MainWindow;
            window.ViewNotification += Window_ViewNotification;
            player = Player.PlayerInstance;
            //IDrawable item = Model.BulletAnimation[0];
            //drawableObjects.Add(new ProjectileClassA(0));
            // Lazy initialization of projectile pool
            projectilePool = ProjectilePool.Instance;
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
                drawableObjects.Add((ProjectileClassA)projectile);

                // TODO play sound
            }
            catch (InvalidOperationException)
            {
                // Out of shots, don't fire
            }
        }
    }
}
