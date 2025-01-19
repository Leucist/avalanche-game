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
            _soundPlayer?.Play(filename, loop);
        }

        public static void Stop()
        {
            _soundPlayer?.Stop();
        }
    }
}
