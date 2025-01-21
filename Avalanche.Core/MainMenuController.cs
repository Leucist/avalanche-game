namespace Avalanche.Core
{
    public class MainMenuController : ISceneController
    {
        private MainMenuModel _model;
        // private MainMenuView _view;

        public MainMenuController()
        {
            _model = new MainMenuModel();
        }

        private void HandleSelection(GameStateType stateType) {
            GameState._state = stateType;
        }

        public void Handle(ActionType action) {
            switch (action) {
                case ActionType.Up:
                    _model.MoveCursorUp();
                    break;
                case ActionType.Down:
                    _model.MoveCursorDown();
                    break;
                case ActionType.Enter:
                    HandleSelection(_model.GetCurrentKey());
                    break;
            }
        }

        public void MoveCursorUp()   => _model.MoveCursorUp();
        public void MoveCursorDown() => _model.MoveCursorDown();
        public void Select()         => HandleSelection(_model.GetCurrentKey());

        public object GetModel() {
            return _model;
        }
    }
}
