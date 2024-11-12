namespace Avalanche.Core
{
    public class CutsceneController : ISceneController
    {
        CutsceneModel _model;

        public CutsceneController()
        {
            _model = new CutsceneModel(0);
        }
        
        public void Handle(ActionType action) {
            if (action == ActionType.NullAction) return;
            if (_model._isOver) {
                if (_model._cutsceneNumber == 0 && action != ActionType.ConsumeMushroom) {
                    // if player presses anything except 'X' frame stays
                    return;
                }
                _model.NextCutscene();
                GameState._state = GameStateType.Game;
            }
            else {
                _model.NextFrame();
            }
        }

        public object GetModel() {
            return _model;
        }
    }
}