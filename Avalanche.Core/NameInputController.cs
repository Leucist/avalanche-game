namespace Avalanche.Core
{
    public class NameInputController : ISceneController
    {
        private  readonly NameInputModel _model;

        public NameInputController(Player player) {
            _model = new NameInputModel(player);
        }

        public void Handle(ActionType action)
        {
            if (GameState._mode == GameModeType.Console || (action == ActionType.Enter && !_model.isNicknameNull))
            {
                GameState._state = GameStateType.Game;
                
            }
        }
        public object GetModel() {
            return _model;
        }
    }
}