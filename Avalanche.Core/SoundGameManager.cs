namespace Avalanche.Core
{
    public static class SoundGameManager
    {
        private static ISoundPlayer? _soundPlayer;

        public static void SetSoundPlayer(ISoundPlayer soundPlayer)
        {
            _soundPlayer = soundPlayer;
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
