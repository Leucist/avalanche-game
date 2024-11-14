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

        public bool WasSwitched() {
            // If cutscene was set externally
            return _cutsceneNumber != (int) GameState._cutscene;
        }

        private void UpdateToExternal() {
            _cutsceneNumber = (int) GameState._cutscene;
        }

        private void ResetCutscene() {
            // - Resets the cutscene attributes
            _currentFrameNumber = 0;
            _isOver = false;
        }

        private void NextCutscene() {
            _cutsceneNumber++;
        }

        private void NextFrame() {
            // Increment frame number
            _currentFrameNumber++;

            // - Count the number of frames in cutscene (Get amount of files and count) 
            string[] files = Directory.GetFiles(Path.Combine(FindSolutionDirectory(), "Avalanche.Console", GetCutsceneFolderPath()));
            int framesInCutscene = files.Length;

            // Check if cutscene has ended
            if (_currentFrameNumber > framesInCutscene) {
                _isOver = true;
            }
        }

        public void Update(ActionType action) {
            // Switch int pointer to the next frame
            NextFrame();

            if (WasSwitched()) {
                UpdateToExternal();
            }

            else {
                if (_isOver) {
                    // For the "eat mushroom" interactive cutscene
                    if (_cutsceneNumber == (int) CutsceneType.GameStart
                        && action != ActionType.ConsumeMushroom) {
                        // if player presses anything except 'X' frame stays
                        return;
                    }
                    NextCutscene();
                    
                    // - Proceed after different types of Cutscenes
                    switch ((CutsceneType) _cutsceneNumber) {
                        case CutsceneType.GameFinish:
                            GameState._cutscene = CutsceneType.GameFinal;
                            break;
                        case CutsceneType.GameFinal:
                            GameState._state = GameStateType.MainMenu;
                            break;
                        case CutsceneType.GameOver:
                            GameState._state = GameStateType.MainMenu;
                            break;
                        default:
                            GameState._state = GameStateType.Game;
                            break;
                    }
                }
            }

            // Set delay for the video-like cutscenes
            if (GameState._cutscene == CutsceneType.GameOver ||
                GameState._cutscene == CutsceneType.GameFinish)
            {
                Thread.Sleep(AppConstants.DefaultFrameTime * 2);
            }

            ResetCutscene();
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