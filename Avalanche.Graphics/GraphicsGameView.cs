using SFML.Graphics;
using SFML.System;
using SFML.Window;

using Avalanche.Core;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Graphics
{
    public class GraphicsGameView : IGameView
    {
        private readonly RenderWindow _window;
        private readonly Dictionary<GameStateType, IView> _views;

        public GraphicsGameView()
        {
            // Create new window
            _window = new RenderWindow(
                new VideoMode(
                    ScreenCharWidth * PixelWidthMultiplier,
                    ScreenCharHeight * PixelHeightMultiplier
                ),
                AppName);

            _views = new Dictionary<GameStateType, IView>();
        }

        public void Initialize()
        {
            _window.SetFramerateLimit(60);
            _window.Closed += (sender, args) => {
                GameState._state = GameStateType.Exit;
                _window.Close();
            };
        }

        public void AddView(GameStateType gameState, ISceneController controller)
        {
            IView view;
            switch (gameState)
            {
                case GameStateType.MainMenu:
                    view = new GraphicsMainMenuView((MainMenuModel)controller.GetModel(), _window);
                    break;
                // case GameStateType.Game:
                //     view = new GraphicsLevelView((LevelModel)controller.GetModel(), _window);
                //     break;
                // TODO + Other Views
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _views[gameState] = view;
        }

        public void Render()
        {
            _window.DispatchEvents();

            if (GameState._state != GameStateType.Exit) {
                _window.Clear(Color.Black);
                _views[GameState._state].Render();
                _window.Display();
            }
        }
    }
}