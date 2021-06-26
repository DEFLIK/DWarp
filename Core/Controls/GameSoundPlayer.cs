using System;
using System.IO;
using System.Media;
using System.Windows.Media;

namespace DWarp.Core.Controls
{
    public static class GameSoundPlayer
    {
        private static MediaPlayer mediaPlayer = new MediaPlayer();
        private static SoundPlayer loopSoundPlayer = new SoundPlayer();
        
        public static void PlayAsync(string soundName)
        {
            mediaPlayer.Dispatcher.Invoke(() =>
            {
                //mediaPlayer.Open(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}\\Resources\\Audio\\{soundName}.wav")); // Build settings
                mediaPlayer.Open(new Uri($"{Path.GetFullPath(@"..\..\")}\\Resources\\Audio\\{soundName}.wav")); // Project settings
                mediaPlayer.Play();
            });
        }

        public static void PlayAmbient(Stream stream)
        {
            loopSoundPlayer.Stream = stream;
            loopSoundPlayer.PlayLooping();
        }

        public static void StopAmbient() => loopSoundPlayer.Stop();

        public static void Dispose()
        {
            loopSoundPlayer.Stop();
            if(loopSoundPlayer.Stream != null)
                loopSoundPlayer.Stream.Position = 0;
            //loopSoundPlayer.Dispose();
        }
    }
}
