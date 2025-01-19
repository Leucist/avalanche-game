﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.IO;

using Avalanche.Core;
using static Avalanche.Core.AppConstants;
using System;
using static System.Formats.Asn1.AsnWriter;

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

        float GraphicsShiftX = RoomDefaultX;
        float GraphicsShiftY = RoomDefaultX;

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
            DrawPlayer();
            DrawEnemy();
            DrawItems();
            DrawBox();
            DrawThrowingRocks();
            RenderDoors();
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
            int count = _model._player._mushrooms;
            if (count > totalItemsCount)
                count = totalItemsCount;

            if (count == 0)
            {
                var noneText = MakeText("X", 18, new Vector2f(xStart, yStart));
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
            int count = _model._player._rocks;
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

        private void DrawPlayer()
        {
            Texture heartTex = _textures[TextureType.Player];

            Sprite s = new Sprite(heartTex)
            {
                Position = new Vector2f((_model._player.GetX()+ RoomDefaultX) * PixelWidthMultiplier, 
                (_model._player.GetY()+ RoomDefaultY) * PixelHeightMultiplier)
            };
            s.Scale = new Vector2f(2f, 2f);
            _renderer.Draw(s);

        }
        private void DrawEnemy()
        {
            List<Enemy> skelet = _model._currentRoom._enemies;

            Texture skeletonTex = _textures[TextureType.Skeleton];

            foreach (Enemy enemy in skelet)
            {
                Sprite s = new Sprite(skeletonTex)
                {
                    Position = new Vector2f((enemy.GetX()+ RoomDefaultX)* PixelWidthMultiplier ,
                    (enemy.GetY() + RoomDefaultY )* PixelHeightMultiplier)
                };
                s.Scale = new Vector2f(2f, 2f);
                _renderer.Draw(s);
            }

        }

        private void DrawItems()
        {

            Texture itemTex;

            foreach (var item in _model._currentRoom!._items)
            {
                switch (item)
                {
                    case LayingRock:
                        itemTex = _textures[TextureType.Rock];

                        Sprite r = new Sprite(itemTex)
                        {
                            Position = new Vector2f((item.GetX() + RoomDefaultX) * PixelWidthMultiplier,
                            (item.GetY() + RoomDefaultY) * PixelHeightMultiplier)
                        };

                        r.Scale = new Vector2f(2f, 2f);
                        _renderer.Draw(r);
                        break;
                    case Mushroom:
                        itemTex = _textures[TextureType.Mushroom];

                        Sprite m = new Sprite(itemTex)
                        {
                            Position = new Vector2f((item.GetX() + RoomDefaultX) * PixelWidthMultiplier,
                            (item.GetY() + RoomDefaultY) * PixelHeightMultiplier)
                        };

                        m.Scale = new Vector2f(2f, 2f);
                        _renderer.Draw(m);
                        break;
                }
            }

        }

        private void DrawRoom()
        {
            List<Enemy> skelet = _model._currentRoom._enemies;

            Texture skeletonTex = _textures[TextureType.Skeleton];

            foreach (Enemy enemy in skelet)
            {
                Sprite s = new Sprite(skeletonTex)
                {
                    Position = new Vector2f((enemy.GetX() + RoomDefaultX) * PixelWidthMultiplier,
                    (enemy.GetY() + RoomDefaultY) * PixelHeightMultiplier)
                };
                s.Scale = new Vector2f(2f, 2f);
                _renderer.Draw(s);
            }

        }

        public void DrawBox(int width = RoomCharWidth, int height = RoomCharHeight, int customX = 0, int customY = 0, bool isCentred = true)
        {
            Texture wallTex = _textures[TextureType.Wall];
            Texture doorOpenTex = _textures[TextureType.DoorOpen];
            Texture doorClosedTex = _textures[TextureType.DoorClosed];
            Texture doorLevelExitTex = _textures[TextureType.DoorLevelExit];
            Sprite wallSprite = new Sprite(wallTex);

            float startingLocX = (customX + RoomDefaultX) * PixelWidthMultiplier;
            float startingLocY = (customY + RoomDefaultY) * PixelHeightMultiplier;

            if (isCentred)
            {
                startingLocX = RoomDefaultX * PixelWidthMultiplier;
                startingLocY = RoomDefaultY * PixelHeightMultiplier;
            }

            // Top border
            for (int i = 0; i <= width; i++)
            {
                wallSprite.Position = new Vector2f(startingLocX + i * wallTex.Size.X, startingLocY);
                _renderer.Draw(wallSprite);
            }

            // Side walls
            for (int i = 1; i <= height; i++)
            {
                // Left wall
                wallSprite.Position = new Vector2f(startingLocX, startingLocY + i * wallTex.Size.Y);
                _renderer.Draw(wallSprite);

                // Right wall
                wallSprite.Position = new Vector2f(startingLocX + (width * wallTex.Size.X), startingLocY + i * wallTex.Size.Y);
                _renderer.Draw(wallSprite);
            }

            // Bottom border
            for (int i = 0; i <= width; i++)
            {
                wallSprite.Position = new Vector2f(startingLocX + i * wallTex.Size.X, startingLocY + (height * wallTex.Size.Y));
                _renderer.Draw(wallSprite);
            }
        }

        private void DrawThrowingRocks()
        {
            List<Entity> otherEntities = _model._currentRoom._otherEntities;

            Texture otherEntityTex;

            foreach (Entity entity in otherEntities)
            {
                if (entity.GetType() == typeof(Rock))
                {
                    otherEntityTex = _textures[TextureType.Rock];
                    Sprite s = new Sprite(otherEntityTex)
                    {
                        Position = new Vector2f((entity.GetX() + RoomDefaultX) * PixelWidthMultiplier,
                        (entity.GetY() + RoomDefaultY) * PixelHeightMultiplier)
                    };

                    s.Scale = new Vector2f(2f, 2f);
                    _renderer.Draw(s);

                }

            }
        }

        private void RenderDoors()
        {
            Texture doorTex;
            foreach (var door in _model._currentRoom!._doors)
            {
                if (!door.Value._isClosed && !door.Value._isLevelExit)
                {
                    doorTex = _textures[TextureType.DoorOpen];
                }
                else if (door.Value._isClosed)
                {
                    doorTex = _textures[TextureType.DoorClosed];
                }
                else
                {
                    doorTex = _textures[TextureType.DoorLevelExit];
                }

                Sprite s = new Sprite(doorTex)
                {
                    Position = new Vector2f((door.Key.GetX() + RoomDefaultX) * PixelWidthMultiplier,
                    (door.Key.GetY() + RoomDefaultY) * PixelHeightMultiplier)
                };

                s.Scale = new Vector2f(2f, 2f);
                _renderer.Draw(s);

            }
        }       
    }
}
