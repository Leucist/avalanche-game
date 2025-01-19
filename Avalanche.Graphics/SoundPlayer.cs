using Avalanche.Core;
using SFML.Audio;
using Avalanche.Core;

namespace Avalanche.Graphics
{
    public class SoundPlayer : ISoundPlayer {
        // Static instance for Singleton -> Only one player may exist at a time
        private static SoundPlayer _instance;

        private Sound _music;
        private SoundBuffer _musicBuffer;
        private List<Sound> _activeSounds = new List<Sound>(); // List to store active sound effects
        private string _folderPath = Pathfinder.GetAudioFolder();


        // Return the existing instance or a newly created
        public static SoundPlayer Instance => _instance ??= new SoundPlayer();

        private SoundPlayer() { 
            _folderPath = Pathfinder.GetAudioFolder();
        }

        /// <summary>
        /// Plays background music, replacing the currently playing track if any.
        /// </summary>
        /// <param name="filename">The name of the audio file.</param>
        /// <param name="loop">Whether the music should loop (default: true).</param>
        public void PlayOnRepeat(string filename)
        {
            try
            {
                StopOnRepeat();

                string filePath = Path.Combine(_folderPath, filename);
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Music file not found: {filePath}");
                    return;
                }

                _musicBuffer = new SoundBuffer(filePath);
                _music = new Sound(_musicBuffer) { Loop = true };
                _music.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing music: {ex.Message}");
            }
        }

        /// <summary>
        /// Plays a sound effect without interrupting background music.
        /// </summary>
        /// <param name="filename">The name of the sound effect file.</param>
        public void PlaySingleSound(string filename)
        {
            try
            {
                string filePath = Path.Combine(_folderPath, filename);
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Sound effect file not found: {filePath}");
                    return;
                }

                var buffer = new SoundBuffer(filePath);
                var sound = new Sound(buffer);
                sound.Play();

                _activeSounds.Add(sound);

                // Remove finished sounds from the list to free memory
                _activeSounds.RemoveAll(s => s.Status == SoundStatus.Stopped);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing sound effect: {ex.Message}");
            }
        }

        /// <summary>
        /// Stops the currently playing background music.
        /// </summary>
        public void StopOnRepeat()
        {
            _music?.Stop();
            _music?.Dispose();
            _musicBuffer?.Dispose();
        }

        /// <summary>
        /// Stops all sounds, including music and effects.
        /// </summary>
        public void StopAll()
        {
            StopOnRepeat();
            foreach (var sound in _activeSounds)
            {
                sound.Stop();
                sound.Dispose();
            }
            _activeSounds.Clear();
        }
    }
}
