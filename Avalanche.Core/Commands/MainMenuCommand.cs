namespace Avalanche.Core.Commands
{
    public abstract class MainMenuCommand : ICommand
    {
        protected readonly MainMenuController _controller;

        public MainMenuCommand(MainMenuController controller) {
            _controller = controller;
        }

        public abstract void Execute();
    }
}