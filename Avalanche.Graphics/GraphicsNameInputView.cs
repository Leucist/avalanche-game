using Avalanche.Core;
using SFML.Graphics;
using SFML.System;

namespace Avalanche.Graphics
{
    public class GraphicsNameInputView : GraphicsView
    {
        private readonly string _textureFolder;


        private readonly NameInputModel _model;
        private readonly Dictionary<TextureType, string> _texturePaths;
        private readonly List<TextureDataObject> _sceneTextures;

        private readonly Font _font;


        public GraphicsNameInputView(NameInputModel model, GraphicsRenderer renderer) : base(renderer) {
            _model = model;

            _textureFolder = Pathfinder.GetGraphicsTexturesFolder();
            _texturePaths = new() {
                {TextureType.Background, "MainMenu_background.png"}
            };

        }

        public override void Render() {

            Color fillColor;
            uint fontSize = 32;
            bool centered = true;
            bool outline = true;

            float optionsStartY = 150;

            Renderer.SetCursorAt(0, optionsStartY);

            string optionText = "Please, enter character name:";

            fillColor = new Color(68, 10, 80, 230);
            Renderer.SetFillColor(fillColor);
            Renderer.WriteLine(optionText, fontSize, centered, outline);

            float rectangleWidth = 400;
            float rectangleHeight = 50; 
            float rectangleX = (AppConstants.ScreenCharWidth * AppConstants.PixelWidthMultiplier / 2) - (rectangleWidth / 2);
            float rectangleY = optionsStartY + 50;

            // Create and configure the blue rectangle
            RectangleShape inputRectangle = new RectangleShape(new Vector2f(rectangleWidth, rectangleHeight));
            inputRectangle.Position = new Vector2f(rectangleX, rectangleY);
            inputRectangle.FillColor = new Color(0, 0, 255, 255); // Fully opaque blue

            // Draw the rectangle
            Renderer.Draw(inputRectangle);
        }
    }
}