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
            if (_model._isOver) {
                _model.NextCutscene();
                GameState._state = GameStateType.Game;
            }
        }

        public object GetModel() {
            return _model;
        }
    }
}