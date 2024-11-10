using Avalanche.Core;

namespace Avalanche.Console
{
    public class ConsoleNameInputView : IView
    {
        public NameInputModel _model { get; }
        public ConsoleNameInputView(NameInputModel model) {
            _model = model;
        }

        public void Render()
        {
            int centerX = System.Console.WindowWidth / 2;
            int centerY = System.Console.WindowHeight / 2;
            string label = "Input Name for your hero:";
            ConsoleRenderer.ClearScreen();
            System.Console.SetCursorPosition(centerX - label.Length / 2, (int) (centerY * 0.75));
            System.Console.WriteLine(label);
            System.Console.SetCursorPosition(centerX - label.Length / 2, centerY);
            ConsoleRenderer.ShowCursor();
        }
    }
}