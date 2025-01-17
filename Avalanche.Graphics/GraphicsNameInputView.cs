using Avalanche.Core;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace Avalanche.Graphics
{
    public class GraphicsNameInputView : GraphicsView
    {
        private readonly string _textureFolder;

        private readonly NameInputModel _model;
        private readonly Dictionary<TextureType, string> _texturePaths;
        private readonly List<TextureDataObject> _sceneTextures;

        private readonly HashSet<Keyboard.Key> _previousKeys = new();
        private readonly Clock _backspaceClock = new Clock(); // Timer for backspace delay
        private const int MaxSymbols = 15; // TODO move this to AppConstants
        private bool _backspaceHeld = false; // Track if backspace is currently being held

        private readonly Font _font;

        private string _userInput = "";

        public GraphicsNameInputView(NameInputModel model, GraphicsRenderer renderer) : base(renderer)
        {
            _model = model;

            _textureFolder = Pathfinder.GetGraphicsTexturesFolder();
            _texturePaths = new() {
                {TextureType.Background, "MainMenu_background.png"}
            };
            _sceneTextures = [
                new TextureDataObject(GetTexture(TextureType.Background), new Vector2f(0, 0))
            ];

        }

        public override void Render()
        {
             foreach (var textureData in _sceneTextures) {
                 Renderer.Draw(textureData.Sprite);
             }

            // Set up the font and text appearance
            Color fillColor;
            uint fontSize = 32;
            bool centered = true;
            bool outline = true;

            float optionsStartY = 150;

            Renderer.SetCursorAt(0, optionsStartY);

            string optionText = "Please, enter character name:";

            fillColor = new Color(173, 216, 230, 230); // Light icy blue
            Renderer.SetFillColor(fillColor);
            Renderer.WriteLine(optionText, fontSize, centered, outline);

            float rectangleWidth = 500;
            float rectangleHeight = 50;
            float rectangleX = (AppConstants.ScreenCharWidth * AppConstants.PixelWidthMultiplier / 2) - (rectangleWidth / 2);
            float rectangleY = optionsStartY + 50;

            // Create and configure the glowing icy rectangle
            RectangleShape inputRectangle = new RectangleShape(new Vector2f(rectangleWidth, rectangleHeight));
            inputRectangle.Position = new Vector2f(rectangleX, rectangleY);
            inputRectangle.FillColor = new Color(0, 100, 255, 180); // Semi-transparent icy blue
            inputRectangle.OutlineColor = new Color(135, 206, 250, 255); // Glowing outline (light sky blue)
            inputRectangle.OutlineThickness = 4;

            // Draw the rectangle
            Renderer.Draw(inputRectangle);

            // Handle keyboard input
            HandleInputMenu();

            // Render the user input text
            Renderer.SetCursorAt((int)(rectangleX + 10), (int)(rectangleY + 10));
            Renderer.SetFillColor(new Color(255, 255, 255, 255)); // White color for text
            Renderer.WriteLine(_userInput, fontSize, false, false);
        }

        private Texture GetTexture(TextureType textureType)
        {
            return new Texture(GetTexturePath(_texturePaths[textureType]));
        }
        private string GetTexturePath(string textureFileName)
        {
            return Path.Combine(_textureFolder, textureFileName);
        }

        private void HandleInputMenu()
        {
            // Get all keys currently pressed
            HashSet<Keyboard.Key> currentlyPressedKeys = new();

            foreach (Keyboard.Key key in Enum.GetValues(typeof(Keyboard.Key)))
            {
                if (Keyboard.IsKeyPressed(key))
                {
                    currentlyPressedKeys.Add(key);
                }
            }

            // Process key releases (keys that were pressed before but not now)
            foreach (Keyboard.Key key in currentlyPressedKeys)
            {
                if (key == Keyboard.Key.Backspace)
                {
                    // Handle backspace logic
                    if (!_previousKeys.Contains(key)) // First press
                    {
                        if (_userInput.Length > 0)
                        {
                            _userInput = _userInput.Substring(0, _userInput.Length - 1); // Delete one character
                        }
                        _backspaceClock.Restart(); // Start the timer
                        _backspaceHeld = true; // Mark backspace as being held
                    }
                    else if (_backspaceHeld && _backspaceClock.ElapsedTime.AsSeconds() >= 0.3f) // Holding backspace
                    {
                        if (_userInput.Length > 0)
                        {
                            _userInput = _userInput.Substring(0, _userInput.Length - 1); // Delete another character
                            _backspaceClock.Restart(); // Restart the timer for the next deletion
                        }
                    }
                }
                else if (!_previousKeys.Contains(key)) // Other keys: add character on press
                {
                    if (_userInput.Length < MaxSymbols) // Check symbol limit
                    {
                        char? character = KeyToChar(key);
                        if (character.HasValue)
                        {
                            _userInput += character.Value; // Add character to input
                        }
                    }
                }
            }

            // Reset backspace held state if key is released
            if (!currentlyPressedKeys.Contains(Keyboard.Key.Backspace))
            {
                _backspaceHeld = false;
            }

            // Update the previous keys for the next frame
            _previousKeys.Clear();
            _previousKeys.UnionWith(currentlyPressedKeys);
        }

        private char? KeyToChar(Keyboard.Key key)
        {
            // Convert Keyboard.Key to char (supports only alphanumeric for simplicity)
            if (key >= Keyboard.Key.A && key <= Keyboard.Key.Z)
            {
                return (char)('A' + (key - Keyboard.Key.A)); // A-Z
            }
            else if (key >= Keyboard.Key.Num0 && key <= Keyboard.Key.Num9)
            {
                return (char)('0' + (key - Keyboard.Key.Num0)); // 0-9
            }
            else
            {
                switch (key)
                {
                    case Keyboard.Key.Space:
                        return ' '; // Spacebar
                    default:
                        return null; // Unsupported keys
                }
            }
        }

        private void OnTextEntered(object sender, TextEventArgs e)
        {
            // Check for special characters like backspace and newline
            if (e.Unicode == "\b" && _userInput.Length > 0) // Backspace
            {
                _userInput = _userInput[0..^1]; // Remove last character 
            }
            else if (e.Unicode == "\r") // Enter key (submit)
            {
                // Handle submission logic here (e.g., pass the input to the model)
                Console.WriteLine($"User entered: {_userInput}");
            }
            else
            {
                // Append valid input characters
                _userInput += e.Unicode;
            }
        }
    }
}