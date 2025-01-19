namespace Avalanche.Core
{
    public interface ISoundPlayer
    {
        void Play(string filename, bool loop = false);
        void Stop();
    }
}