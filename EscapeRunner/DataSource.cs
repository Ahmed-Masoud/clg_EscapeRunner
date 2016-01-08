using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner
{
    /// <summary>
    /// This class loads the sounds and the animations of game objects
    /// </summary>
    internal class DataSource
    {
        public static List<Bitmap> LoadCharacterAnimationFromDisk()
        {
            List<Bitmap> characterAnimation = new List<Bitmap>();
            // Animation images are included in the base class as a protected List<Bitmap>
            //characterAnimation.Add(Properties.Resources.wingMan1);
            //characterAnimation.Add(Properties.Resources.wingMan2);
            //characterAnimation.Add(Properties.Resources.wingMan3);
            //characterAnimation.Add(Properties.Resources.wingMan4);
            //characterAnimation.Add(Properties.Resources.wingMan5);
            //characterAnimation.Add(Properties.Resources.wingMan6);
            //characterAnimation.Add(Properties.Resources.wingMan7);
            //characterAnimation.Add(Properties.Resources.wingMan8);
            //characterAnimation.Add(Properties.Resources.wingMan9);
            characterAnimation.Add(Properties.Resources.bunny1_walk1);
            characterAnimation.Add(Properties.Resources.bunny1_walk2);
            return characterAnimation;
        }

        public static List<Bitmap> LoadExplosionAnimationFromDisk()
        {
            List<Bitmap> explosionAnimation = new List<Bitmap>();
            // Animation images are included in the base class as a protected List<Bitmap>
            explosionAnimation.Add(Properties.Resources.Boom1);
            explosionAnimation.Add(Properties.Resources.Boom2);
            explosionAnimation.Add(Properties.Resources.Boom3);
            explosionAnimation.Add(Properties.Resources.Boom4);
            explosionAnimation.Add(Properties.Resources.Boom4);
            explosionAnimation.Add(Properties.Resources.Boom5);
            explosionAnimation.Add(Properties.Resources.Boom6);
            explosionAnimation.Add(Properties.Resources.Boom7);
            explosionAnimation.Add(Properties.Resources.Boom8);
            explosionAnimation.Add(Properties.Resources.Boom9);
            explosionAnimation.Add(Properties.Resources.Boom10);

            return explosionAnimation;
        }

        public static List<Bitmap> LoadBulletClassA()
        {
            List<Bitmap> bullets = new List<Bitmap>();
            bullets.Add(Properties.Resources.Bullet0);
            bullets.Add(Properties.Resources.Bullet01);
            bullets.Add(Properties.Resources.Bullet02);
            bullets.Add(Properties.Resources.Bullet03);
            bullets.Add(Properties.Resources.Bullet04);

            return bullets;
        }
    }
}