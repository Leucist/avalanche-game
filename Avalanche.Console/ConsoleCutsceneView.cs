using Avalanche.Core;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class ConsoleCutsceneView : IView
    {
        private CutsceneModel _model;
        private string _cutscenesFolderPath;
        public int _framesCount { get; set; }

        public ConsoleCutsceneView(CutsceneModel model) {
            _model = model;
            // Set Cutscenes folder for the console
            string solutionPath = _model.FindSolutionDirectory();
            string consoleFolder = "Avalanche.Console";
            _cutscenesFolderPath = Path.Combine(solutionPath, consoleFolder);
        }

        public void Render()
        {
            // Clear the screen
            ConsoleRenderer.ClearScreen();

            // Get path to the current frame
            string relativeFilePath = _model.GetFrameFilePath()  + ".txt";
            
            // Combine with the directory path
            string filePath = Path.Combine(_cutscenesFolderPath, relativeFilePath);
            
            // Set console font color
            //ConsoleColor color = ConsoleColor.White;
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.Black;

            int TimeToSleep = DefaultCutsceneTime;
            // TimeToSleep *= 1000;
            try
            {
                // Read ASCII-Art file
                string[] lines = File.ReadAllLines(filePath);

                // Get starting point to draw art in the center
                int startX = System.Console.WindowWidth / 2 - lines[0].Length / 2;
                int startY = System.Console.WindowHeight / 2 - lines.Length / 2;

                if (startY < 0)
                {
                    startY = 0;
                }
                
                // Draw ASCII-Art
                System.Console.Write(new string('\n', startY));
                foreach (string line in lines)
                {
                    System.Console.Write(new string(' ', startX));
                    System.Console.WriteLine(line);
                }

                // Wait
                Thread.Sleep(TimeToSleep);
            }

            catch (Exception ex)
            {
               System.Console.WriteLine("Error reading the file: " + ex.Message);  // I'm not chatGPT, trust me, bro
            }

            _model.NextFrame();
        }
    }
}
