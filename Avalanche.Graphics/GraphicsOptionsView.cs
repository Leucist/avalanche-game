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
    public class GraphicsOptionsView : GraphicsView
    {
        private readonly string _textureFolder;
        private readonly OptionsModel _model;
        private readonly Dictionary<TextureType, string> _texturePaths;
        private readonly List<TextureDataObject> _sceneTextures;

        private readonly Font _font;

        // Constants for layout
        private const float RectangleWidth = 400f;
        private const float RectangleHeight = 550f;
        private const float RectangleOutlineThickness = 5f;

        public GraphicsOptionsView(OptionsModel model, GraphicsRenderer renderer) : base(renderer)
        {
            _model = model;

            _textureFolder = Pathfinder.GetGraphicsTexturesFolder();
            _texturePaths = new()
            {
                { TextureType.SecondBackground, "OptionsBackground.png" }
            };

            _sceneTextures = new List<TextureDataObject>
            {
                new TextureDataObject(GetTexture(TextureType.SecondBackground), new Vector2f(0f, 0f))
            };

            _font = new Font(Path.Combine(
                Pathfinder.FindSolutionDirectory(),
                "Avalanche.Graphics/fonts/Cinzel/static/Cinzel-Bold.ttf"));
        }

        public override void Reset()
        {
            Renderer.ResetEventHandlers();
        }

        public override void Render()
        {
            foreach (var textureData in _sceneTextures)
            {
                //textureData.Sprite.Position = new Vector2f(0f, 40f);
                //textureData.SetScale(0.65f*2.5f,0.56f * 2.5f);
                //Renderer.Draw(textureData.Sprite); 
            }

            DrawOptionsRectangle();
        }
        public DateTime _cursorMovedTime = DateTime.Now;

        private void DrawOptionsRectangle()
        {


            // Calculate center position
            float centerX = (AppConstants.ScreenCharWidth * AppConstants.PixelWidthMultiplier) / 2;
            float centerY = (AppConstants.ScreenCharHeight * AppConstants.PixelHeightMultiplier) / 2;

            // Draw the rectangle
            /*
            RectangleShape rectangle = new RectangleShape(new Vector2f(RectangleWidth, RectangleHeight))
            {
                Position = new Vector2f(centerX - RectangleWidth / 2, centerY - RectangleHeight / 2),
                FillColor = Color.White,
                OutlineColor = Color.Black,
                OutlineThickness = RectangleOutlineThickness
            };
            */

            //Renderer.Draw(rectangle);
                // Add title text
                Renderer.SetCursorAt((int)(centerX - 100), (int)(centerY - RectangleHeight / 2 + 20));
                Renderer.SetFillColor(Color.White);
                Renderer.WriteLine("OPTIONS", 46, true, true);

                // Add option labels
                float optionY = centerY - RectangleHeight / 2 + 100;
                Renderer.WriteLine(((DifficultyLevelType)_model.Difficulty).ToString(), 24, true, true);

                // Add footer labels
                string footer = "Escape to return to the main menu";
                Renderer.SetCursorAt((int)(centerX - 150), (int)(centerY + RectangleHeight / 2 - 40));
                Renderer.WriteLine(footer, 20, true, false);
                _cursorMovedTime = DateTime.Now;
        }

        private Texture GetTexture(TextureType textureType)
        {
            return new Texture(GetTexturePath(_texturePaths[textureType]));
        }

        private string GetTexturePath(string textureFileName)
        {
            return Path.Combine(_textureFolder, textureFileName);
        }
    }
}
