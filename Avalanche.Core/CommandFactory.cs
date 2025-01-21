using Avalanche.Core.Commands;

namespace Avalanche.Core
{
    public class CommandFactory : ICommandFactory
    {
        private readonly Dictionary<GameStateType, ICommandFactory> _commandFactories = [];

        public void AddCommandFactory(GameStateType state, ISceneController controller) {
            _commandFactories[state] = state switch {
                GameStateType.MainMenu  =>  new MainMenuCommandFactory((MainMenuController) controller),
                _                       =>  throw new NotImplementedException(),
            };
        }

        public ICommand CreateCommand(ActionType action) {
            return _commandFactories[GameState._state].CreateCommand(action);
        }
    }
}