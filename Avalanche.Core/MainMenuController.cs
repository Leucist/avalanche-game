namespace Avalanche.Core
{
    internal class MainMenuController : ISceneController
    {
        private MainMenuModel _model;
        // private MainMenuView _view;

        public MainMenuController()
        {
            _model = new MainMenuModel();
        }

        private void HandleSelection(GameStateType stateType) {
            GameState._state = stateType;
            // switch (stateType)
            // {
            //     case GameStateType.NewGame:
                    
            //         break;
            //     case GameStateType.OptionsMenu:
                    
            //         break;
            //     case GameStateType.Exit:
            //         Ga
            //         break;
            // }
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

        public MainMenuModel GetModel() {
            return _model;
        }
    }
}
