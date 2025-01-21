namespace Avalanche.Core
{
    public class OptionsController : ISceneController
    {
        private readonly OptionsModel _model;

        public OptionsController() {
            _model = new();
        }

        public void Handle(ActionType action) {
            switch (action) {
                case ActionType.Left:
                    _model.LowerDifficulty();
                    break;
                case ActionType.Right:
                    _model.RiseDifficulty();
                    break;
                case ActionType.Enter:
                    _model.SubmitDifficulty();
                    break;
                case ActionType.Escape:
                    GameState._state = GameStateType.MainMenu;
                    break;
            }

        }

        public object GetModel() {
            return _model;
        }
    }
}
