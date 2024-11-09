using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public static class ConsoleRenderer
    {
        public static void ClearScreen() {
            System.Console.Clear();
        }
        
        public static void HideCursor() {
            System.Console.Write("\x1B[?25l");
        }

        public static void ShowCursor() {
            System.Console.Write("\x1B[?25h");
        }

        public static void DrawAlert(string message) {
            int centerX = GetCenterX() - message.Length / 2 - 1;
            if (centerX < 0) centerX = 0;
            int centerY = GetCenterY();

            System.Console.SetCursorPosition(centerX, centerY);

            System.Console.Write('|');
            System.Console.Write(message);
            System.Console.WriteLine('|');
        }

        public static void DrawCornerMarkers() {
            int[][] corners = {
                [0, 0], 
                [ScreenCharWidth, 0], 
                [0, ScreenCharHeight], 
                [ScreenCharWidth, ScreenCharHeight]};

            foreach (int[] corner in corners) {
                System.Console.SetCursorPosition(corner[0], corner[1]);
                System.Console.Write('#');
            }
        }

        public static void DrawWidthArrows() {
            for (int i = 0; i < ScreenCharHeight; i+=4) {
                System.Console.Write('<');
                for (int j = 1; j < ScreenCharWidth-1; j++) {
                    System.Console.Write('-');
                }
                System.Console.Write(">\n");
            }
        }

        private static int GetCenterX() {
            return System.Console.WindowWidth / 2;
        }
        private static int GetCenterY() {
            return System.Console.WindowHeight / 2;
        }
    }
}