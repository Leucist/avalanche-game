using Avalanche.Core.Enums;

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
            if (action == ActionType.NullAction) {
                // For frame-by-frame cutscenes to stay "interactive" &
                // For video-like cutscenes to be played uninterrupted
                if (GameState._cutscene != CutsceneType.GameOver &&
                    GameState._cutscene != CutsceneType.GameFinish) {
                        return;
                    }
            }
            _model.Update(action);
        }

        public object GetModel() {
            return _model;
        }
    }
}