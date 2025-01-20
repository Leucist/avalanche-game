using SFML.Graphics;
using SFML.System;
using SFML.Window;

using Avalanche.Core;

namespace Avalanche.Graphics
{
    public class GraphicsRenderer
    {
        /// <summary>
        /// Inner class that represents the pseudo-cursor for the Renderer
        /// </summary>
        private class CursorPointer {
            private float _posX, _posY;
            private float _windowWidth;
            public Vector2f Position => new Vector2f(_posX, _posY);

            public CursorPointer(RenderWindow window) {
                _windowWidth = window.Size.X;
            }

            public void Move(float diffX, float diffY) {
                _posX += diffX;
                _posY += diffY;
            }

            public void SetPosition(float x, float y) {
                _posX = x;
                _posY = y;
            }

            public void CenterHorizontally() {
                _posX = _windowWidth / 2.0f;
            }

            public void NextLine(uint identation) {
                _posY += identation;
            }
        }


        // - GraphicsRenderer atributes
        private readonly RenderWindow _window;
        private readonly Dictionary<FontType, string> _fontPaths;
        private Font _font;
        private Color _fillColor;

        private CursorPointer _cursorPointer;

        // A new list of clickable elements, each with defined bounds and an associated action.
        private List<(FloatRect bounds, Action onClick)> _clickableElements = new();


        public Vector2f CursorPosition => _cursorPointer.Position;



        /// <summary>
        /// Renderer constructor
        /// </summary>
        /// <param name="window">RenderWindow object where content is being rendered</param>
        public GraphicsRenderer(RenderWindow window) {
            _window = window;
            
            _fontPaths = new () {
                {FontType.Variable, "Cinzel-VariableFont_wght.ttf"},
                {FontType.Regular, "static/Cinzel-Regular.ttf"},
                {FontType.Medium, "static/Cinzel-Medium.ttf"},
                {FontType.SemiBold, "static/Cinzel-SemiBold.ttf"},
                {FontType.Bold, "static/Cinzel-Bold.ttf"},
                {FontType.ExtraBold, "static/Cinzel-ExtraBold.ttf"},
                {FontType.Black, "static/Cinzel-Black.ttf"}
            };
            // Set Default font params
            _font = SetFont(FontType.Regular);
            _fillColor = Color.Black;

            _cursorPointer = new CursorPointer(_window);

            // Set mouse click listener and link with handler
            _window.MouseButtonPressed += HandleMouseClick;
        }

        public Font SetFont(FontType fontType) {
            _font = new Font(Path.Combine(
                Pathfinder.GetGraphicsFontsFolder(),
                _fontPaths[fontType]));
            return _font;
        }

        public void SetFillColor(Color color) {
            _fillColor = color;
        }

        public void WriteLine(string content, uint fontSize=12, bool centered=false, bool outline=false) {
            Text text = new Text(content, _font) {
                CharacterSize = fontSize,
                FillColor = _fillColor
            };

            if(centered) {
                FloatRect textBounds = text.GetLocalBounds();
                // text.Origin = new Vector2f(textBounds.Left + textBounds.Width / 2.0f, textBounds.Top + textBounds.Height / 2.0f);

                _cursorPointer.CenterHorizontally();
                _cursorPointer.Move(-(textBounds.Width / 2.0f), 0);
            }

            // Set text position according to the cursor pointer
            text.Position = _cursorPointer.Position;

            if (outline) {
                text.OutlineColor = new Color(0, 0, 0);
                text.OutlineThickness = 2;
            }
            
            _window.Draw(text);
            _cursorPointer.NextLine(fontSize);
        }

        public void SetCursorAt(float x, float y) {
            _cursorPointer.SetPosition(x, y);
        }

        public void Draw(Drawable drawable) {
            _window.Draw(drawable);
        }

        /// <summary>
        /// Clears all event handlers to prevent duplication.
        /// </summary>
        public void ResetEventHandlers() {
            _clickableElements.Clear();
        }

        /// <summary>
        /// Adds a clickable element with specified bounds and an associated action to be invoked on click.
        /// </summary>
        /// <param name="bounds">The rectangular area that defines the clickable element.</param>
        /// <param name="onClick">The action to be executed when the element is clicked.</param>
        public void AddClickableElement(FloatRect bounds, Action onClick) {
            _clickableElements.Add((bounds, onClick));
        }

        /// <summary>
        /// Handles mouse click events and invokes the corresponding action if a clickable element is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The mouse button event arguments containing information about the click.</param>
        private void HandleMouseClick(object? sender, MouseButtonEventArgs e) {
            if (e.Button == Mouse.Button.Left) {
                Vector2f mousePos = (Vector2f)Mouse.GetPosition(_window);

                foreach (var (bounds, action) in _clickableElements) {
                    if (bounds.Contains(mousePos)) {
                        action?.Invoke();
                        break;
                    }
                }
            }
        }
    }
}