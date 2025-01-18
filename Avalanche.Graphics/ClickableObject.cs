using SFML.Graphics;

namespace Avalanche.Graphics
{
    public class ClickableObject
    {
        private readonly FloatRect _rect;
        private readonly Action _action;

        public virtual FloatRect Bounds => _rect;
        public Action Action => _action;

        public ClickableObject(FloatRect rect, Action action) {
            _rect = rect;
            _action = action;
        }

        public void Click()
        {
            _action?.Invoke();
        }
    }
}