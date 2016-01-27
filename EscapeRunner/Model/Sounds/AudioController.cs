using System.Diagnostics;
using WMPLib;

namespace EscapeRunner
{
    /// <summary>
    /// This class handles the audio play during the game
    /// </summary>
    internal sealed class AudioController
    {
        private static WindowsMediaPlayer backgroundPlayer = new WindowsMediaPlayer();
        private static WindowsMediaPlayer laserPlayer = new WindowsMediaPlayer();
        private static WindowsMediaPlayer terroristPlayer = new WindowsMediaPlayer();
        private static WindowsMediaPlayer spaceTrashPlayer = new WindowsMediaPlayer();

        public static void Initialize()
        {
            try
            {
                laserPlayer.settings.autoStart = false;
                spaceTrashPlayer.settings.autoStart = false;
                terroristPlayer.settings.autoStart = false;
                backgroundPlayer.settings.autoStart = false;

                laserPlayer.URL = Model.SoundFiles[0];
                spaceTrashPlayer.URL = Model.SoundFiles[1];
                terroristPlayer.URL = Model.SoundFiles[2];
                backgroundPlayer.URL = Model.SoundFiles[3];
                (backgroundPlayer.settings as IWMPSettings).setMode("loop", true);
            }
            catch (System.Exception)
            {
                Debugger.Break();
            }
        }

        public static void PlayBackgroundSound()
        {
            backgroundPlayer.controls.play();
        }

        public static void StopBackgroundSound()
        {
            backgroundPlayer.controls.stop();
        }

        public static void PlayLaserSound()
        {
            laserPlayer.controls.play();
        }

        public static void PlayTerroristSound()
        {
            terroristPlayer.controls.play();
        }

        public static void PlayMonsterDieSound()
        {
            spaceTrashPlayer.controls.play();
        }
    }
}