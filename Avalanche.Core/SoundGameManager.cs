namespace Avalanche.Core
{
    public static class SoundGameManager
    {
        private static ISoundPlayer? _soundPlayer;
        private static Dictionary<SoundType, string> _sounds = new() {
            {SoundType.MainMenuBackground, "Music/Visager - Ice Cave.mp3"}
        };

        public static void SetSoundPlayer(ISoundPlayer soundPlayer)
        {
            _soundPlayer = soundPlayer;
        }

        public static void Play(SoundType sound, bool loop = false) {
            Play(_sounds[sound], loop);
        }

        public static void Play(string filename, bool loop = false)
        {
            if (loop)
                PlayOnRepeat(filename);
            else
                PlaySingleSound(filename);
        }

        public static void PlayOnRepeat(string filename)
        {
            // StopAll();
            _soundPlayer?.PlayOnRepeat(filename);
        }

        public static void PlaySingleSound(string filename)
        {
            _soundPlayer?.PlaySingleSound(filename);
        }

        public static void StopAll()
        {
            _soundPlayer?.StopAll();
        }

        public static void StopOnRepeat()
        {
            _soundPlayer?.StopOnRepeat();
        }
    }
}
