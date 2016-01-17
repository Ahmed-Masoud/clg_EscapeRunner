using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace EscapeRunner
{
    /// <summary>
    /// This class loads the sounds and the animations of game objects
    /// </summary>
    public static class DataSource
    {
        private static List<Bitmap> bulletClassA;
        private static List<Bitmap> characterAnimation;
        private static List<Bitmap> explosionAnimation;

        public static List<Bitmap> ExplosionAnimation { get { return explosionAnimation; } }
        public static List<Bitmap> BulletAnimation { get { return bulletClassA; } }
        public static List<Bitmap> CharacterAnimation { get { return characterAnimation; } }
        /// <summary>
        /// Loads all the animations and puts them in private fields
        /// </summary>
        static DataSource()
        {
            // Load animations
            LoadAnimations();
        }

        private static List<Bitmap> LoadAnimations()
        {
            string projectPath = FindProjectPath();
            string folderPath = Path.Combine(projectPath, "Res");

            // Check for the main resource folder
            if (Directory.Exists(folderPath))
            {
                // Character animation
                string charAnimationsFolder = Path.Combine(folderPath, "Char");
                string explosionAnimationsFolder = Path.Combine(folderPath, "Boom");
                string bulletAnimationsFolder = Path.Combine(folderPath, "BulletA");

                if (Directory.Exists(charAnimationsFolder)
                    && Directory.Exists(explosionAnimationsFolder)
                    && Directory.Exists(bulletAnimationsFolder))
                {
                    characterAnimation = LoadAnimationFromDisk(charAnimationsFolder);
                    explosionAnimation = LoadAnimationFromDisk(explosionAnimationsFolder);
                    bulletClassA = LoadAnimationFromDisk(bulletAnimationsFolder);

                }
                else
                    throw new InvalidOperationException("Animation File cannot be found");
            }
            else
            {
                throw new InvalidOperationException("Animation File cannot be found");
            }
            return characterAnimation;
        }

        private static string FindProjectPath()
        {
            // Get the project folder on the harddisk
            string binFolder = Environment.CurrentDirectory;
            string[] parentDirectories = binFolder.Split('\\');

            if (parentDirectories.Length >= 2)
            {
                // If the project is not directly on the hard-disk C: ProjectFile \bin
                StringBuilder projectDirectory = new StringBuilder(16);

                // Get the path of the project
                for (int i = 0; i < parentDirectories.Length - 2; i++)
                {
                    projectDirectory.Append($"{parentDirectories[i]}\\");
                }
                return projectDirectory.ToString();
            }
            else
            {
                throw new InvalidOperationException("Project Folder Can't be found");
            }
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
    }
}