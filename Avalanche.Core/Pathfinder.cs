namespace Avalanche.Core
{
    public static class Pathfinder
    {
        public static string FindSolutionDirectory()
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

        public static string GetGraphicsTexturesFolder() {
            return Path.Combine(FindSolutionDirectory(), "Avalanche.Graphics/textures");
        }
        public static string GetGraphicsFontsFolder() {
            return Path.Combine(FindSolutionDirectory(), "Avalanche.Graphics/fonts/Cinzel");
        }


        public static string GetCutsceneFolderPath(CutsceneType cutscene) {
            string cutscenesFolder = "Cutscenes";
            string cutsceneFolderName = "Cutscene_" + cutscene.ToString();
            return Path.Combine(cutscenesFolder, cutsceneFolderName);
        }

        public static string GetFrameFilePath(int frameNumber, CutsceneType cutscene) {
            string cutsceneFolderPath = GetCutsceneFolderPath(cutscene);
            string frameFileName = frameNumber.ToString();
            string frameFilePath = Path.Combine(cutsceneFolderPath, frameFileName);
            return frameFilePath;
        }
    }
}