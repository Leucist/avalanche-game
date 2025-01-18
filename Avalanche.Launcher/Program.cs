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
            GameState._mode = args.Length > 0 && args[0].Equals("console", StringComparison.OrdinalIgnoreCase) ? GameModeType.Console : GameModeType.Graphics;
            // bool useGraphics = args.Length > 0 && args[0].Equals("graphics", StringComparison.OrdinalIgnoreCase);

            IGameView gameView;
            IInputController inputController;

            if (GameState._mode == GameModeType.Console)
            {
                System.Console.WriteLine("Launching console version of the game...");
                gameView = new ConsoleGameView();
                inputController = new ConsoleInputController();
                
            }
            else
            {
                System.Console.WriteLine("Launching graphics version of the game...");
                GraphicsGameView graphicsGameView = new();
                gameView = graphicsGameView;
                inputController = new GraphicsInputController(graphicsGameView.GetWindow());
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
