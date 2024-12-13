using SFML.Graphics;
using SFML.System;
using Avalanche.Core;
using SFML.Window;

namespace Avalanche.Graphics
{

    public class GraphicsMainMenuView : GraphicsView
    {
        private MainMenuModel _model;
        
        private readonly string _textureFolder;
        private readonly Dictionary<TextureType, string> _texturePaths;
        private readonly List<TextureDataObject> _sceneTextures;

        private readonly Font _font;
        

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
        }

        private string GetTexturePath(string textureFileName) {
            return Path.Combine(_textureFolder, textureFileName);
        }

        private Texture GetTexture(TextureType textureType) {
            return new Texture(GetTexturePath(_texturePaths[textureType]));
        }

        public override void Render() {
            // foreach (var textureData in _sceneTextures) {
            //     Renderer.Draw(textureData.Sprite);
            // }

                       
            Color fillColor;
            uint fontSize = 32;
            bool centered = true;
            bool outline = true;

            // float optionsStartX = AppConstants.ScreenCharWidth * AppConstants.PixelWidthMultiplier / 2 - 100;
            float optionsStartY = 150;

            Renderer.SetCursorAt(0, optionsStartY);

            for (int i = 0; i < _model._options.Count; i++) {
                string optionText = _model._options[i].Item1;

                // Chose the text line color based on the current option selection
                fillColor = (i == _model._currentIndex)
                    ? new Color(68, 10, 80, 230)
                    : new Color(10, 40, 80, 220);

                Renderer.SetFillColor(fillColor);
                Renderer.WriteLine(optionText, fontSize, centered, outline);
            }
        }
    }
}