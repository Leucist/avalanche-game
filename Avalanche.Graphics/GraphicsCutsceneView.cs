using System;
using System.Collections.Generic;
using System.IO;
using Avalanche.Core;
using Avalanche.Core.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace Avalanche.Graphics
{
    public class GraphicsCutsceneView : GraphicsView
    {
        // used to CENTER the image
        float _screenWidth = AppConstants.ScreenCharWidth * AppConstants.PixelWidthMultiplier;
        float _screenHeight = AppConstants.ScreenCharHeight * AppConstants.PixelHeightMultiplier;

        private readonly CutsceneModel _model;
        // private readonly List<TextureDataObject> _sceneTextures;


        private string _cutscenesFolderPath;
        public int _framesCount { get; set; }
        private int _prevFrameNumber;

        public GraphicsCutsceneView(CutsceneModel model, GraphicsRenderer renderer)
            : base(renderer)
        {
            _model = model;

            // _sceneTextures = new List<TextureDataObject>();

            // Set Cutscenes folder for the graphics
            string solutionPath = Pathfinder.FindSolutionDirectory();
            string graphicsFolder = "Avalanche.Graphics";
            _cutscenesFolderPath = Path.Combine(solutionPath, graphicsFolder);
            _prevFrameNumber = _model._currentFrameNumber - 1;
        }

        public override void Reset() {
            Renderer.ResetEventHandlers();
        }

        public override void Render()
        {
            // // Checks if there's nothing to Update
            // if (_prevFrameNumber == _model._currentFrameNumber) return;

            // // Clear the screen
            // Renderer.ClearWindow();

            // // Updates the indicator int
            // _prevFrameNumber = _model._currentFrameNumber;



            
            // Get path to the current frame
            string relativeFilePath = Pathfinder.GetFrameFilePath(
                _model._currentFrameNumber,
                (CutsceneType)_model._cutsceneNumber) + ".png";
                
            // Combine with the directory path
            string filePath = Path.Combine(_cutscenesFolderPath, relativeFilePath);

            // Actual reading art from the file and drawing
            try
            {
                // Load the frame
                Texture tex = new Texture(filePath);
                var texDataObj = new TextureDataObject(tex, new Vector2f(0, 0));

                Sprite sprite = texDataObj.Sprite;

                

                float texWidth = sprite.Texture.Size.X;
                float texHeight = sprite.Texture.Size.Y;

                // Scale so the image fits entirely on screen
                float scaleX = _screenWidth / texWidth;
                float scaleY = _screenHeight / texHeight;
                float finalScale = Math.Min(scaleX, scaleY);

                // Apply that scale
                sprite.Scale = new Vector2f(finalScale, finalScale);

                // Compute scaled width/height
                float scaledWidth = texWidth * finalScale;
                float scaledHeight = texHeight * finalScale;

                // Center it
                float offsetX = (_screenWidth - scaledWidth) * 0.5f;
                float offsetY = (_screenHeight - scaledHeight) * 0.5f;
                sprite.Position = new Vector2f(offsetX, offsetY);

                Renderer.Draw(sprite);
            }

            catch (Exception ex)
            {
               System.Console.WriteLine("Error reading the file: " + ex.Message);
            }
        }
    }
}
