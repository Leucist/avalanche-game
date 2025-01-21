namespace Avalanche.Core.Commands
{
    public class MoveMenuCursorDown(MainMenuController controller) : MainMenuCommand(controller)
    {
        public override void Execute() {
            _controller.MoveCursorDown();
        }
    }
}