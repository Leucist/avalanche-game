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
            string solutionPath = Pathfinder.FindSolutionDirectory();
            string consoleFolder = "Avalanche.Console";
            _cutscenesFolderPath = Path.Combine(solutionPath, consoleFolder);
            _prevFrameNumber = _model._currentFrameNumber - 1;
        }

        public void Reset() {
            _prevFrameNumber = _model._currentFrameNumber - 1;
        }

        private void SetColors(CutsceneType cutsceneType) {
            switch (cutsceneType) {
                case CutsceneType.GameOver:
                    // Set red-black colors for skeletons animation
                    System.Console.BackgroundColor = ConsoleColor.Red;
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case CutsceneType.GameFinish:
                    // Set red-black colors for skeletons animation
                    System.Console.BackgroundColor = ConsoleColor.White;
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    break;
                default:
                    // Set default cutscene (inverted) colors
                    System.Console.BackgroundColor = ConsoleColor.Black;
                    System.Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        public void Render()
        {
            // Checks if there's nothing to Update
            if (_prevFrameNumber == _model._currentFrameNumber) return;

            // Clear the screen
            ConsoleRenderer.ClearScreen();

            // Updates the indicator int
            _prevFrameNumber = _model._currentFrameNumber;
            
            // Set comsole colors
            SetColors(GameState._cutscene);

            // Get path to the current frame
            string relativeFilePath = Pathfinder.GetFrameFilePath(
                _model._currentFrameNumber,
                (CutsceneType)_model._cutsceneNumber) + ".txt";
                
            // Combine with the directory path
            string filePath = Path.Combine(_cutscenesFolderPath, relativeFilePath);

            // Actual reading art from the file and drawing
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

            // Reset colors
            SetColors(CutsceneType.GameIntroduction);
        }
    }
}
