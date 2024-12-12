using SFML.Graphics;
using SFML.System;
using Avalanche.Core;
using SFML.Window;

namespace Avalanche.Graphics
{

    public class GraphicsMainMenuView : IView
    {
        private MainMenuModel _model;
        private readonly RenderWindow _window;
        
        private string _textureFolder;
        private Dictionary<TextureType, string> _texturePaths;
        private List<TextureDataObject> _sceneTextures;
        private Stack<BoundSprite> _spritesToDraw;

        private readonly Font _font;
        

        public GraphicsMainMenuView(MainMenuModel model, RenderWindow window) {
            _model = model;
            _window = window;

            _textureFolder = Path.Combine(Pathfinder.FindSolutionDirectory(), "Avalanche.Graphics/textures");
            _texturePaths = new() {
                {TextureType.Background, "MainMenu_background.png"},
                {TextureType.ButtonNewGame, "button.png"}
            };
            _spritesToDraw = new Stack<BoundSprite>();

            _sceneTextures = [
                new TextureDataObject(GetTexture(TextureType.Background), new Vector2f(0, 0))
            ];
            
            _font = new Font(Path.Combine(
                Pathfinder.FindSolutionDirectory(),
                "Avalanche.Graphics/fonts/Cinzel/static/Cinzel-Bold.ttf"));
        }

        private string GetTexturePath(string textureFileName) {
            return Path.Combine(_textureFolder, textureFileName);
        }

        private Texture GetTexture(TextureType textureType) {
            return new Texture(GetTexturePath(_texturePaths[textureType]));
        }

        // private void GatherSprites() {
        //     foreach (var textureFileName in _texturePaths.Values) {
        //         Texture texture = GetTexture(textureFileName);

        //         BoundSprite sprite = new (
        //             texture,
        //             () => new Vector2f())
        //         {
        //             Position = new Vector2f(0, 0), // position
        //             // Scale = new Vector2f(0.5f, 0.5f)  // scale
        //         };

        //         _spritesToDraw.Push(sprite);
        //     }
        // }

        // public void DrawSprites() {
        //     while(_spritesToDraw.Any()) {
        //         _window.Draw(_spritesToDraw.Pop());
        //     }
        // }

        public void Render() {
            foreach (var textureData in _sceneTextures) {
                _window.Draw(textureData.Sprite);
            }

            int i = 0;
            Color fillColor;
            foreach (var option in _model._options) {
                string label = option.Item1;
                if (i == _model._currentIndex) {
                    fillColor = new Color(68, 10, 80, 230);
                }
                else {
                    fillColor = new Color(10, 40, 80, 220);
                }

                Text menuOption = new Text(label, _font) {
                    CharacterSize = 36,
                    FillColor = fillColor,
                    OutlineColor = new Color(30, 50, 70),
                    OutlineThickness = 1,
                    Position = new Vector2f(
                        AppConstants.ScreenCharWidth * AppConstants.PixelWidthMultiplier / 2 - 100,
                        150 + i * 60)
                };

                _window.Draw(menuOption);
                i++;
            }
            
            // GatherSprites();
            // DrawSprites();
        }
    }
}