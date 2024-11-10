namespace Avalanche.Core
{
    public class NameInputController : ISceneController
    {
        private  readonly NameInputModel _model;
        private string _name;

        public NameInputController(Player player) {
            _model = new NameInputModel(player);
            _name = AppConstants.DefaultPlayerName;
        }

        public void Handle(ActionType action) {
            string? newName = System.Console.ReadLine();
            if (newName != null) _name = newName;

            if (action == ActionType.Enter) {
                _model.Submit(_name);
            }
            GameState._state = GameStateType.Game;
        }

        public object GetModel() {
            return _model;
        }
    }
}