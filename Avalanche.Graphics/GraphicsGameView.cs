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
        private readonly GraphicsRenderer _renderer;
        private readonly Dictionary<GameStateType, IView> _views;
        private IView? _currentView;

        public GraphicsGameView()
        {
            // Create new window
            _window = new RenderWindow(
                new VideoMode(
                    ScreenCharWidth * PixelWidthMultiplier,
                    ScreenCharHeight * PixelHeightMultiplier
                ),
                AppName);
            // Set window icon
            Image icon = new Image(Pathfinder.GetIconPath());
            _window.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);
            
            _renderer = new GraphicsRenderer(_window);

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
                    view = new GraphicsMainMenuView((MainMenuModel)controller.GetModel(), _renderer);
                    break;
                case GameStateType.OptionsMenu:
                    view = new GraphicsOptionsView((OptionsModel)controller.GetModel(), _renderer);
                    break;
                case GameStateType.NameInput:
                    view = new GraphicsNameInputView((NameInputModel)controller.GetModel(), _renderer);
                    break;
                case GameStateType.Game:
                    view = new GraphicsLevelView((LevelModel)controller.GetModel(), _renderer);
                    break;
                case GameStateType.Cutscene:
                    view = new GraphicsCutsceneView((CutsceneModel)controller.GetModel(), _renderer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _views[gameState] = view;
        }

        public void Render()
        {
            _window.DispatchEvents();

            // if (GameState._state != GameStateType.Exit) {
            if (GameState._state != GameStateType.Exit && _window.IsOpen) {
                _window.Clear(Color.Black);
                
                if (_currentView != _views[GameState._state]) {
                    _currentView = _views[GameState._state];
                    _currentView.Reset();
                }
                _currentView.Render();

                _window.Display();
            }
        }

        public RenderWindow GetWindow() {
            return _window;
        }
    }
}