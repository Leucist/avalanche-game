﻿using System;
using System.Collections.Generic;
using System.IO;
using SFML.Audio;

namespace Avalanche.Core
{
    public static class SoundManager
    {

        private static readonly Dictionary<SoundType, string> _sounds = new()
        {
            { SoundType.MainMenuBackground,       Path.Combine("Music", "Visager - Ice Cave.mp3") },
            { SoundType.VoiceOver1GameStart,      Path.Combine("Sounds", "CS1.mp3") },
            { SoundType.VoiceOver2GameStart,      Path.Combine("Sounds", "CS2.mp3") },
            { SoundType.VoiceOver3GameStart,      Path.Combine("Sounds", "CS3.mp3") },
            { SoundType.VoiceOver4GameStart,      Path.Combine("Sounds", "CS4.mp3") },
            { SoundType.VoiceOver5GameStart,      Path.Combine("Sounds", "CS5.mp3") },
            { SoundType.VoiceOver6GameStart,      Path.Combine("Sounds", "CS6.mp3") },
            { SoundType.VoiceOver7GameStart,      Path.Combine("Sounds", "CS7.mp3") },
            { SoundType.VoiceOver8GameStart,      Path.Combine("Sounds", "CS8.mp3") },
            { SoundType.CutScene1Start,           Path.Combine("Music", "wind.mp3") },
            { SoundType.CutScene2Start,           Path.Combine("Music", "CaveAudio.mp3") }
        }; 

        private static readonly string _audioFolderPath = GetAudioFolder();

        private static Music _currentMusic;

        private static readonly List<Sound> _activeSounds = new();

        public static void PlayMusic(SoundType type, bool loop = false)
        {
            StopMusic();

            string filePath = ResolveFilePath(type);
            if (string.IsNullOrEmpty(filePath)) return;

            try
            {
                _currentMusic = new Music(filePath)
                {
                    Loop = loop
                };
                _currentMusic.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to play music {filePath}: {ex.Message}");
            }
        }

        public static void StopMusic()
        {
            if (_currentMusic != null)
            {
                _currentMusic.Stop();
                _currentMusic.Dispose();
                _currentMusic = null;
            }
        }

        public static void PlaySound(SoundType type)
        {
            string filePath = ResolveFilePath(type);
            if (string.IsNullOrEmpty(filePath)) return;

            try
            {
                var buffer = new SoundBuffer(filePath);
                var sound = new Sound(buffer);
                sound.Play();
                _activeSounds.Add(sound);

                CleanupStoppedSounds();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to play sound {filePath}: {ex.Message}");
            }
        }

        public static void CleanupStoppedSounds()
        {
            _activeSounds.RemoveAll(s => s.Status == SoundStatus.Stopped);
        }

        private static string ResolveFilePath(SoundType type)
        {
            if (!_sounds.TryGetValue(type, out var relativePath))
            {
                Console.WriteLine($"SoundType {type} not found in dictionary.");
                return null;
            }

            string fullPath = Path.Combine(_audioFolderPath, relativePath);

            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"Audio file not found: {fullPath}");
                return null;
            }

            return fullPath;
        }

        private static string GetAudioFolder()
        {
            string finalPath = Pathfinder.GetAudioFolder();
            Console.WriteLine("currentDir = " + finalPath);

            //Console.WriteLine($"[SoundManager] Using Audio folder: {finalPath}");

            return finalPath;
        }
    }
}
