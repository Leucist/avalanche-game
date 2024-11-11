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
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.Black;

            ConsoleRenderer.ClearScreen();
            ConsoleRenderer.HideCursor();

            CheckWindowSize();
        }

        public void Render() {
            // ConsoleRenderer.ClearScreen();

            _views[GameState._state].Render();
        }
        
        public void AddView(GameStateType gameState, ISceneController controller) {
            IView view;
            switch (gameState) {
                case GameStateType.MainMenu:
                    view = new ConsoleMainMenuView((MainMenuModel)controller.GetModel());
                    break;
                case GameStateType.NameInput:
                    view = new ConsoleNameInputView((NameInputModel)controller.GetModel());
                    break;
                case GameStateType.Game:
                    view = new ConsoleLevelView((LevelModel)controller.GetModel());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _views[gameState] = view;
        }
        
        private void CheckWindowSize() {
            (int, int) screenSize = (0, 0);
            (int, int) prevScreenSize = screenSize;
            while ((screenSize.Item1 < ScreenCharWidth) || (screenSize.Item2 < ScreenCharHeight))
            {
                screenSize = (System.Console.WindowWidth, System.Console.WindowHeight);
                if (screenSize != prevScreenSize) {
                    ConsoleRenderer.ClearScreen();
                    ConsoleRenderer.DrawWidthArrows();
                    ConsoleRenderer.DrawAlert("PLEASE EXPAND THIS WINDOW or change the font size so that you'll see unwrapped one-line arrows");
                    prevScreenSize = screenSize;
                }
            }
            ConsoleRenderer.ClearScreen();
        }
    }
}