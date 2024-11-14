using Avalanche.Core;
using System.Drawing;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public static class ConsoleRenderer
    {
        private static int _startingLocY = RoomDefaultX + 1;  // skips room walls by spawning at +1,+1 coordinates
        private static int _startingLocX = RoomDefaultY + 1;

        public static Dictionary<GameObjectType, char> textures = new() {
            { GameObjectType.Wall, '█' },
            { GameObjectType.DoorClosed, 'Ξ' },
            { GameObjectType.DoorOpened, '▒' },
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

        public static void DrawExpandAlert()
        {
            string[] message =
            {
                "█████ █    █ █████ █████ ██  █ ████      █████ █   █ █████     █  █  █ ███ ██  █ ████  █████ █  █  █",
                "█      █  █  █   █ █   █ ██  █ █   █       █   █   █ █         █  █  █  █  ██  █ █   █ █   █ █  █  █",
                "█████   ██   █████ █████ █ █ █ █   █       █   █████ █████     █  █  █  █  █ █ █ █   █ █   █ █  █  █",
                "█      █  █  █     █   █ █  ██ █   █       █   █   █ █         █  █  █  █  █  ██ █   █ █   █ █  █  █",
                "█████ █    █ █     █   █ █  ██ ████        █   █   █ █████      █████  ███ █  ██ ████  █████  █████ "
            };

            int xPosition = (System.Console.WindowWidth - message[0].Length) / 2;
            if (xPosition < 0) xPosition = 0; 

            int yPosition = (System.Console.WindowHeight - message.Length) / 2;
            if (yPosition < 0) yPosition = 0; 

            foreach (string element in message)
            {
                System.Console.SetCursorPosition(xPosition, yPosition++);
                System.Console.WriteLine(element);
            }
        }


        public static void DrawCornerMarkers()
        {
            // Визначаємо координати чотирьох кутів
            int[][] corners = new int[][] {
                new int[] { 0, 0 }, // Верхній лівий
                new int[] { System.Console.WindowWidth - 1, 0 }, // Верхній правий
                new int[] { 0, System.Console.WindowHeight - 1 }, // Нижній лівий
                new int[] { System.Console.WindowWidth - 1, System.Console.WindowHeight - 1 } // Нижній правий
            };

            // Проходимо по всіх координатах і малюємо символ у кожному куті
            foreach (int[] corner in corners)
            {
                // Перевіряємо, чи координати не виходять за межі екрану
                if (corner[0] >= 0 && corner[0] < System.Console.WindowWidth &&
                    corner[1] >= 0 && corner[1] < System.Console.WindowHeight)
                {
                    System.Console.SetCursorPosition(corner[0], corner[1]);
                    System.Console.Write('#');
                }
            }
        }


        //public static void DrawWidthArrows() {
        //    for (int y = 0; y < ScreenCharHeight; y++) {
        //        if (y % 5 == 0 || y == ScreenCharHeight - 1) {
        //            System.Console.Write('<');
        //            for (int x = 1; x < ScreenCharWidth - 1; x++) {
        //                System.Console.Write('-');
        //            }
        //            System.Console.Write('>');
        //        }
        //        else {
        //            System.Console.Write('|');
        //            for (int x = 1; x < ScreenCharWidth - 1; x++)
        //            {
        //                System.Console.Write(' ');
        //            }
        //            System.Console.Write('|');
        //        }
        //        System.Console.Write('\n');
        //    }
        //}

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

        private static void DrawDoorOfType(int x, int y, GameObjectType type) {
            x += RoomDefaultX;
            y += RoomDefaultY;

            System.Console.SetCursorPosition(x, y);
            System.Console.Write(textures[type]);
        }
        public static void DrawDoor(int x, int y, bool isClosed) {
            GameObjectType type;
            if (isClosed)
                type = GameObjectType.DoorClosed;
            else
                type = GameObjectType.DoorOpened;
            DrawDoorOfType(x, y, type);
        }

        public static void ClearDirtyPixels(List<int[]> dirtyPixels) {
            int x, y;
            foreach (int[] pixel in dirtyPixels) {
                x = pixel[0] + _startingLocX;
                y = pixel[1] + _startingLocY;
                System.Console.SetCursorPosition(x, y);
                System.Console.Write(' ');
            }
        }

        public static void DrawGameObject(int x, int y, GameObjectType type) {
            x += _startingLocX;
            y += _startingLocY;

            System.Console.SetCursorPosition(x, y);
            System.Console.Write(textures[type]);
        }

        public static void DrawPlayer(int x, int y)
        {
            GameObjectType type = GameObjectType.Player;
            DrawGameObject(x, y, type);
        }

        public static void DrawEnemy(int x, int y)
        {
            GameObjectType type = GameObjectType.Enemy;
            DrawGameObject(x, y, type);
        }

    }
}