using SFML.Graphics;
using SFML.System;

namespace Avalanche.Graphics
{
    public class BoundSprite(Texture texture, Func<Vector2f> positionGetter) : Sprite(texture)
    {
        // Lambda to get object position
        private readonly Func<Vector2f> _positionGetter = positionGetter;

        public void Update()
        {
            Position = _positionGetter();
        }

        public void SetScale(float x, float y) {
            Scale = new Vector2f(x, y);
        }

        public static BoundSprite CopySprite(Sprite sprite) {
            return new BoundSprite(sprite.Texture, () => sprite.Position);
        }
    }
}