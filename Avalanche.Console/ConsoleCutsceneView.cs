using System.Drawing;
using Avalanche.Core;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class ConsoleCutsceneView : IView
    {
        private CutsceneModel _model;
        private string _cutscenesFolderPath;
        public int _framesCount { get; set; }
        private int _prevFrameNumber { get; set; }

        public ConsoleCutsceneView(CutsceneModel model) {
            _model = model;
            // Set Cutscenes folder for the console
            string solutionPath = _model.FindSolutionDirectory();
            string consoleFolder = "Avalanche.Console";
            _cutscenesFolderPath = Path.Combine(solutionPath, consoleFolder);
            _prevFrameNumber = model._currentFrameNumber - 1;
        }

        public void Render()
        {
            if (_prevFrameNumber == _model._currentFrameNumber) return;
            // Clear the screen
            ConsoleRenderer.ClearScreen();
            _prevFrameNumber = _model._currentFrameNumber;
            if (GameState._state == GameStateType.GameOver) {
                // Set console colors
                System.Console.BackgroundColor = ConsoleColor.Red;
                System.Console.ForegroundColor = ConsoleColor.Black;
            }
            else {
                // Set console colors
                System.Console.BackgroundColor = ConsoleColor.Black;
                System.Console.ForegroundColor = ConsoleColor.White;
            }

            // Get path to the current frame
            string relativeFilePath = _model.GetFrameFilePath()  + ".txt";
            
            // Combine with the directory path
            string filePath = Path.Combine(_cutscenesFolderPath, relativeFilePath);

            // int TimeToSleep = DefaultCutsceneTime;
            // TimeToSleep *= 1000;
            try
            {
                // Read ASCII-Art file
                string[] lines = File.ReadAllLines(filePath);

                // Get starting point to draw art in the center
                int startX = System.Console.WindowWidth / 2 - lines[0].Length / 2;
                int startY = System.Console.WindowHeight / 2 - lines.Length / 2;
                if (startX < 0 ) startX = 0;
                if ( startY < 0) startY = 0;
                
                // Draw ASCII-Art
                System.Console.Write(new string('\n', startY));
                foreach (string line in lines)
                {
                    System.Console.Write(new string(' ', startX));
                    System.Console.WriteLine(line);
                }
            }

            catch (Exception ex)
            {
               System.Console.WriteLine("Error reading the file: " + ex.Message);  // I'm not chatGPT, trust me, bro
            }

            if (GameState._state == GameStateType.GameOver) {
                _model.NextFrame();
                // Wait
                Thread.Sleep(DefaultFrameTime * 2);
            }
        }
    }
}
