using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

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
            soundFiles = new List<string>();

        }
        public static async Task InitializeModelAsync()
        {
            await LoadAnimations();
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

        private static Func<string, Bitmap> bitmapRead = ((string file) => ((Bitmap)Image.FromFile(file)));


        private static async Task LoadAnimations()
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
                string monsterFolder = Path.Combine(resFolderPath, "Monster");

                if (Directory.Exists(charAnimationsFolder)
                    && Directory.Exists(explosionAnimationsFolder)
                    && Directory.Exists(bulletAnimationsFolder)
                    && Directory.Exists(levelTileFolder)
                    && Directory.Exists(backgroundFolder)
                    && Directory.Exists(monsterFolder))
                {
                    flareAnimation = await LoadResourceFromDisk(bitmapRead, flareFolder, "*.png");
                    characterAnimation = await LoadResourceFromDisk(bitmapRead, charAnimationsFolder, "*.png");
                    explosionAnimation = await LoadResourceFromDisk(bitmapRead, explosionAnimationsFolder, "*.png");
                    bulletClassA = await LoadResourceFromDisk(bitmapRead, bulletAnimationsFolder, "*.png");
                    tileTextures = await LoadResourceFromDisk(bitmapRead, levelTileFolder, "*.png");

                    monsterAnimation = await LoadResourceFromDisk(bitmapRead, monsterFolder, "*.png");
                    backgrounds = await LoadResourceFromDisk(bitmapRead, backgroundFolder, "*.png");

                }
                else
                    throw new InvalidOperationException("Animation Folder cannot be found");
            }
            else
            {
                throw new InvalidOperationException("Animation Folder cannot be found");
            }

        }

        /// <summary>
        /// General resource reader
        /// </summary>
        /// <typeparam name="T">List element type</typeparam>
        /// <param name="reader">Function that reads the file</param>
        /// <param name="resourceFolder"></param>
        /// <param name="extension">Extension for target file</param>
        /// <returns></returns>
        private static async Task<List<T>> LoadResourceFromDisk<T>(Func<string, T> reader, string resourceFolder, string extension)
        {
            List<T> loadedAnimation = new List<T>(16);
            string[] resourceFileNames = Directory.GetFiles(resourceFolder, extension);
            await Task.Run(() =>
            {
                foreach (string file in resourceFileNames)
                {
                    loadedAnimation.Add(reader(file));
                }
            });

            return loadedAnimation;
        }
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

    }
}