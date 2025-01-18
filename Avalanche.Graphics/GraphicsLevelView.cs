using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.IO;

using Avalanche.Core;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Graphics
{
    public class GraphicsLevelView : GraphicsView
    {
        private readonly LevelModel _model;
        private readonly GraphicsRenderer _renderer;

        private readonly Font _font;

        // Paths to textures
        private readonly string _textureFolder;
        private readonly Dictionary<TextureType, string> _texturePaths;
        private readonly Dictionary<TextureType, Texture> _textures;


        private const int TileWidth = 32;
        private const int TileHeight = 32;

        public GraphicsLevelView(LevelModel model, GraphicsRenderer renderer) : base(renderer)
        {
            _model = model;
            _renderer = renderer;

            // Load a font
            _font = new Font(Path.Combine(
                Pathfinder.FindSolutionDirectory(),
                "Avalanche.Graphics/fonts/Cinzel/static/Cinzel-Bold.ttf"));

            // Texture folder path
            _textureFolder = Pathfinder.GetGraphicsTexturesFolder();

            _texturePaths = new Dictionary<TextureType, string>
            {
                {TextureType.Player,         "Player.png"},
                {TextureType.Skeleton,       "Skeleton.png"},
                {TextureType.Wall,           "Wall.png"},
                {TextureType.DoorClosed,     "DoorClosed.png"},
                {TextureType.DoorOpen,       "DoorOpen.png"},
                {TextureType.DoorLevelExit,  "DoorLevelExit.png"},
                {TextureType.Rock,           "Rock.png"},
                {TextureType.Mushroom,       "Mushroom.png"},
                {TextureType.Fire,           "Fire.png"},
                {TextureType.Heart,          "Heart.png"},
            };

            _textures = new Dictionary<TextureType, Texture>();
            foreach (var kv in _texturePaths)
            {
                _textures[kv.Key] = new Texture(Path.Combine(_textureFolder, kv.Value));
            }

            int totalWindowWidth = ScreenCharWidth * PixelWidthMultiplier;
            int totalWindowHeight = ScreenCharHeight * PixelHeightMultiplier;
        }

        public override void Render()
        {
            // Always draw everything, no change tracking
            DrawUI();
        }

        private void DrawUI()
        {
            float topRowY = 20f;    // vertical offset from top
            float startX = 10f;     // left margin
            float labelGap = 150f;  // space between label and icons
            float itemGap = 12f;    // space between icons (hearts/fires)
            int yCorLowText = 10;

            var hpLabel = MakeText("Health Points:", 18,
                                   new Vector2f(startX, topRowY));
            _renderer.Draw(hpLabel);

            float heartsX = startX + 150f;
            DrawHeartsUI(heartsX, topRowY+ yCorLowText, itemGap);

            int _heatImageOffsetFromLabel = 200;
            float heatLabelX = heartsX + _heatImageOffsetFromLabel;
            var heatLabel = MakeText("Heat:", 18,
                                     new Vector2f(heatLabelX, topRowY));
            _renderer.Draw(heatLabel);

            int _heatIconsImageOffsetFromLabel = 65;
            float fireX = heatLabelX + _heatIconsImageOffsetFromLabel;
            DrawHeatUI(fireX, topRowY+yCorLowText, itemGap);

            float levelLabelX = fireX + (10 * itemGap);
            DrawLevelAndRoomUI(levelLabelX, topRowY);

            float secondRowY = 70f;
            float secondRowX = startX;

            // Mushrooms label
            var mushLabel = MakeText("[X] Mushrooms:", 18,
                                     new Vector2f(secondRowX, secondRowY));
            _renderer.Draw(mushLabel);

            // Mushrooms icons / “X”
            int _mushroomImageOffsetFromLabel = 160;
            float mushIconsX = secondRowX + _mushroomImageOffsetFromLabel;
            DrawMushroomsUI(mushIconsX, secondRowY+ yCorLowText);

            // Rocks label
            int _rockImageOffset = 400;
            float rocksLabelX = mushIconsX + _rockImageOffset;
            var rocksLabel = MakeText("[R] Rocks:", 18,
                                      new Vector2f(rocksLabelX, secondRowY));
            _renderer.Draw(rocksLabel);

            // Rocks icons / “X”
            int _rockOffsetFromLabel = 105;
            float rocksIconsX = rocksLabelX + _rockOffsetFromLabel;
            DrawRocksUI(rocksIconsX, secondRowY + yCorLowText);
        }

        private void DrawHeartsUI(float xStart, float yStart, float gap)
        {
            int totalHeartsCount = 10;
            int heartsCount = _model._player._health / totalHeartsCount;
            heartsCount = 10;

            // Draw hearts
            Texture heartTex = _textures[TextureType.Heart];

            for (int i = 0; i < heartsCount; i++)
            {
                Sprite s = new Sprite(heartTex)
                {
                    Position = new Vector2f(xStart + i * gap, yStart - 4f)
                };

                s.Scale = new Vector2f(2f, 2f);

                _renderer.Draw(s);
            }

        }


        private void DrawHeatUI(float xStart, float yStart, float gap)
        {
            const int totalBarsCount = 10;
            int playerHeatBars = (int)(_model._player._heat / 1000);
            if (playerHeatBars > totalBarsCount)
                playerHeatBars = totalBarsCount;

            Texture fireTex = _textures[TextureType.Fire];
            for (int i = 0; i < playerHeatBars; i++)
            {
                Sprite s = new Sprite(fireTex)
                {
                    Position = new Vector2f(xStart + i * gap, yStart - 4f)                };

                s.Scale = new Vector2f(2f, 2f);

                _renderer.Draw(s);
            }
        }

        private void DrawLevelAndRoomUI(float xStart, float yStart)
        {
            string labelStr = $"Level: {_model.LevelNumber} | Room: {_model._currentRoomID}";
            var text = MakeText(labelStr, 18, new Vector2f(xStart, yStart));
            _renderer.Draw(text);
        }

        private void DrawMushroomsUI(float xStart, float yStart)
        {
            int totalItemsCount = 10;
            int count = 10;
            if (count > totalItemsCount)
                count = totalItemsCount;

            if (count == 0)
            {
                var noneText = MakeText("X", 18, new Vector2f(xStart, yStart));
                noneText.Color = Color.Red;
                _renderer.Draw(noneText);
            }
            else
            {
                Texture mushTex = _textures[TextureType.Mushroom];
                for (int i = 0; i < count; i++)
                {
                    Sprite s = new Sprite(mushTex)
                    {
                        Position = new Vector2f(xStart + i * 32f, yStart - 4f)
                    };

                    s.Scale = new Vector2f(2f, 2f);

                    _renderer.Draw(s);
                }
            }
        }

        private void DrawRocksUI(float xStart, float yStart)
        {
            int totalItemsCount = 10;
            int count = 10;
            if (count > totalItemsCount)
                count = totalItemsCount;

            if (count == 0)
            {
                var NoneText = MakeText("X", 18, new Vector2f(xStart, yStart));
                NoneText.Color = Color.Red;
                _renderer.Draw(NoneText);
            }
            else
            {
                Texture rockTex = _textures[TextureType.Rock];
                for (int i = 0; i < count; i++)
                {
                    Sprite s = new Sprite(rockTex)
                    {
                        Position = new Vector2f(xStart + i * 32f, yStart - 4f)
                    };
                    s.Scale = new Vector2f(3f, 3f);
                    _renderer.Draw(s);
                }
            }
        }

        private Text MakeText(string str, uint fontSize, Vector2f pos)
        {
            return new Text(str, _font, fontSize)
            {
                Position = pos,
                FillColor = Color.White
            };
        }

    }
}
