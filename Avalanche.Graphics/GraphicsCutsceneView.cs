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
        private readonly CutsceneModel _model;
        private readonly List<TextureDataObject> _sceneTextures;

        public GraphicsCutsceneView(CutsceneModel model, GraphicsRenderer renderer)
            : base(renderer)
        {
            _model = model;

            _sceneTextures = new List<TextureDataObject>();

            string solutionPath = Pathfinder.FindSolutionDirectory();
            string cutsceneFolderPath = Path.Combine(
                solutionPath,
                "Avalanche.Graphics",
                "Cutscenes",
                "Cutscene_GameStart"
            );

            for (int i = 0; i <= 7; i++)
            {
                string filePath = Path.Combine(cutsceneFolderPath, $"{i}.png");
                if (File.Exists(filePath))
                {
                    Texture tex = new Texture(filePath);
                    // Position at (0,0); we'll re-center it when drawing
                    var texDataObj = new TextureDataObject(tex, new Vector2f(0, 0));
                    _sceneTextures.Add(texDataObj);
                }
            }

            Renderer.ResetEventHandlers();
        }

        public override void Render()
        {
            // which frame to display:
            int currentFrameIndex = _model._currentFrameNumber;

            // If img out of range
            if (currentFrameIndex < 0 || currentFrameIndex >= _sceneTextures.Count)
                return;

            Sprite sprite = _sceneTextures[currentFrameIndex].Sprite;

            // CENTER the image
            float screenWidth = AppConstants.ScreenCharWidth * AppConstants.PixelWidthMultiplier;
            float screenHeight = AppConstants.ScreenCharHeight * AppConstants.PixelHeightMultiplier;

            float texWidth = sprite.Texture.Size.X;
            float texHeight = sprite.Texture.Size.Y;

            // Scale so the image fits entirely on screen
            float scaleX = screenWidth / texWidth;
            float scaleY = screenHeight / texHeight;
            float finalScale = Math.Min(scaleX, scaleY);

            // Apply that scale
            sprite.Scale = new Vector2f(finalScale, finalScale);

            // Compute scaled width/height
            float scaledWidth = texWidth * finalScale;
            float scaledHeight = texHeight * finalScale;

            // Center it
            float offsetX = (screenWidth - scaledWidth) * 0.5f;
            float offsetY = (screenHeight - scaledHeight) * 0.5f;
            sprite.Position = new Vector2f(offsetX, offsetY);

            Renderer.Draw(sprite);
        }
    }
}
