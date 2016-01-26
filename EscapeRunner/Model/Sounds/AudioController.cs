namespace EscapeRunner.Sounds
{
    /// <summary>
    /// This class handles the audio play during the game
    /// </summary>
    internal sealed class AudioController
    {

        static WMPLib.WindowsMediaPlayer backgroundPlayer = new WMPLib.WindowsMediaPlayer();
        static WMPLib.WindowsMediaPlayer laserPlayer = new WMPLib.WindowsMediaPlayer();
        static WMPLib.WindowsMediaPlayer terroristPlayer = new WMPLib.WindowsMediaPlayer();
        static WMPLib.WindowsMediaPlayer torpedoPlayer = new WMPLib.WindowsMediaPlayer();
        static WMPLib.WindowsMediaPlayer spaceTrashPlayer = new WMPLib.WindowsMediaPlayer();
        
        public static void playBackgroundSound()
        {
            backgroundPlayer.URL = Model.SoundFiles[4];

            backgroundPlayer.controls.play();
        }

        public static void stopBackgroundSound()
        {
            backgroundPlayer.controls.stop();
        }

        public static void playLaserSound()
        {
            laserPlayer.URL = Model.SoundFiles[0];

            laserPlayer.controls.play();
        }

        public static void stopLaserSound()
        {
            laserPlayer.controls.stop();
        }

        public static void playTerroristSound()
        {
            terroristPlayer.URL = Model.SoundFiles[3];

            terroristPlayer.controls.play();
        }

        public static void stopTerrorsitSound()
        {
            terroristPlayer.controls.stop();
        }

        public static void playTorpedoSound()
        {
            torpedoPlayer.URL = Model.SoundFiles[5];

            torpedoPlayer.controls.play();
        }

        public static void stopTorpedoSound()
        {
            torpedoPlayer.controls.stop();
        }

        public static void playSpaceTrashSound()
        {
            spaceTrashPlayer.URL = Model.SoundFiles[1];

            spaceTrashPlayer.controls.play();
        }

        public static void stopSpaceTrashSound()
        {
            spaceTrashPlayer.controls.stop();
        }
    }
}