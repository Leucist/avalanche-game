using Avalanche.Core.Commands;

namespace Avalanche.Core
{
    public class MainMenuCommandFactory : ICommandFactory
    {
        private MainMenuController _controller;

        public MainMenuCommandFactory(MainMenuController controller)
        {
            _controller = controller;
        }

        public ICommand CreateCommand(ActionType action)
        {
            return action switch {
                ActionType.Up       =>  new MoveMenuCursorUp(_controller),
                ActionType.Down     =>  new MoveMenuCursorDown(_controller),
                ActionType.Enter    =>  new SelectMenuOption(_controller),
                _                   =>  new NullCommand()
            };
        }
    }
}