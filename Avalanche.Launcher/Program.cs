using System;
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
            
            // Creates new game instance
            var gameController = new GameController();
 
            IGameView gameView;

            if (useConsole)
            {
                Console.WriteLine("Launching console version of the game...");
                gameView = new ConsoleGameView();  // Console View
            }
            else
            {
                Console.WriteLine("Launching graphical version of the game...");
                gameView = new GraphicsGameView();  // Graphical View
            }

            // Pass the view and launches the game
            gameController.SetView(gameView);
            gameController.Run();
        }
    }
}
