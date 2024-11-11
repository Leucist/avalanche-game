using Avalanche.Core;
using System.Linq.Expressions;
using static Avalanche.Core.AppConstants;

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
            //string label = "Input Name for your hero:";
            string[] label = new string[]
            {
                "███ ██  █ █████ █  ██ █████   ██  █ █████ █   █ █████    █████ █████ █████   █   █ █████ █   █  █████   █   █ █████ █████ █████  ",
                " █  ██  █ █   █ █  ██   █     ██  █ █   █ ██ ██ █        █     █   █ █   █    █ █  █   █ █   █  █   █   █   █ █     █   █ █   █ █",
                " █  █ █ █ █████ █ ███   █     █ █ █ █████ █ █ █ █████    █████ █   █ █████     █   █   █ █   █  █████   █████ █████ █████ █   █  ",
                " █  █  ██ ██    █ █ █   █     █  ██ █   █ █   █ █        █     █   █ █  █      █   █   █ █   █  █  █    █   █ █     █  █  █   █ █",
                "███ █  ██ ██    ██  █   █     █  ██ █   █ █   █ █████    █     █████ █  ██     █   █████ █████  █  ██   █   █ █████ █  ██ █████  "
            };
            string afterLabel = "PRESS ANY KEY TO CONTINUE...";


            // Show questio
            ConsoleRenderer.ClearScreen();
            //System.Console.SetCursorPosition(centerX - label.Length / 2, (int) (centerY * 0.75));
            //System.Console.WriteLine(label);

            for (int height = 0; height < ScreenCharHeight / 3; height++)
            {
                System.Console.WriteLine();
            }

            foreach (string element in label)
            {
                System.Console.Write(new string(' ', (int)(ScreenCharWidth / 5.71)));
                System.Console.WriteLine(element);
            }

            System.Console.WriteLine(new string(' ', 5));
            System.Console.Write(new string(' ', (int)(ScreenCharWidth / 5.71)));
            //System.Console.SetCursorPosition(centerX - label.Length / 2, centerY);
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
        }
    }
}