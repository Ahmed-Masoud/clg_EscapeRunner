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
                // Get WAV and mp3 files
                string[] wavFiles = Directory.GetFiles(soundFolder, "*.wav");
                string[] mp3Files = Directory.GetFiles(soundFolder, "*.mp3");
                List<string> files = new List<string>();
                files.AddRange(wavFiles);
                files.AddRange(mp3Files);

                foreach (var soundFile in files)
                {
                    soundFiles.Add(soundFile);
                }
            }
        }
    }
}