using Avalanche.Core;

namespace Avalanche.Core
{
    public class CutsceneModel
    {
        public int _cutsceneNumber { get; set; }
        public int _currentFrameNumber { get; set; }
        public bool _isOver { get; set; }

        // *1 Quick hack:
        public bool _isPlayingAudio;

        public CutsceneModel(int cutsceneNumber) {
            _cutsceneNumber = cutsceneNumber;
            _currentFrameNumber = 0;
            _isOver = false;
            _isPlayingAudio = false;    // *1 Quick hack:
        }

        public bool WasSwitched() {
            // If cutscene was set externally
            return _cutsceneNumber != (int) GameState._cutscene;
        }

        private void UpdateToExternal() {
            _cutsceneNumber = (int) GameState._cutscene;
            ResetCutsceneAttributes();
        }

        private void ResetCutsceneAttributes() {
            // - Resets the cutscene attributes
            _currentFrameNumber = 0;
            _isOver = false;
        }

        private void NextCutscene() {
            _cutsceneNumber++;
        }

        private void CheckIfCutsceneEnded() {
            // - Count the number of frames in cutscene (counts amount of files) 
            string[] files = Directory.GetFiles(
                Path.Combine(
                    Pathfinder.FindSolutionDirectory(),
                    "Avalanche." + GameState._mode.ToString(),
                    Pathfinder.GetCutsceneFolderPath((CutsceneType)_cutsceneNumber)
                    )
                );
            int framesInCutscene = files.Length;

            // Check if cutscene has ended
            if (_currentFrameNumber >= framesInCutscene - 1) {
                _isOver = true;
            }
        }

        private void NextFrame() {
            // Exits if the cutscene is already over
            if (_isOver) return;

            // Increment frame number
            _currentFrameNumber++;

            _isPlayingAudio = false;    // *1 Quick hack
        }

        DateTime _frameDelay = DateTime.Now;
        public void Update(ActionType action) {
            // - Switch frame (as the View has already been rendered, this executes afterwards)
            if (GameState._mode == GameModeType.Graphics )
            {
                if ((DateTime.Now - _frameDelay).TotalSeconds >= 0.35f)
                {
                    _frameDelay = DateTime.Now;
                    NextFrame();
                }
            }
            else 
            {
                NextFrame();
            }

            // // Play cutscene Audio
            // SoundMainScene();

            // Check if cutscene hasn't ended
            CheckIfCutsceneEnded();

            if (_isOver) {
                // For the "eat mushroom" interactive cutscene
                if (_cutsceneNumber == (int) CutsceneType.GameStart
                    && action != ActionType.ConsumeMushroom) {
                    // if player presses anything except 'X' frame stays
                    return;
                }

                ResetCutsceneAttributes();
                
                HandleCutsceneOutcome();
            }

            // Set delay for the video-like cutscenes
            if (GameState._cutscene == CutsceneType.GameOver ||
                GameState._cutscene == CutsceneType.GameFinish)
            {
                Thread.Sleep(AppConstants.DefaultFrameTime * 2);
            }

            // Switch current cutscene if another was set in GameState
            if (WasSwitched()) {
                UpdateToExternal();
            }
        }

        private void HandleCutsceneOutcome() {
            // - Proceed after different types of Cutscenes
            switch ((CutsceneType) _cutsceneNumber) {
                case CutsceneType.GameFinish:
                    GameState._cutscene = CutsceneType.GameFinal;
                    break;
                case CutsceneType.GameStart:
                    GameState._cutscene = CutsceneType.GameIntroduction;
                    break;
                case CutsceneType.GameFinal:
                    GameState._state = GameStateType.MainMenu;
                    GameState._cutscene = CutsceneType.GameStart;
                    break;
                case CutsceneType.GameOver:
                    GameState._state = GameStateType.MainMenu;
                    GameState._cutscene = CutsceneType.GameStart;
                    break;
                default:
                    NextCutscene();
                    GameState._state = GameStateType.Game;
                    GameState._cutscene = (CutsceneType) _cutsceneNumber;
                    break;
            }
        }

        // Made public for quick hack
        public void SoundMainScene()
        {
            if (GameState._cutscene == CutsceneType.GameStart)
            {
                _isPlayingAudio = true;    // *1 Quick hack

                SoundType? sound = null;
                SoundType? music = null;

                // Stop all single sounds playing
                SoundManager.Instance.StopAllSounds();

                switch (_currentFrameNumber)
                {
                    case 0:
                        SoundManager.Instance.StopMusic();
                        music = SoundType.CutScene1Start;
                        sound = SoundType.VoiceOver1GameStart;
                        break;
                    case 1:
                        sound = SoundType.VoiceOver2GameStart;
                        break;
                    case 2:
                        sound = SoundType.VoiceOver3GameStart;
                        break;
                    case 3:
                        sound = SoundType.VoiceOver4GameStart;
                        break;
                    case 4:
                        music = SoundType.CutScene2Start;
                        sound = SoundType.VoiceOver5GameStart;
                        break;
                    case 5:
                        sound = SoundType.VoiceOver6GameStart;
                        break;
                    case 6:
                        sound = SoundType.VoiceOver7GameStart;
                        break;
                    case 7:
                        sound = SoundType.VoiceOver8GameStart;
                        break;
                }

                if (sound != null) SoundManager.Instance.PlaySound((SoundType) sound);
                if (music != null) SoundManager.Instance.PlayMusic((SoundType) music, true);
            }
        }
    }
}
