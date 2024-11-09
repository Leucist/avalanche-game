using Avalanche.Core;
using System.Runtime.InteropServices;

using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class ConsoleGameView : IGameView
    {
        // private IGameSceneModel gameSceneModel;

        public ConsoleGameView() {}

        public void Initialize() {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.Black;

            ConsoleRenderer.ClearScreen();
            System.Console.Write("\x1B[?25l");  // hides the cursor

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                System.Console.SetWindowSize(ScreenCharWidth, ScreenCharHeight);
            }
            CheckWindowSize();

            System.Console.Write("\x1B[?25h");  // returns cursor visibility
        }

        public void Render() {

        }
        
        private void CheckWindowSize() {
            (int, int) screenSize = (0, 0);
            (int, int) prevScreenSize = screenSize;
            while ((screenSize.Item1 != ScreenCharWidth) && 
                (screenSize.Item2 != ScreenCharHeight))
            {
                screenSize = (System.Console.WindowWidth, System.Console.WindowHeight);
                if (screenSize != prevScreenSize) {
                    ConsoleRenderer.ClearScreen();
                    ConsoleRenderer.Alert("PLEASE EXPAND THIS WINDOW");
                    prevScreenSize = screenSize;
                }
            }
        }
    }
}