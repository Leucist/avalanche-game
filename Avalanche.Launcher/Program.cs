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
            bool useConsole = args.Length > 0 && args[0].Equals("console", StringComparison.OrdinalIgnoreCase);
            // bool useGraphics = args.Length > 0 && args[0].Equals("graphics", StringComparison.OrdinalIgnoreCase);

            IGameView gameView;
            IInputController inputController;

            if (useConsole)
            {
                System.Console.WriteLine("Launching console version of the game...");
                GameState._mode = GameModeType.Console;
                gameView = new ConsoleGameView();
                inputController = new ConsoleInputController();
                
            }
            else
            {
                GraphicsGameView graphicsGameView = new();
                GameState._mode = GameModeType.Graphics;
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
