using Avalanche.Core;

namespace Avalanche.Console
{
    public class ConsoleNameInputView : IView
    {
        public NameInputModel _model { get; }
        public string _name { get; set; }

        public ConsoleNameInputView(NameInputModel model) {
            _model = model;
            _name = AppConstants.DefaultPlayerName;
        }

        public void Render()
        {
            // Get center coords
            int centerX = System.Console.WindowWidth / 2;
            int centerY = System.Console.WindowHeight / 2;
            // Init labels
            string label = "Input Name for your hero:";
            string afterLabel = "Press any key to continue...";

            // Show questio
            ConsoleRenderer.ClearScreen();
            System.Console.SetCursorPosition(centerX - label.Length / 2, (int) (centerY * 0.75));
            System.Console.WriteLine(label);
            System.Console.SetCursorPosition(centerX - label.Length / 2, centerY);
            ConsoleRenderer.ShowCursor();

            // User inputs character name
            string? newName = System.Console.ReadLine();
            if (newName != null) _name = newName;
            _model.Submit(_name); 

            // Write afterLabel below
            System.Console.SetCursorPosition(centerX - afterLabel.Length / 2, (int) (centerY * 1.25));
            System.Console.WriteLine(afterLabel);

            // Hide cursor and Clear the game screen
            ConsoleRenderer.HideCursor();
            ConsoleRenderer.ClearScreen();
        }
    }
}