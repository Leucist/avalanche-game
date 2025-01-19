namespace Avalanche.Core
{
    public interface ISoundPlayer
    {
        void PlayOnRepeat(string filename);
        void PlaySingleSound(string filename);
        void StopAll();
        void StopOnRepeat();
    }
}