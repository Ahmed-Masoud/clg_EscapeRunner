using System.Diagnostics;
using System.IO;

namespace EscapeRunner.Sounds
{
    /// <summary>
    /// This class handles the audio play during the game
    /// </summary>
    internal sealed class AudioController
    {
        private static WMPLib.WindowsMediaPlayer backgroundPlayer = new WMPLib.WindowsMediaPlayer();
        private static WMPLib.WindowsMediaPlayer laserPlayer = new WMPLib.WindowsMediaPlayer();
        private static WMPLib.WindowsMediaPlayer terroristPlayer = new WMPLib.WindowsMediaPlayer();
        private static WMPLib.WindowsMediaPlayer torpedoPlayer = new WMPLib.WindowsMediaPlayer();
        private static WMPLib.WindowsMediaPlayer monsterDiePlayer = new WMPLib.WindowsMediaPlayer();

        public static void Initialize()
        {
            try
            {
                laserPlayer.URL = Model.SoundFiles[0];
                monsterDiePlayer.URL = Model.SoundFiles[1];
                backgroundPlayer.URL = Model.SoundFiles[2];
                terroristPlayer.URL = Model.SoundFiles[3];
                backgroundPlayer.EndOfStream += BackgroundPlayer_EndOfStream;
                (backgroundPlayer.settings as WMPLib.IWMPSettings).setMode("loop", true);
            }
            catch (FileNotFoundException)
            {
                Debugger.Break();
            }
        }

        public static void PlayBackgroundSound()
        {
            backgroundPlayer.controls.play();
        }

        private static void BackgroundPlayer_EndOfStream(int Result)
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

        public static void StopLaserSound()
        {
            laserPlayer.controls.stop();
        }

        public static void PlayTerroristSound()
        {
            terroristPlayer.controls.play();
        }

        public static void StopTerrorsitSound()
        {
            terroristPlayer.controls.stop();
        }

        public static void PlayMonsterDieSound()
        {
            monsterDiePlayer.controls.play();
        }

        public static void StopMonsterDieSound()
        {
            monsterDiePlayer.controls.stop();
        }
    }
}