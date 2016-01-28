﻿using System.Diagnostics;
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
        private static WindowsMediaPlayer explosionPlayer = new WindowsMediaPlayer();
        private static WindowsMediaPlayer bombExplosionPlayer = new WindowsMediaPlayer();
        private static WindowsMediaPlayer smallPowerUp = new WindowsMediaPlayer();
        private static WindowsMediaPlayer bigPowerUp = new WindowsMediaPlayer();
        private static IWMPPlaylist playlist = explosionPlayer.playlistCollection.newPlaylist("myplaylist");

        public static void Initialize()
        {
            try
            {
                laserPlayer.settings.autoStart = false;
                spaceTrashPlayer.settings.autoStart = false;
                terroristPlayer.settings.autoStart = false;
                backgroundPlayer.settings.autoStart = false;
                backgroundPlayer.settings.volume = 20;
                explosionPlayer.settings.autoStart = false;
                bombExplosionPlayer.settings.autoStart = false;
                smallPowerUp.settings.autoStart = false;
                bigPowerUp.settings.autoStart = false;

                laserPlayer.URL = Model.SoundFiles[0];
                spaceTrashPlayer.URL = Model.SoundFiles[1];
                terroristPlayer.URL = Model.SoundFiles[2];
                backgroundPlayer.URL = Model.SoundFiles[3];
                //explosionPlayer.URL = Model.SoundFiles[4];
                bombExplosionPlayer.URL = Model.SoundFiles[4];
                bigPowerUp.URL = Model.SoundFiles[5];
                smallPowerUp.URL = Model.SoundFiles[6];

                playlist.appendItem(explosionPlayer.newMedia(Model.SoundFiles[2]));
                playlist.appendItem(explosionPlayer.newMedia(Model.SoundFiles[4]));
                explosionPlayer.currentPlaylist = playlist;
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

        public static void PlayBombExplosion()
        {
            bombExplosionPlayer.controls.play();
        }

        public static void PlayLaserSound()
        {
            laserPlayer.controls.play();
        }
        public static void PlayExplosion()
        {
            explosionPlayer.controls.play();
        }
        public static void PlayTerroristSound()
        {
            terroristPlayer.controls.play();
        }

        public static void PlayMonsterDieSound()
        {
            spaceTrashPlayer.controls.play();
        }
        public static void PlaySmallPowerUp()
        {
            smallPowerUp.controls.play();
        }
        public static void PlayBigPowerUp()
        {
            bigPowerUp.controls.play();
        }
    }
}