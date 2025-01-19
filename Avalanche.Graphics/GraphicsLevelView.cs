using SFML.Graphics;
using SFML.System;

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

        float GraphicsShiftX = RoomDefaultX;
        float GraphicsShiftY = RoomDefaultX;

        float _uiTextShift = -8f;
        float _uiElementsShift = -6f;

        float StandartScale = 1f;


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
                {TextureType.DistinguishedFire, "DistinguishedFire.png" },
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
            DrawRoomBackground();
            // Always draw everything, no change tracking
            DrawUI();
            DrawPlayer();
            DrawEnemy();
            DrawItems();
            DrawBox();  // sigmaboy commit (cringe -_-)
            DrawThrowingRocks();
            RenderDoors();  // must stay after DrawBox
            RenderCampfires();
        }

        private void DrawRoomBackground()
        {
            Texture backgroundTex = _textures[TextureType.DistinguishedFire];

            // Початкова позиція фону
            float startX = RoomDefaultX * PixelWidthMultiplier;
            float startY = RoomDefaultY * PixelHeightMultiplier;

            // Масштаб текстури
            Sprite backgroundSprite = new Sprite(backgroundTex)
            {
                Scale = new Vector2f(StandartScale, StandartScale)
            };

            // Зменшуємо відстань між текстурами, застосовуючи коефіцієнт масштабу
            float tileWidth = backgroundTex.Size.X * StandartScale * 0.9f; // Зменшення ширини на 10%
            float tileHeight = backgroundTex.Size.Y * StandartScale * 0.9f; // Зменшення висоти на 10%

            // Заповнюємо кімнату текстурою
            for (int y = 0; y <= RoomCharHeight + 4; y++)
            {
                for (int x = 0; x <= RoomCharWidth; x++)
                {
                    backgroundSprite.Position = new Vector2f(startX + x * tileWidth, startY + y * tileHeight);
                    _renderer.Draw(backgroundSprite);
                }
            }
        }


        public void DrawUI()
        {
            float FirstRow = 70f;  // X
            float SecondRow = 420f;
            float ThirdRow = 770f;

            float FirstLine = 65f;  // Y
            float SecondLine = 430f;

            Vector2f heartsGroupPos = new Vector2f((FirstRow + SecondRow) / 2, SecondLine);
            Vector2f heatGroupPos = new Vector2f((SecondRow + ThirdRow) / 2, SecondLine);

            Vector2f mushroomGroupPos = new Vector2f(FirstRow, FirstLine);
            Vector2f rocksGroupPos = new Vector2f(SecondRow, FirstLine);
            Vector2f levelGroupPos = new Vector2f(ThirdRow, FirstLine);


            DrawHeartsGroup(heartsGroupPos);
            DrawHeatGroup(heatGroupPos);
            DrawLevelAndRoomUI(levelGroupPos);
            DrawMushroomsGroup(mushroomGroupPos);
            DrawRocksGroup(rocksGroupPos);
        }


        private void DrawHeartsGroup(Vector2f basePos)
        {
            // Draw the label
            Text hpLabel = MakeText("Health Points:", 18, basePos);
            _renderer.Draw(hpLabel);

            // Let's offset hearts from the label, e.g. 150px to the right.
            float heartsOffsetX = 150f;
            float heartsOffsetY = 10f; // slight downward offset so they align nicely
            float gapBetweenHearts = 12f;

            // Now draw the hearts themselves
            DrawHeartsUI(basePos.X + heartsOffsetX,
                         basePos.Y + heartsOffsetY,
                         gapBetweenHearts);
        }


        private void DrawHeatGroup(Vector2f basePos)
        {
            // Draw the label
            Text heatLabel = MakeText("Heat:", 18, basePos);
            _renderer.Draw(heatLabel);

            // Offset for the fire icons
            float fireOffsetX = 65f;
            float fireOffsetY = 10f;
            float gapBetweenFires = 12f;

            DrawHeatUI(basePos.X + fireOffsetX,
                       basePos.Y + fireOffsetY,
                       gapBetweenFires);
        }


        private void DrawMushroomsGroup(Vector2f basePos)
        {
            Text mushLabel = MakeText("[X] Mushrooms:", 18, basePos);
            _renderer.Draw(mushLabel);

            float iconOffsetX = 160f;
            float iconOffsetY = 10f;

            DrawMushroomsUI(basePos.X + iconOffsetX, basePos.Y + iconOffsetY);
        }


        private void DrawRocksGroup(Vector2f basePos)
        {
            Text rocksLabel = MakeText("[R] Rocks:", 18, basePos);
            _renderer.Draw(rocksLabel);

            float iconOffsetX = 105f;
            float iconOffsetY = 10f;

            DrawRocksUI(basePos.X + iconOffsetX, basePos.Y + iconOffsetY);
        }

        private void DrawLevelAndRoomUI(Vector2f basePos)
        {
            string labelStr = $"Level: {_model.LevelNumber} | Room: {_model._currentRoomID}";
            var text = MakeText(labelStr, 18, basePos);
            _renderer.Draw(text);
        }

        private void DrawHeartsUI(float xStart, float yStart, float gap)
        {
            int totalHeartsCount = 10;
            int heartsCount = _model._player._health / totalHeartsCount;

            Texture heartTex = _textures[TextureType.Heart];

            for (int i = 0; i < heartsCount; i++)
            {
                Sprite s = new Sprite(heartTex)
                {
                    Position = new Vector2f(xStart + i * gap, yStart + _uiElementsShift),
                    Scale = new Vector2f(2f, 2f)
                };
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
                    Position = new Vector2f(xStart + i * gap, yStart + _uiElementsShift),
                    Scale = new Vector2f(2f, 2f)
                };
                _renderer.Draw(s);
            }
        }


        private void DrawMushroomsUI(float xStart, float yStart)
        {
            int totalItemsCount = 10;
            int count = _model._player._mushrooms;
            if (count > totalItemsCount)
                count = totalItemsCount;

            if (count == 0)
            {
                var noneText = MakeText("X", 18, new Vector2f(xStart, yStart + _uiTextShift));
                noneText.FillColor = Color.Red;
                _renderer.Draw(noneText);
            }
            else
            {
                Texture mushTex = _textures[TextureType.Mushroom];
                for (int i = 0; i < count; i++)
                {
                    Sprite s = new Sprite(mushTex)
                    {
                        Position = new Vector2f(xStart + i * 32f, yStart - 4f),
                        Scale = new Vector2f(2f, 2f)
                    };
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
                var noneText = MakeText("X", 18, new Vector2f(xStart, yStart + _uiTextShift));
                noneText.FillColor = Color.Red;
                _renderer.Draw(noneText);
            }
            else
            {
                Texture rockTex = _textures[TextureType.Rock];
                for (int i = 0; i < count; i++)
                {
                    Sprite s = new Sprite(rockTex)
                    {
                        Position = new Vector2f(xStart + i * 32f, yStart - 6f),
                        Scale = new Vector2f(3f, 3f)
                    };
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
                Position = new Vector2f((_model._player.GetX() + RoomDefaultX) * PixelWidthMultiplier,
                (_model._player.GetY() + RoomDefaultY) * PixelHeightMultiplier)
            };
            s.Scale = new Vector2f(StandartScale, StandartScale);
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
                    Position = new Vector2f((enemy.GetX() + RoomDefaultX) * PixelWidthMultiplier,
                    (enemy.GetY() + RoomDefaultY) * PixelHeightMultiplier)
                };
                s.Scale = new Vector2f(StandartScale, StandartScale);
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

                        r.Scale = new Vector2f(StandartScale, StandartScale);
                        _renderer.Draw(r);
                        break;
                    case Mushroom:
                        itemTex = _textures[TextureType.Mushroom];

                        Sprite m = new Sprite(itemTex)
                        {
                            Position = new Vector2f((item.GetX() + RoomDefaultX) * PixelWidthMultiplier,
                            (item.GetY() + RoomDefaultY) * PixelHeightMultiplier)
                        };

                        m.Scale = new Vector2f(StandartScale, StandartScale);
                        _renderer.Draw(m);
                        break;
                }
            }

        }
        public void DrawBox(int width = RoomCharWidth, int height = RoomCharHeight, int customX = 0, int customY = 0, bool isCentred = true)
        {
            Texture wallTex = _textures[TextureType.Wall];
            Sprite wallSprite = new Sprite(wallTex);

            float startingLocX = (customX + RoomDefaultX) * PixelWidthMultiplier;
            float startingLocY = (customY + RoomDefaultY) * PixelHeightMultiplier;

            if (isCentred)
            {
                startingLocX = RoomDefaultX * PixelWidthMultiplier;
                startingLocY = RoomDefaultY * PixelHeightMultiplier;
            }

            // Коригування для уникнення зазорів
            float wallWidth = wallTex.Size.X * StandartScale;
            float wallHeight = wallTex.Size.Y * StandartScale;

            // Top border
            for (int i = 0; i <= width; i++)
            {
                wallSprite.Position = new Vector2f(startingLocX + i * wallWidth, startingLocY - wallHeight); // Легке зміщення вгору
                wallSprite.Scale = new Vector2f(StandartScale, StandartScale);
                _renderer.Draw(wallSprite);
            }

            // Side walls
            for (int i = 0; i <= height; i++)
            {
                // Left wall
                wallSprite.Position = new Vector2f(startingLocX - wallWidth, startingLocY + i * wallHeight); // Легке зміщення вліво
                wallSprite.Scale = new Vector2f(StandartScale, StandartScale);
                _renderer.Draw(wallSprite);

                // Right wall
                wallSprite.Position = new Vector2f(startingLocX + width * wallWidth, startingLocY + i * wallHeight);
                wallSprite.Scale = new Vector2f(StandartScale, StandartScale);
                _renderer.Draw(wallSprite);
            }

            // Bottom border
            for (int i = 0; i <= width; i++)
            {
                wallSprite.Position = new Vector2f(startingLocX + i * wallWidth, startingLocY + height * wallHeight);
                wallSprite.Scale = new Vector2f(StandartScale, StandartScale);
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

                    s.Scale = new Vector2f(StandartScale, StandartScale);
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

                s.Scale = new Vector2f(StandartScale, StandartScale);
                _renderer.Draw(s);

            }
        }

        private void RenderCampfires()
        {
            Campfire campfire = _model._currentRoom._campfire;
            if (campfire.IsBurning)
            {
                Texture skeletonTex = _textures[TextureType.Fire];

                Sprite s = new Sprite(skeletonTex)
                {
                    Position = new Vector2f((campfire.GetX() + RoomDefaultX) * PixelWidthMultiplier,
                    (campfire.GetY() + RoomDefaultY) * PixelHeightMultiplier)
                };
                s.Scale = new Vector2f(StandartScale, StandartScale);
                _renderer.Draw(s);
            }
            else
            {
                Texture skeletonTex = _textures[TextureType.DistinguishedFire];

                Sprite s = new Sprite(skeletonTex)
                {
                    Position = new Vector2f((campfire.GetX() + RoomDefaultX) * PixelWidthMultiplier,
                    (campfire.GetY() + RoomDefaultY) * PixelHeightMultiplier)
                };
                s.Scale = new Vector2f(StandartScale, StandartScale);
                _renderer.Draw(s);
            }
        }

    }
}
