using System.Collections.Generic;
using System.IO;

namespace EscapeRunner
{
    enum SoundEffects
    {

    }
    /// <summary>
    /// The sound part from model
    /// </summary>
    public static partial class Model
    {
        private static List<string> soundFiles;
        private static void LoadSounds()
        {
            string soundFolder = Path.Combine(resFolderPath, "Sounds");

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