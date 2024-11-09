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
                System.Console.SetWindowSize(ScreenWidth, ScreenHeight);
            }
            CheckWindowSize();

            System.Console.Write("\x1B[?25h");  // returns cursor visibility
        }

        public void Render() {

        }
        
        private void CheckWindowSize() {
            // (int, int) screenSize;
            // if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            // {
            //     screenSize = GetWindowsScreenSize();
            // }
            // else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
            //     screenSize = GetLinuxScreenSize();
            // }
            // else { 
            //     System.Console.WriteLine("Screen Size checking is not implemented for your platform");
            //     return;
            // }
            // AppConstants.ScreenWidth = screenSize.Item1;
            // AppConstants.ScreenHeight = screenSize.Item2;

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

        // private (int, int) GetWindowsScreenSize() {
        //     [DllImport("user32.dll")]
        //     static extern int GetSystemMetrics(int nIndex);

        //     const int SM_CXSCREEN = 0;
        //     const int SM_CYSCREEN = 1;

        //     // screenWidth = GetSystemMetrics(SM_CXSCREEN);
        //     // screenHeight = GetSystemMetrics(SM_CYSCREEN);

        //     return (GetSystemMetrics(SM_CXSCREEN),GetSystemMetrics(SM_CYSCREEN));
        // }

        // private (int, int) GetLinuxScreenSize() { return (1920, 1080); }
    }
}