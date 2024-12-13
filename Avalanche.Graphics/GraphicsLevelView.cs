using SFML.Graphics;
using SFML.System;
using SFML.Window;

using Avalanche.Core;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Graphics
{

    public class GraphicsLevelView : GraphicsView
    {
        private readonly LevelModel _model;
        
        private string _textureFolder;
        private Dictionary<TextureType, string> _texturePaths;
        private List<TextureDataObject> _sceneTextures;
        private Stack<BoundSprite> _spritesToDraw;

        private readonly Font _font;

        private int _roomStartPointX;
        private int _roomStartPointY;
        

        public GraphicsLevelView(LevelModel model, GraphicsRenderer renderer) : base(renderer) {
            _model = model;

            _textureFolder = Pathfinder.GetGraphicsTexturesFolder();
            _texturePaths = new() {
                // {TextureType.Background, "Level_background.png"},
                {TextureType.Player, "Player.png"},
                {TextureType.Skeleton, "Skeleton.png"},
                {TextureType.Wall, "Wall.png"},
                {TextureType.DoorClosed, "DoorClosed.png"},
                {TextureType.DoorOpen, "DoorOpen.png"},
                {TextureType.DoorLevelExit, "DoorLevelExit.png"},
                {TextureType.Rock, "Rock.png"},
                {TextureType.Mushroom, "Mushroom.png"},
                {TextureType.Fire, "Fire.png"},
                {TextureType.Heart, "Heart.png"},
            };
            _spritesToDraw = new Stack<BoundSprite>();


            _sceneTextures = [
                // new TextureDataObject(GetTexture(TextureType.Background), new Vector2f(0, 0)),
                new TextureDataObject(GetTexture(TextureType.Player), _model._player)
            ];
            // Add enemies
            foreach (var enemy in _model._currentRoom!._enemies) {
                _sceneTextures.Add(new TextureDataObject(GetTexture(TextureType.Skeleton), enemy));
            }
            // Add items
            foreach (var item in _model._currentRoom!._otherEntities) {
                _sceneTextures.Add(new TextureDataObject(GetTexture(TextureType.Skeleton), item));
            }
            
            _font = new Font(Path.Combine(
                Pathfinder.FindSolutionDirectory(),
                "Avalanche.Graphics/fonts/Cinzel/static/Cinzel-Bold.ttf"));
            
            _roomStartPointX = (ScreenCharWidth * PixelWidthMultiplier - RoomCharWidth) / 2;
            _roomStartPointY = (ScreenCharHeight * PixelHeightMultiplier - RoomCharHeight) / 2;
        }

        private string GetTexturePath(string textureFileName) {
            return Path.Combine(_textureFolder, textureFileName);
        }

        private Texture GetTexture(TextureType textureType) {
            return new Texture(GetTexturePath(_texturePaths[textureType]));
        }

        // private void GatherSprites() {
        //     foreach (var textureFileName in _texturePaths.Values) {
        //         Texture texture = GetTexture(textureFileName);

        //         BoundSprite sprite = new (
        //             texture,
        //             () => new Vector2f())
        //         {
        //             Position = new Vector2f(0, 0), // position
        //             // Scale = new Vector2f(0.5f, 0.5f)  // scale
        //         };

        //         _spritesToDraw.Push(sprite);
        //     }
        // }

        // public void DrawSprites() {
        //     while(_spritesToDraw.Any()) {
        //         _window.Draw(_spritesToDraw.Pop());
        //     }
        // }

        private void DrawHealthUI() {
            int heartsMaxCount = 10;
            int playerHP = _model._player._health;
            int heartsCount = playerHP / heartsMaxCount;

            Vector2f position = new Vector2f(10, 10);
            Texture texture = GetTexture(TextureType.Heart);
            // List<Sprite> heart
            for (int i = 0; i < heartsCount; i++) {

            }
            
            
        }

        private void DrawUI() {
            DrawHealthUI();
            // DrawHeatUI();
            // DrawMushroomsUI();
            // DrawRocksUI();
        }

        private void DrawRoom() {
            for (int y = 0; y < RoomCharHeight; y++) {
                for (int x = 0; x < RoomCharWidth; x++) {
                    
                }
            }
        }

        public override void Render() {
            foreach (var textureData in _sceneTextures) {
                Renderer.Draw(textureData.Sprite);
            }

            DrawUI();
            DrawRoom();
            
            // GatherSprites();
            // DrawSprites();
        }
    }
}