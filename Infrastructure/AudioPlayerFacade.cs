using NetCoreAudio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Infrastructure
{
    class AudioPlayerFacade
    {
        private Player _player { get; set; }
        protected readonly object lockObj = new object();

        private static AudioPlayerFacade audioPlayerFacade { get; set; }
        private AudioPlayerFacade()
        {
            _player = new Player();
        }

        public static AudioPlayerFacade GetInstance()
        {
            if (audioPlayerFacade == null)
                audioPlayerFacade = new AudioPlayerFacade();

            return audioPlayerFacade;
        }

        public void Play(string audioPath)
        {
                Task.Factory.StartNew(() => Console.Beep(40, 3));
                return;
        }

        public void Stop()
        {
            _player.Stop();
        }

        public bool IsPlaying()
        {
            return _player.Playing;
        }
    }
}
