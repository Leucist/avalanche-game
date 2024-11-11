namespace Avalanche.Core
{
    public class NameInputController : ISceneController
    {
        private  readonly NameInputModel _model;

        public NameInputController(Player player) {
            _model = new NameInputModel(player);
        }

        public void Handle(ActionType action) {
            GameState._state = GameStateType.Cutscene;
        }

        public object GetModel() {
            return _model;
        }
    }
}