using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace EscapeRunner
{
    /// <summary>
    /// This class loads the sounds and the animations of game objects
    /// </summary>
    public static class Model
    {
        private static List<Bitmap> bulletClassA;
        private static List<Bitmap> characterAnimation;
        private static List<Bitmap> explosionAnimation;
        private static List<Bitmap> levelMap;
        private static List<Bitmap> flareAnimation;
        /// <summary>
        /// Loads all the animations and puts them in private fields
        /// </summary>
        static Model()
        {
            // Load animations
            LoadAnimations();
        }

        public static List<Bitmap> BulletAnimation { get { return bulletClassA; } }
        public static List<Bitmap> CharacterAnimation { get { return characterAnimation; } }
        // Read properties to return the List<Bitmap> fields
        public static List<Bitmap> ExplosionAnimation { get { return explosionAnimation; } }
        public static List<Bitmap> LevelMap { get { return levelMap; } }
        public static List<Bitmap> FlareAnimation { get { return flareAnimation; } }

        private static string FindProjectPath()
        {
            // Get the project folder on the harddisk
            string path = Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Directory.GetCurrentDirectory())) + "\\";

            if (path != null)
                return path;

            throw new DirectoryNotFoundException();
        }

        private static List<Bitmap> LoadAnimationFromDisk(string animationFolder)
        {
            List<Bitmap> loadedAnimation = new List<Bitmap>(16);
            string[] animationFileNames = Directory.GetFiles(animationFolder, "*.png");

            foreach (string pic in animationFileNames)
            {
                loadedAnimation.Add((Bitmap)Image.FromFile(pic));
            }

            return loadedAnimation;
        }

        private static List<Bitmap> LoadAnimations()
        {
            string projectPath = FindProjectPath();
            string folderPath = Path.Combine(projectPath, "Res");

            // Check for the main resource folder
            if (Directory.Exists(folderPath))
            {
                // Subfolders in animation class
                string charAnimationsFolder = Path.Combine(folderPath, "Char");
                string explosionAnimationsFolder = Path.Combine(folderPath, "Boom");
                string bulletAnimationsFolder = Path.Combine(folderPath, "BulletA");
                string levelTileFolder = Path.Combine(folderPath, "Level");
                string flareFolder = Path.Combine(folderPath, "Flare");

                if (Directory.Exists(charAnimationsFolder)
                    && Directory.Exists(explosionAnimationsFolder)
                    && Directory.Exists(bulletAnimationsFolder)
                    && Directory.Exists(levelTileFolder))
                {
                    characterAnimation = LoadAnimationFromDisk(charAnimationsFolder);
                    explosionAnimation = LoadAnimationFromDisk(explosionAnimationsFolder);
                    bulletClassA = LoadAnimationFromDisk(bulletAnimationsFolder);
                    levelMap = LoadAnimationFromDisk(levelTileFolder);
                    flareAnimation= LoadAnimationFromDisk(flareFolder);
                }
                else
                    throw new InvalidOperationException("Animation Folder cannot be found");
            }
            else
            {
                throw new InvalidOperationException("Animation Folder cannot be found");
            }
            return characterAnimation;
        }
    }
}