namespace Avalanche.Core.Commands
{
    public class SelectMenuOption(MainMenuController controller) : MainMenuCommand(controller)
    {
        public override void Execute() {
            _controller.Select();
        }
    }
}