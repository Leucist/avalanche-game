using Avalanche.Core;
using System.Drawing;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public static class ConsoleRenderer
    {
        public static Dictionary<GameObjectType, char> textures = new() {
            { GameObjectType.Wall, '█' },
            { GameObjectType.Player, '웃' },
            { GameObjectType.Enemy, '☠' }
        };
        public static void ClearScreen() {
            System.Console.Clear();
        }
        
        public static void HideCursor() {
            System.Console.Write("\x1B[?25l");
        }

        public static void ShowCursor() {
            System.Console.Write("\x1B[?25h");
        }

        public static void SetTextColor(ConsoleColor color) {
            System.Console.ForegroundColor = color;
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
            int[][] corners = [
                [0, 0], 
                [ScreenCharWidth, 0], 
                [0, ScreenCharHeight], 
                [ScreenCharWidth, ScreenCharHeight]];

            foreach (int[] corner in corners) {
                System.Console.SetCursorPosition(corner[0], corner[1]);
                System.Console.Write('#');
            }
        }

        public static void DrawWidthArrows() {
            for (int y = 0; y < ScreenCharHeight; y++) {
                if (y % 5 == 0 || y == ScreenCharHeight - 1) {
                    System.Console.Write('<');
                    for (int x = 1; x < ScreenCharWidth - 1; x++) {
                        System.Console.Write('-');
                    }
                    System.Console.Write('>');
                }
                else {
                    System.Console.Write('|');
                    for (int x = 1; x < ScreenCharWidth - 1; x++)
                    {
                        System.Console.Write(' ');
                    }
                    System.Console.Write('|');
                }
                System.Console.Write('\n');
            }
        }

        public static int GetCenterX() {
            return System.Console.WindowWidth / 2;
        }
        public static int GetCenterY() {
            return System.Console.WindowHeight / 2;
        }

        public static void DrawBox(int width, int height, int customX = 0, int customY = 0
            , ConsoleColor borderColor = ConsoleColor.White, bool isCentred = true)
        {
            string topBorder = "╔" + new string('═', width) + "╗";
            string emptyLine = "║" + new string(' ', width) + "║";
            string bottomBorder = "╚" + new string('═', width) + "╝";

            int startingLocY = 0;
            int startingLocX = 0;

            System.Console.ForegroundColor = borderColor;


            if (ScreenCharHeight < height || ScreenCharWidth < width)  // checking incorrect constants
            {
                throw new Exception("Big screen error: The screen dimensions exceed allowable limits.");
            }

            if (isCentred)
            {
                startingLocY = RoomDefaultX;
                startingLocX = RoomDefaultY; 
            }
              


            // Move the cursor to the starting location
            System.Console.SetCursorPosition(startingLocX, startingLocY);

            // Draw top border
            System.Console.WriteLine(topBorder);

            // Draw side walls
            for (int i = 1; i < height; i++)
            {
                System.Console.SetCursorPosition(startingLocX, startingLocY + i);
                System.Console.WriteLine(emptyLine);
            }

            // Draw bottom border
            System.Console.SetCursorPosition(startingLocX, startingLocY + height);
            System.Console.WriteLine(bottomBorder);

            System.Console.ResetColor();
        }

        // public static void DrawMenuOption(string option) {
        //     int xOffset = System.Console.WindowWidth / 2 - option.Length / 2;
        //     if (xOffset < 0) xOffset = 0;
        //     System.Console.Write(new string(' ', xOffset));
        //     System.Console.WriteLine(option);
        // }

        /*
        void PlaceSymbol(int x, int y, char symbol)
        {
            if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
            {
                grid[x, y] = symbol; // Replace the symbol at given coordinates
            }
            else
            {
                Console.WriteLine("Coordinates out of bounds.");
            }
        }
        */


        public static void DrawPlayer(int x, int y)
        {
            GameObjectType type = GameObjectType.Player;

            int startingLocY = RoomDefaultX+1;  // skips room walls by spawning at +1,+1 coordinates
            int startingLocX = RoomDefaultY+1;

            x += startingLocX;
            y += startingLocY;


            System.Console.SetCursorPosition(x, y);
            System.Console.Write(textures[type]);

        }

        public static void DrawEnemy(int x, int y)
        {
            GameObjectType type = GameObjectType.Enemy;

            int startingLocY = RoomDefaultX + 1;  // skips room walls by spawning at +1,+1 coordinates
            int startingLocX = RoomDefaultY + 1;

            x += startingLocX;
            y += startingLocY;


            System.Console.SetCursorPosition(x, y);
            System.Console.Write(textures[type]);

        }

    }
}