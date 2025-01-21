namespace Avalanche.Core
{
    public class CommandFactoriesManager : ICommandFactory
    {
        private readonly Dictionary<GameStateType, ICommandFactory> _commandFactories = [];

        public void AddCommandFactory(GameStateType state, ICommandFactory factory) {
            _commandFactories[state] = factory;
        }

        public ICommand CreateCommand(ActionType action) {
            return _commandFactories[GameState._state].CreateCommand(action);
        }
    }
}