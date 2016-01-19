using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace EscapeRunner
{
    /// <summary>
    /// This class loads the animations of game objects
    /// </summary>
    public static partial class Model
    {
        private static List<Bitmap> backgrounds;

        private static List<Bitmap> bulletClassA;

        private static List<Bitmap> characterAnimation;

        private static List<Bitmap> explosionAnimation;

        private static List<Bitmap> flareAnimation;

        private static List<Bitmap> monsterAnimation;

        private static string resFolderPath;

        private static List<Bitmap> tileTextures;

        /// <summary>
        /// Loads all the animations and puts them in private fields
        /// </summary>
        static Model()
        {
            string projectPath = FindProjectPath();
            resFolderPath = Path.Combine(projectPath, "Res");

            LoadAnimations();
            LoadSounds();
        }

        public static List<Bitmap> Backgrounds { get { return backgrounds; } }
        public static List<Bitmap> BulletAnimation { get { return bulletClassA; } }
        public static List<Bitmap> CharacterAnimation { get { return characterAnimation; } }

        // Read properties to return the List<Bitmap> fields
        public static List<Bitmap> ExplosionAnimation { get { return explosionAnimation; } }

        public static List<Bitmap> FlareAnimation { get { return flareAnimation; } }
        public static List<Bitmap> MonsterAnimation { get { return monsterAnimation; } }
        public static string ResFolder { get { return resFolderPath; } }
        public static List<Bitmap> TileTextures { get { return tileTextures; } }

        private static string FindProjectPath()
        {
            // Get the project folder on the hard disk
            string path = Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Directory.GetCurrentDirectory())) + "\\";

            if (path != null)
                return path;

            throw new DirectoryNotFoundException();
        }

        private static List<Bitmap> LoadAnimations()
        {
            // Check for the main resource folder
            if (Directory.Exists(resFolderPath))
            {
                // Sub folders in animation class
                string charAnimationsFolder = Path.Combine(resFolderPath, "Char");
                string explosionAnimationsFolder = Path.Combine(resFolderPath, "Boom");
                string bulletAnimationsFolder = Path.Combine(resFolderPath, "BulletA");
                string levelTileFolder = Path.Combine(resFolderPath, "Tiles");
                string flareFolder = Path.Combine(resFolderPath, "Flare");
                string backgroundFolder = Path.Combine(resFolderPath, "Background");
                String monsterFolder = Path.Combine(resFolderPath, "Monster");

                if (Directory.Exists(charAnimationsFolder)
                    && Directory.Exists(explosionAnimationsFolder)
                    && Directory.Exists(bulletAnimationsFolder)
                    && Directory.Exists(levelTileFolder)
                    && Directory.Exists(backgroundFolder)
                    && Directory.Exists(monsterFolder))
                {
                    characterAnimation = LoadAnimationFromDisk(charAnimationsFolder);
                    explosionAnimation = LoadAnimationFromDisk(explosionAnimationsFolder);
                    bulletClassA = LoadAnimationFromDisk(bulletAnimationsFolder);
                    tileTextures = LoadAnimationFromDisk(levelTileFolder);
                    flareAnimation = LoadAnimationFromDisk(flareFolder);
                    backgrounds = LoadAnimationFromDisk(backgroundFolder);
                    monsterAnimation = LoadAnimationFromDisk(monsterFolder);

                    //backgrounds = LoadAnimationFromDisk((() => (Bitmap)Image.FromFile(backgroundFolder)), backgroundFolder, "*.png");
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
        private static List<T> LoadAnimationFromDisk<T>(Func<T> reader, string animationFolder, string extension)
        {
            List<T> loadedAnimation = new List<T>(16);
            string[] animationFileNames = Directory.GetFiles(animationFolder, extension);

            foreach (string pic in animationFileNames)
            {
                loadedAnimation.Add(reader.Invoke());
            }

            return loadedAnimation;
        }
    }
}