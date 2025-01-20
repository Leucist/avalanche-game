using Avalanche.Core;
using Avalanche.Core.Interfaces;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;

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
        private const int MaxSymbols = 15; // TODO: move this to AppConstants
        private bool _backspaceHeld = false; // Track if backspace is currently being held

        private readonly Font _font;

        // Store the current user input
        private string _userInput = "";

        // Store the last warning message
        private string _lastWarning = "";

        // We'll manage the warning sign separately so we can control its visibility easily
        private Sprite _warningSignSprite;


        private float _warningFadeTimer = 0f;     
        private float _warningDuration = 5f;
        private bool _isFading = false;           
        private byte _warningAlpha = 255;

        public GraphicsNameInputView(NameInputModel model, GraphicsRenderer renderer) : base(renderer)
        {
            _model = model;

            _textureFolder = Pathfinder.GetGraphicsTexturesFolder();
            _texturePaths = new()
            {
                { TextureType.Background, "MainMenu_background.png" },
                { TextureType.WarningSign, "WarningSign.png" }
            };

            _sceneTextures = new List<TextureDataObject>
            {
                new TextureDataObject(GetTexture(TextureType.Background), new Vector2f(0, 0))
            };

            _warningSignSprite = new Sprite(GetTexture(TextureType.WarningSign));
        }

        public override void Reset() {
            Renderer.ResetEventHandlers();
        }

        public override void Render()
        {
            foreach (var textureData in _sceneTextures)
            {
                Renderer.Draw(textureData.Sprite);
            }
            UpdateWarningFade(); // handle alpha transitions

            Color fillColor;
            uint fontSize = 32;
            bool centered = true;
            bool outline = true;

            float optionsStartY = 150;

            // Title text
            Renderer.SetCursorAt(0, optionsStartY);
            string optionText = "Please, enter character name:";
            fillColor = new Color(0, 255, 255); // Light icy blue
            Renderer.SetFillColor(fillColor);
            Renderer.WriteLine(optionText, fontSize, centered, outline);

            // Rectangle for user text input
            float rectangleWidth = 500;
            float rectangleHeight = 50;
            float rectangleX = (AppConstants.ScreenCharWidth * AppConstants.PixelWidthMultiplier / 2) - (rectangleWidth / 2);
            float rectangleY = optionsStartY + 50;

            RectangleShape inputRectangle = new RectangleShape(new Vector2f(rectangleWidth, rectangleHeight))
            {
                Position = new Vector2f(rectangleX, rectangleY),
                FillColor = new Color(0, 100, 255, 180),        // Semi-transparent icy blue
                OutlineColor = new Color(135, 206, 250, 255),  // Light sky blue
                OutlineThickness = 4
            };
            Renderer.Draw(inputRectangle);

            // Handle keyboard input
            HandleInputMenu();

            // Render the user input text
            Renderer.SetCursorAt((int)(rectangleX + 10), (int)(rectangleY + 10));
            Renderer.SetFillColor(new Color(255, 255, 255, 255)); // White color for text
            Renderer.WriteLine(_userInput, fontSize, false, false);

            // Position the warning sign to the right of the rectangle
            float warningSignX = rectangleX + rectangleWidth + 20;
            float warningSignY = rectangleY;

            _warningSignSprite.Position = new Vector2f(warningSignX, warningSignY - 5);
            _warningSignSprite.Scale = new Vector2f(0.6f, 0.6f); // Reduce size to 60%

            // If there's a warning message, draw the warning sign and text with the current alpha
            if (!string.IsNullOrEmpty(_lastWarning))
            {
                // Apply the alpha to the sign
                _warningSignSprite.Color = new Color(255, 255, 255, _warningAlpha);
                Renderer.Draw(_warningSignSprite);

                // Draw the warning text in red with the current alpha
                Renderer.SetCursorAt((int)rectangleX, (int)(rectangleY + rectangleHeight + 30));
                Renderer.SetFillColor(new Color(255, 0, 0, _warningAlpha));
                Renderer.WriteLine(_lastWarning, 24, false, false);
            }
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

            foreach (Keyboard.Key key in currentlyPressedKeys)
            {
                if (key == Keyboard.Key.Backspace)
                {
                    // Handle backspace logic
                    if (!_previousKeys.Contains(key)) // First press
                    {
                        if (_userInput.Length > 0)
                        {
                            _userInput = _userInput.Substring(0, _userInput.Length - 1);
                        }
                        _backspaceClock.Restart();
                        _backspaceHeld = true;
                    }
                    else if (_backspaceHeld && _backspaceClock.ElapsedTime.AsSeconds() >= 0.15f)
                    {
                        if (_userInput.Length > 0)
                        {
                            _userInput = _userInput.Substring(0, _userInput.Length - 1);
                            _backspaceClock.Restart();
                        }
                    }
                }
                else if (!_previousKeys.Contains(key))
                {
                    char? character = KeyToChar(key);

                    if (character.HasValue)
                    {
                        // If within limit, add it
                        if (_userInput.Length < MaxSymbols)
                        {
                            _userInput += character.Value;

                            _lastWarning = "";
                            _isFading = false;
                            _warningFadeTimer = 0f;
                            _warningAlpha = 255;
                        }
                        else
                        {
                            // We reached the limit
                            ShowNewWarning("You have reached the maximum name size.");
                        }
                    }
                    else if (key == Keyboard.Key.Enter)
                    {
                        if (string.IsNullOrEmpty(_userInput))
                        {
                            ShowNewWarning("Your name field is empty.");
                        }
                        else
                        {
                            _model.Submit(_userInput);
                        }
                    }
                    else
                    {
                        if (IsCharacterKey(key))
                        {
                            ShowNewWarning("Please enter only alphanumeric characters or space.");
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
            // Convert Keyboard.Key to char (supports only alphanumeric + space)
            if (key >= Keyboard.Key.A && key <= Keyboard.Key.Z)
            {
                // A-Z
                return (char)('A' + (key - Keyboard.Key.A));
            }
            else if (key >= Keyboard.Key.Num0 && key <= Keyboard.Key.Num9)
            {
                // 0-9
                return (char)('0' + (key - Keyboard.Key.Num0));
            }
            else
            {
                switch (key)
                {
                    case Keyboard.Key.Space:
                        return ' ';
                    default:
                        return null; // Unsupported keys
                }
            }
        }

        private bool IsCharacterKey(Keyboard.Key key)
        {
            if ((key >= Keyboard.Key.A && key <= Keyboard.Key.Z)
                || (key >= Keyboard.Key.Num0 && key <= Keyboard.Key.Num9)
                || key == Keyboard.Key.Space)
            {
                return true;
            }
            return false;
        }

        private void ShowNewWarning(string message)
        {
            _lastWarning = message;
            _warningFadeTimer = 0f;
            _isFading = true;
            _warningAlpha = 255; // fully opaque at start
        }

        private void UpdateWarningFade()
        {
            if (!_isFading) return;

            float dt = AvalanchyFrameTime();

            _warningFadeTimer += dt;

            // We'll linearly fade from 255 to 0 over _warningDuration seconds
            float fraction = _warningFadeTimer / _warningDuration;
            if (fraction > 1f) fraction = 1f; // clamp

            // Convert fraction to alpha (255 -> 0)
            _warningAlpha = (byte)((1f - fraction) * 255f);

            // Once fully faded
            if (fraction >= 1f)
            {
                _lastWarning = "";
                _isFading = false;
            }
        }

        private Clock _frameClock = new Clock(); 
        private float AvalanchyFrameTime()
        {
            float elapsed = _frameClock.ElapsedTime.AsSeconds();
            _frameClock.Restart();
            return elapsed;
        }

        private void OnTextEntered(object sender, TextEventArgs e)
        {
            // If you use text events instead of KeyToChar, logic goes here.
            if (e.Unicode == "\b" && _userInput.Length > 0)
            {
                _userInput = _userInput[0..^1];
            }
            else if (e.Unicode == "\r") // Enter key (submit)
            {
                Console.WriteLine($"User entered: {_userInput}");
            }
            else
            {
                _userInput += e.Unicode;
            }
        }
    }
}
