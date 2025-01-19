using SFML.Audio;
using Avalanche.Core;

namespace Avalanche.Graphics
{
    public class SoundPlayer : ISoundPlayer {
        // Static instance for Singleton -> Only one player may exist at a time
        private static SoundPlayer _instance;

        private Sound? _sound;
        private SoundBuffer? _buffer;
        private string _folderPath;

        private SoundPlayer() { 
            _folderPath = Pathfinder.GetAudioFolder();
        }

        // Return the existing instance or a newly created
        public static SoundPlayer Instance => _instance ??= new SoundPlayer();

        public void Play(string filename, bool loop)
        {
            try
            {
                // Ensure output resources are released and free for use
                Stop();

                // Get full path for the audio file
                string filePath = Path.Combine(_folderPath, filename);

                // Ensure file exists
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Audio file not found: {filePath}");
                    return;
                }

                // Configure variables and Play the audio
                _buffer = new SoundBuffer(filePath);
                _sound = new Sound(_buffer);
                _sound.Loop = loop;
                _sound.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing audio: {ex.Message}");
            }
        }

        public void Stop()
        {
            _sound?.Stop();
            _sound?.Dispose();
            _buffer?.Dispose();
        }
    }
}