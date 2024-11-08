using Avalanche.Core;

using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class ConsoleGameView : IGameView
    {

        public ConsoleGameView() {}

        public void Initialize() {
            // System.Console.SetWindowSize(ScreenCharWidth, ScreenCharHeight);
            // // System.Console.SetBufferSize(ScreenWidth, ScreenHeight);

            System.Console.Clear();
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

        public void Render() {

        }
        
    }
}