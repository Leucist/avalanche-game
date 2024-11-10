using Avalanche.Core;
using System.Runtime.InteropServices;

using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class ConsoleGameView : IGameView
    {
        // private IGameSceneModel _gameSceneModel;
        private Dictionary<GameStateType, IView> _views;

        public ConsoleGameView() {
            _views = new Dictionary<GameStateType, IView>();
            // _views[GameStateType.MainMenu] = new ConsoleMainMenuView();
        }

        public void Initialize() {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.Black;

            ConsoleRenderer.ClearScreen();
            ConsoleRenderer.HideCursor();

            CheckWindowSize();

            ConsoleRenderer.ShowCursor();
        }

        public void Render() {
            ViewManager._views[GameState._state].Render();
        }
        
        private void CheckWindowSize() {
            (int, int) screenSize = (0, 0);
            (int, int) prevScreenSize = screenSize;
            while ((screenSize.Item1 < ScreenCharWidth) && 
                (screenSize.Item2 < ScreenCharHeight))
            {
                screenSize = (System.Console.WindowWidth, System.Console.WindowHeight);
                if (screenSize != prevScreenSize) {
                    ConsoleRenderer.ClearScreen();
                    // ConsoleRenderer.Alert("PLEASE EXPAND THIS WINDOW");
                    ConsoleRenderer.Alert(screenSize.ToString());
                    prevScreenSize = screenSize;
                }
            }
        }
    }
}