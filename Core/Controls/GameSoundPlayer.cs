using System;
using System.IO;
using System.Media;
using System.Windows.Media;

namespace DWarp.Core.Controls
{
    public class GameSoundPlayer
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private SoundPlayer loopSoundPlayer = new SoundPlayer();
        
        public void PlayAsync(string soundName)
        {
            //mediaPlayer.Open(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}\\Resources\\Audio\\{soundName}.wav")); // Build settings
            mediaPlayer.Open(new Uri($"{Path.GetFullPath(@"..\..\")}\\Resources\\Audio\\{soundName}.wav")); //Project settings
            mediaPlayer.Play();
        }

        public void PlayAmbient(Stream stream)
        {
            loopSoundPlayer.Stream = stream;
            loopSoundPlayer.Play();
        }

        public void StopAmbient() => loopSoundPlayer.Stop();

        public void Dispose()
        {
            mediaPlayer.Stop();
            loopSoundPlayer.Stop();
            if(loopSoundPlayer.Stream != null)
                loopSoundPlayer.Stream.Position = 0;
            loopSoundPlayer.Dispose();
        }
    }
}
