namespace Avalanche.Core.Commands
{
    public class MoveMenuCursorUp(MainMenuController controller) : MainMenuCommand(controller)
    {
        public override void Execute() {
            _controller.MoveCursorUp();
        }
    }
}