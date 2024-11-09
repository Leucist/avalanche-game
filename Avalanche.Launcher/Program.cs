using Avalanche.Core;
using Avalanche.Console;
using Avalanche.Graphics;

namespace Avalanche.Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check command line arguments
            // TEMP CHANGE from > bool useConsole = args.Length > 0 && args[0].Equals("console", StringComparison.OrdinalIgnoreCase);
            bool useGraphics = args.Length > 0 && args[0].Equals("graphics", StringComparison.OrdinalIgnoreCase);

            IGameView gameView;
            IInputController inputController;

            // TEMP CHANGE from > if (useConsole)
            if (!useGraphics)
            {
                System.Console.WriteLine("Launching console version of the game...");
                gameView = new ConsoleGameView();
                inputController = new ConsoleInputController();
                
            }
            else
            {
                throw new NotImplementedException("The graphical interface has not been implemented yet.");
                // System.Console.WriteLine("Launching graphical version of the game...");
                // gameView = new GraphicsGameView();
                // inputController = new GraphicsInputController();
            }

            // Creates new game instance
            GameController gameController = new GameController(gameView, inputController);

            gameController.StartGame();
        }

        // private static void InitConsole() {
        //     IGameView gameView = new ConsoleGameView();
        // }

        // private static void InitGraphics() {
        //     IGameView gameView = new GraphicsGameView();
        // }
    }
}
