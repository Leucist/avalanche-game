using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public static class ConsoleCutscene
    {
        public static void Render(string artName, ConsoleColor color = ConsoleColor.White, 
            int TimeToSleep = DefaultCutsceneTime)
        {
            TimeToSleep *= 1000;
            string localPath = "CutScenes\\";
            string projectPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
            string filePath = Path.Combine(projectPath,localPath, artName);

            System.Console.ForegroundColor = color;

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    System.Console.WriteLine(line);
                }
                Thread.Sleep(TimeToSleep);
            }

            catch (Exception ex)
            {
               System.Console.WriteLine("Error reading the file: " + ex.Message);  // I'm not chatGPT, trust me, bro
            }
        }
    }
}
