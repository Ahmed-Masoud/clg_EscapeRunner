using System.Collections.Generic;
using System.IO;

namespace EscapeRunner
{
    /// <summary>
    /// The sound part from model
    /// </summary>
    public static partial class Model
    {
        private static List<string> soundFiles;

        private static void LoadSounds()
        {
            if (soundFiles != null)
                return;

            string soundFolder = Path.Combine(resFolderPath, "Sounds");
            soundFiles = new List<string>();
            if (Directory.Exists(soundFolder))
            {
                string[] folders = Directory.GetFiles(soundFolder, "*.wav");
                foreach (var soundFile in folders)
                {
                    soundFiles.Add(soundFile);
                }
            }
        }
    }
}