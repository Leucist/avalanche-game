namespace Avalanche.Core
{
    public class CutsceneController : ISceneController
    {
        CutsceneModel _model;

        public CutsceneController()
        {
            _model = new CutsceneModel((int) CutsceneType.GameStart);
        }
        
        public void Handle(ActionType action) {
            // *1 Quick hack:
            if (_model._currentFrameNumber == 0 && !_model._isPlayingAudio) { _model.SoundMainScene(); }

            // ~ note: okay, looks somewhat unorthodoxal, so "if" structure may be changed

            // - IF USER presses NOTHING and CUTSCENE is not one of the stated below, it will return and NOT UPDATE
            if (action == ActionType.NullAction) {
                // For frame-by-frame cutscenes to stay "interactive" &
                // For video-like cutscenes to be played uninterrupted
                if (GameState._cutscene != CutsceneType.GameOver &&
                    GameState._cutscene != CutsceneType.GameFinish) {
                        return;
                    }
            }

            _model.Update(action);

            if (!_model._isPlayingAudio) _model.SoundMainScene();
        }

        public object GetModel() {
            return _model;
        }
    }
}