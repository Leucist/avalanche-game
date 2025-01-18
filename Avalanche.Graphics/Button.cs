using SFML.Graphics;

namespace Avalanche.Graphics
{
    public class Button : ClickableObject
    {
        private readonly Text _text;
        public Text Text => _text;
        public override FloatRect Bounds => _text.GetGlobalBounds();

        public Button(Text text, Action action) : base(text.GetGlobalBounds(), action) {
            _text = text;
        }
    }
}