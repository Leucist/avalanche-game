using CSCore.SoundOut;
using CSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore.Codecs;

namespace Avalanche.Core
{
    public static class SoundGameManager
    {
        private static ISoundOut _soundOut; // Handles audio playback
        private static IWaveSource _waveSource; // Represents the audio file source

        private static string FindSolutionDirectory()
        {
            // Current directory of the executing process
            string startDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var directory = new DirectoryInfo(startDirectory);

            // Go up until we find solution file (.sln)
            while (directory != null && !File.Exists(Path.Combine(directory.FullName, "Avalanche.sln")))
            {
                directory = directory.Parent;
            }

            // Return path if it's found
            if (directory != null)
            {
                return directory.FullName;
            }

            // Otherwise exception is thrown
            throw new DirectoryNotFoundException("Solution root was not found.");
        }

        public static void Play(string filePath)
        {
            try
            {
                Stop();

                string slnPath = FindSolutionDirectory(); // IDK how to use it

                string FullPath = Path.Combine(slnPath, filePath);  // DOESN'T WORK (IDK how to use it)

                // Initialize the audio source and output
                _waveSource = CodecFactory.Instance.GetCodec(FullPath);
                _soundOut = new WasapiOut();

                _soundOut.Initialize(_waveSource);
                _soundOut.Play();

                Console.WriteLine($"Playing audio: {FullPath}");
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing audio: {ex.Message}");
            }
        }

        public static void Stop()
        {
            try
            {
                _soundOut?.Stop();
                _soundOut?.Dispose(); // Release output resources
                _waveSource?.Dispose(); // Release source resources

                _soundOut = null;
                _waveSource = null;

                Console.WriteLine("Audio playback stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping audio: {ex.Message}");
            }
        }
    }
}
