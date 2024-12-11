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

        public GraphicsMainMenuView(MainMenuModel model, RenderWindow window) {
            _model = model;
            _window = window;

            _textureFolder = Path.Combine(Pathfinder.FindSolutionDirectory(), "Avalanche.Graphics/textures");
            _texturePaths = new Dictionary<TextureType, string>() {
                {TextureType.Background, "MainMenu_background.png"}
            };
        }

        private string GetTexturePath(TextureType textureType) {
            return Path.Combine(_textureFolder, _texturePaths[textureType]);
        }

        private Texture GetTexture(TextureType textureType) {
            return new Texture(Path.Combine(_textureFolder, _texturePaths[textureType]));
            // return new Texture(GetTexturePath(textureType));
        }

        private void GatherTextures() {
            foreach (var texture in _texturePaths.Values) {

            }
        }

        public void Render() {
            Texture backgroundTexture = new Texture(GetTexturePath(TextureType.Background));

            Sprite backgroundSprite = new Sprite(backgroundTexture)
            {
                Position = new Vector2f(0, 0), // position
                // Scale = new Vector2f(0.5f, 0.5f)  // scale
            };
            _window.Draw(backgroundSprite);
        }
    }
}