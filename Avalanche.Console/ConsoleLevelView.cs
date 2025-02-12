﻿using Avalanche.Core;
using System.Runtime.CompilerServices;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class ConsoleLevelView : IView
    {
        private readonly LevelModel _model;
        private bool _wasNeverDrawn;
        private int _previousHealthPointsCount;
        private int _previousHeat;
        private int _previousMushrooms;
        private int _previousRocks;
        private int _previousRoomID;
        private int _previousLevelNumber;

        public ConsoleLevelView(LevelModel model)
        {
            _model = model;
            ResetInnerParams();
        }

        private void ResetInnerParams() {
            _wasNeverDrawn = true;

            _previousHealthPointsCount = -100;
            _previousHeat = -1000;
            _previousMushrooms = -100;
            _previousRocks = -100;

            _previousRoomID = _model._currentRoomID - 1;
            _previousLevelNumber = _model.LevelNumber - 1;
        }

        public void Reset() {
            ResetInnerParams();
        }

        public void DrawHPUI()
        {
            // if ((_model._player._health != _previousHealthPointsCount) || _wasNeverDrawn)
            // {
            //System.Console.WriteLine($"Player's HP: {_model._player._health}");
                //System.Console.WriteLine($"Player's Heat: {_model._player._heat}");
                string label = "Health Points: ";

                int uiX = RoomDefaultX + 4;
                int uiY = RoomDefaultY - 3;

                int totalHeartsCount = 10;
                int heartsCount = _model._player._health / totalHeartsCount;

                // Clear previous data
                System.Console.SetCursorPosition(uiX + label.Length, uiY);
                System.Console.Write(" ", totalHeartsCount * 2);

                // Resets the cursor
                System.Console.SetCursorPosition(uiX, uiY);
                // Draw label:
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write(label);
                // Draw Hearts
                for (int i = 0; i < totalHeartsCount; i++)
                {   
                    if (heartsCount > i) {
                        System.Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else {
                        System.Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    System.Console.Write("♥ ");
                }
                System.Console.ForegroundColor = ConsoleColor.White;

                // _previousHealthPointsCount = _model._player._health;
            // }
        }

        public void DrawHeatUI()
        {
            // if(_model._player._heat != _previousHeat
            //     || _wasNeverDrawn)
            // {
                string label = "Heat: ";

                int uiX = RoomDefaultX + 50;
                int uiY = RoomDefaultY - 3;

                int totalBarsCount = 10;
                int playerHeatBars = (int)(_model._player._heat / 1000);
                if( playerHeatBars > 10)
                {
                    playerHeatBars = 10;
                }

                // Clear previous data
                System.Console.SetCursorPosition(uiX + label.Length, uiY);
                System.Console.Write(" ", totalBarsCount * 2);

                // Resets the cursor
                System.Console.SetCursorPosition(uiX, uiY);
                // Draw label:
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write(label);
                // Draw Bars
                for (int i = 0; i < totalBarsCount; i++)
                {   
                    if (playerHeatBars > i) {
                        System.Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else {
                        System.Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    System.Console.Write("█ ");
                }
                System.Console.ForegroundColor = ConsoleColor.White;

            //     _previousHeat = _model._player._heat;
            // }
        }

        public void DrawMushroomsUI()
        {
            // if (_model._player._mushrooms != _previousMushrooms
            //     || _wasNeverDrawn)
            // {
                string label = "[X] Mushrooms: ";

                int uiX = RoomDefaultX + 4;
                int uiY = RoomDefaultY + RoomCharHeight + 1;

                int totalItemsCount = 10;
                int playerItemsCount = _model._player._mushrooms;

                if( playerItemsCount > 10)
                {
                    playerItemsCount = 10;
                }

                // Clear previous data
                System.Console.SetCursorPosition(uiX + label.Length, uiY);
                System.Console.Write(new string(' ', label.Length + totalItemsCount * 2 + 2)); ;

                // Resets the cursor
                System.Console.SetCursorPosition(uiX, uiY);
                // Draw label:
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write(label);

                if (playerItemsCount == 0)
                {
                    System.Console.Write("✕");
                }

                for (int i = 0; i < playerItemsCount && i < totalItemsCount; i++)
                {
                    System.Console.Write("🍄 ");
                }

                System.Console.ForegroundColor = ConsoleColor.White;

                _previousMushrooms = _model._player._mushrooms;
            // }
        }

        public void DrawRocksUI()
        {
            // if (_model._player._rocks != _previousRocks
            //     || _wasNeverDrawn)
            // {
                string label = "[R] Rocks: ";

                int uiX = RoomDefaultX + 50;
                int uiY = RoomDefaultY + RoomCharHeight + 1;

                int totalItemsCount = 10;
                int playerItemsCount = _model._player._rocks;

                if( playerItemsCount > 10)
                {
                    playerItemsCount = 10;
                }

                // Clear previous data
                System.Console.SetCursorPosition(uiX + label.Length, uiY);
                System.Console.Write(new string(' ', label.Length + totalItemsCount * 2));

                // Resets the cursor
                System.Console.SetCursorPosition(uiX, uiY);
                // Draw label:
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write(label);

                if (playerItemsCount == 0)
                {
                    System.Console.Write("✕");
                }

                for (int i = 0; i < playerItemsCount && i < totalItemsCount; i++)
                {
                    System.Console.Write("🪨 ");
                }

                System.Console.ForegroundColor = ConsoleColor.White;

                _previousRocks = _model._player._rocks;
            // }
        }

        public void DrawRoomAndLevelNumberUI()
        {
            string levelLabel = "Level: ";
            string roomLabel = "Room: ";

            int labelWidth = levelLabel.Length + roomLabel.Length + 6;

            int uiX = RoomDefaultX + RoomCharWidth - labelWidth;
            int uiY = RoomDefaultY - 3;

            // Clear previous data
            System.Console.SetCursorPosition(uiX, uiY);
            System.Console.Write(" ", labelWidth);

            // Resets the cursor
            System.Console.SetCursorPosition(uiX, uiY);
            System.Console.ForegroundColor = ConsoleColor.White;
            // Draw label:
            System.Console.Write(levelLabel);
            System.Console.Write(_model.LevelNumber);
            System.Console.Write(" | ");
            System.Console.Write(roomLabel);
            System.Console.Write(_model._currentRoomID);
        }

        public void DrawFirecamp()
        {
            if (_model._currentRoom!.Campfire != null)
            {
                Campfire campfire = _model._currentRoom.Campfire;
                if (campfire.IsBurning)
                { 
                ConsoleRenderer.DrawFirecampSymbol(campfire.GetX(), campfire.GetY(), "🔥");
                }
                else
                {
                    ConsoleRenderer.DrawFirecampSymbol(campfire.GetX(), campfire.GetY(), "⚫ ");
                }
            }
        }

        private void DrawUI() {
            DrawHPUI();
            DrawHeatUI();
            DrawRoomAndLevelNumberUI();
            DrawMushroomsUI();
            DrawRocksUI();
            DrawFirecamp();
        }

        public void Render()
        {
            if (_model.IsPaused)
            {
                if (!_wasNeverDrawn)
                {
                    ConsoleRenderer.DrawPauseAlert();
                    _wasNeverDrawn = true;
                }
                return;
            }

            DrawUI();

            if (_previousRoomID != _model._currentRoomID || _model.LevelNumber != _previousLevelNumber)
            {
                _wasNeverDrawn = true;
                _previousRoomID = _model._currentRoomID;
                _previousLevelNumber = _model.LevelNumber;
            }

            if (_wasNeverDrawn)
            {
                ConsoleRenderer.ClearScreen();
                ConsoleRenderer.DrawBox(RoomCharWidth, RoomCharHeight);

                RenderDoors();

                foreach (var item in _model._currentRoom!.Items)
                {
                    // System.Console.WriteLine($"Rendering item at ({item.GetX()}, {item.GetY()}) of type {item.GetType().Name}");
                    GameObjectType gameObjectType = item is LayingRock ? GameObjectType.Rock : GameObjectType.Mushroom;
                    ConsoleRenderer.DrawGameObject(item.GetX(), item.GetY(), gameObjectType);
                }

                _wasNeverDrawn = false;
            }

            ConsoleRenderer.ClearDirtyPixels(_model._currentRoom!.GetDirtyPixels());
            _model._currentRoom.ClearDirtyPixels();

            if (_model._currentRoom.IsDirty())
            {
                RenderDoors();

                ConsoleRenderer.DrawPlayer(
                    _model._player.GetX(),
                    _model._player.GetY());

                foreach (var enemy in _model._currentRoom.Enemies)
                {
                    ConsoleRenderer.DrawEnemy(enemy.GetX(), enemy.GetY());
                }

                foreach (var entity in _model._currentRoom.OtherEntities)
                {
                    ConsoleRenderer.DrawGameObject(entity.GetX(), entity.GetY(), GameObjectType.Rock);
                }

                _model._currentRoom.MarkClean();
            }

            _previousHealthPointsCount = _model._player._health;
            _previousHeat = _model._player._heat;
            _previousMushrooms = _model._player._mushrooms;
            _previousRocks = _model._player._rocks;
        }



        private void RenderDoors()
        {
            foreach (var door in _model._currentRoom!.Doors)
            {
                // ConsoleRenderer.DrawDoor(door.Key.GetX(), door.Key.GetY(), door.Value._isLevelExit);
                ConsoleRenderer.DrawDoor(door.Key.GetX(), door.Key.GetY(), door.Value._isClosed, door.Value._isLevelExit);
            }
        }
    }
}