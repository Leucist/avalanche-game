using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public static class ConsoleRenderer
    {
        // private (int, int) _screenSize;

        // public ConsoleRenderer((int, int) screenSize) {
        //     _screenSize = screenSize;
        // }

        public static void ClearScreen() {
            System.Console.Clear();
        }

        public static void Alert(string message) {
            int centerX = System.Console.WindowWidth / 2 - message.Length / 2 - 1;
            if (centerX < 0) centerX = 0;
            int centerY = System.Console.WindowHeight / 2;

            System.Console.SetCursorPosition(centerX, centerY);

            System.Console.Write('|');
            System.Console.Write(message);
            System.Console.WriteLine('|');
        }

        public static void DrawBorderBox() {
            for (int i = 0; i < ScreenCharHeight; i++) {
                System.Console.Write('|');
                for (int j = 1; j < ScreenCharWidth-1; j++) {
                    switch (i) {
                        case 0:
                            System.Console.Write('T');
                            break;
                        case ScreenCharHeight - 1:
                            System.Console.Write('_');
                            break;
                        default:
                            System.Console.Write(' ');
                            break;
                    }
                    
                }
                System.Console.Write("|\n");
            }
        }
    }
}