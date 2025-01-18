using SFML.Graphics;
using SFML.System;
using Avalanche.Core;

namespace Avalanche.Graphics
{

    public class GraphicsMainMenuView : GraphicsView
    {
        private MainMenuModel _model;
        
        private readonly string _textureFolder;
        private readonly Dictionary<TextureType, string> _texturePaths;
        private readonly List<TextureDataObject> _sceneTextures;

        private readonly Font _font;

        private readonly List<Button> _menuButtons;
        

        public GraphicsMainMenuView(MainMenuModel model, GraphicsRenderer renderer) : base(renderer) {
            _model = model;

            _textureFolder = Pathfinder.GetGraphicsTexturesFolder();
            _texturePaths = new() {
                {TextureType.Background, "MainMenu_background.png"}
            };

            _sceneTextures = [
                new TextureDataObject(GetTexture(TextureType.Background), new Vector2f(0, 0))
            ];
            
            _font = new Font(Path.Combine(
                Pathfinder.FindSolutionDirectory(),
                "Avalanche.Graphics/fonts/Cinzel/static/Cinzel-Bold.ttf"));

            // - Generate menu buttons Text objects
            _menuButtons = new();
            GenerateMenuButtons();

            // - Make buttons clickable
            Renderer.ResetEventHandlers();
            SubscribeMenuButtons();
        }

        private string GetTexturePath(string textureFileName) {
            return Path.Combine(_textureFolder, textureFileName);
        }

        private Texture GetTexture(TextureType textureType) {
            return new Texture(GetTexturePath(_texturePaths[textureType]));
        }

        private void GenerateMenuButtons() {
            // text params
            uint fontSize = 32;
            // first button positioning
            float optionsStartX = AppConstants.ScreenCharWidth * AppConstants.PixelWidthMultiplier / 2 - 50;
            float optionsStartY = 150;

            // TODO May be centered as it was before, temp removal
            Renderer.SetCursorAt(optionsStartX, optionsStartY);

            for (int i = 0; i < _model._options.Count; i++) {
                // Extract the menu option label
                string buttonLabel = _model._options[i].Item1;

                // - Create the Text and Button objects
                Text buttonTextObject = new Text(buttonLabel, _font, fontSize);
                buttonTextObject.Position = new Vector2f(optionsStartX, optionsStartY);
                GameStateType relatedGS = _model._options[i].Item2;
                Button menuItem = new(
                    buttonTextObject,
                    () => { GameState._state = relatedGS; }
                );
                
                
                // move imaginary cursor to the next line
                optionsStartY += fontSize;

                // Add the ready-to-use button to the list
                _menuButtons.Add(menuItem);
            }
        }

        private void SubscribeMenuButtons() {
            foreach (Button menuButton in _menuButtons) {
                Renderer.AddClickableElement(menuButton.Bounds, menuButton.Action);
            }
        }

        public override void Render() {
            // Background
            foreach (var textureData in _sceneTextures) {
                Renderer.Draw(textureData.Sprite);
            }

            // Buttons
            for (int i = 0; i < _menuButtons.Count; i++)
            {
                var button = _menuButtons[i];

                button.Text.FillColor = (i == _model._currentIndex)
                    ? new Color(68, 10, 80, 230)    // Highlight the active button
                    : new Color(10, 40, 80, 220);   // Defailt colour

                Renderer.Draw(button.Text);
            }
        }
    }
}