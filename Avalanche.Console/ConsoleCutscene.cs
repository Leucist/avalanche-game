using Avalanche.Core;
using Avalanche.Core.Interfaces;


namespace Avalanche.Console
{
    public class ConsoleCutscene
    {
        public string localPath = "CutScenes\\";
        public void Render(string artName)
        {
            string projectPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
            string filePath = Path.Combine(projectPath,localPath, artName);

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    System.Console.WriteLine(line);
                }
            }

            catch (Exception ex)
            {
               System.Console.WriteLine("Error reading the file: " + ex.Message);  // I'm not chatGPT, trust me, bro
            }
        }
    }
}
