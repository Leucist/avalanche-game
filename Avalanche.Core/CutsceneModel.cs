using Avalanche.Core.Enums;

namespace Avalanche.Core
{
    public class CutsceneModel
    {
        public int _cutsceneNumber { get; set; }
        public int _currentFrameNumber { get; set; }
        public bool _isOver { get; set; }

        public CutsceneModel(int cutsceneNumber) {
            _cutsceneNumber = cutsceneNumber;
            _currentFrameNumber = 0;
            _isOver = false;
        }

        public void NextCutscene() {
            _cutsceneNumber++;
            _currentFrameNumber = 0;
            _isOver = true;
        }

        public void NextFrame() {
            // Get amount of files
            string[] files = Directory.GetFiles(Path.Combine(FindSolutionDirectory(), "Avalanche.Console", GetCutsceneFolderPath()));
            int framesInCutscene = files.Length;
            if (_currentFrameNumber == framesInCutscene) {
                _isOver = true;
                // _currentFrameNumber = 0;
                return;
            }
            _currentFrameNumber++;
        }

        private string GetCutsceneFolderPath() {
            string cutscenesFolder = "Cutscenes";
            string cutsceneFolderName = "Cutscene_" + ((CutsceneType)_cutsceneNumber).ToString();
            return Path.Combine(cutscenesFolder, cutsceneFolderName);
        }

        public string GetFrameFilePath() {
            string cutsceneFolderPath = GetCutsceneFolderPath();
            string frameFileName = _currentFrameNumber.ToString();
            string frameFilePath = Path.Combine(cutsceneFolderPath, frameFileName);
            return frameFilePath;
        }

        public string FindSolutionDirectory()
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
    }
}