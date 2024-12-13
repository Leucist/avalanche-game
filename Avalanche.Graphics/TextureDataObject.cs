using Avalanche.Core;
using SFML.Graphics;
using SFML.System;

namespace Avalanche.Graphics
{
    public class TextureDataObject
    {
        private Texture _texture;
        private BoundSprite _sprite;

        public BoundSprite Sprite => _sprite;

        public TextureDataObject(Texture texture, GameObject origin) {
            _texture = texture;
            _sprite = new (
                texture,
                () => new Vector2f(origin.GetX(), origin.GetY())
            );
        }

        public TextureDataObject(Texture texture, Vector2f constCoords) {
            _texture = texture;
            _sprite = new (
                texture,
                () => constCoords
            );
        }

        public void SetScale(float x, float y) {
            _sprite.SetScale(x, y);
        }
    }
}