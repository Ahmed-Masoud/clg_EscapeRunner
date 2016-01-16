using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace EscapeRunner
{
    /// <summary>
    /// This class loads the sounds and the animations of game objects
    /// </summary>
    internal class DataSource
    {

        #region Public Methods

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
            //characterAnimation.Add(Properties.Resources.bunny1_walk1);
            //characterAnimation.Add(Properties.Resources.bunny1_walk2);

            string folderPath = Path.Combine(Environment.CurrentDirectory, "Character Animation");
            //string projectDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (Directory.Exists(folderPath))
            {
                FileInfo aniFile = new FileInfo(folderPath);

                //aniFile.
                string[] animationFileNames = Directory.GetFiles("Character Animation", "*.png");
                //Image i =
                //folderPath;

                foreach (string pic in animationFileNames)
                {
                    
                    characterAnimation.Add((Bitmap)Image.FromFile(pic));
                }
            }
            else
            {
                throw new InvalidOperationException("Animation File cannot be found");
            }
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

        #endregion
    }
}