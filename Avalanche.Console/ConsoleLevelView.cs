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

        public ConsoleLevelView(LevelModel model)
        {
            _model = model;
            _previousHealthPointsCount = _model._player._health;
            _wasNeverDrawn = true;
            _previousHeat = _model._player._heat;
        }

        public void DrawHP()
        {

            if (_model._player._health != _previousHealthPointsCount)
            {

                _previousHealthPointsCount = _model._player._health;
            }


            System.Console.SetCursorPosition(6, 9);
            System.Console.Write("Health Points: ");
            System.Console.ForegroundColor = ConsoleColor.Red;
            //System.Console.WriteLine(new string('♥️', _model._player._health));
            if (_model._player._health > 10)
            {
                _model._player._health = 10;
            }

            for (int i = 0; i < _model._player._health; i++)
            {
                System.Console.Write("♥ ");
            }

            if (_previousHealthPointsCount < 10)
                for (int j = _previousHealthPointsCount; j < 10; j++)
                {
                    System.Console.ForegroundColor = ConsoleColor.DarkGray;
                    System.Console.Write("♥ ");
                }


            System.Console.ForegroundColor = ConsoleColor.White;

        }

        public void DrawHeat()
        {
            if(_model._player._heat != _previousHeat)
            {
                _previousHeat = _model._player._heat;
            }

            System.Console.SetCursorPosition(50, 9);
            System.Console.Write("Heat: ");
            System.Console.ForegroundColor = ConsoleColor.DarkYellow;

            int playerHeatView = (int)(_model._player._heat / 100);

            if( playerHeatView > 10)
            {
                playerHeatView = 10;
            }

            for (int i = 0; i < playerHeatView; i++)
            {
                System.Console.Write("█ ");
            }

            if (playerHeatView < 10)
            for (int j = playerHeatView; j < 10; j++)
            {
                System.Console.ForegroundColor = ConsoleColor.DarkGray;
                System.Console.Write("█ ");
            }

            System.Console.ForegroundColor = ConsoleColor.White;

        }

        public void Render()
        {
            if (_wasNeverDrawn)
            {
                // Clear screen and draw room borders
                ConsoleRenderer.ClearScreen();
                DrawHP();
                DrawHeat();
                ConsoleRenderer.DrawBox(RoomCharWidth, RoomCharHeight);
                _wasNeverDrawn = false;
                RenderDoors();
            }

            ConsoleRenderer.ClearDirtyPixels(_model._currentRoom!._dirtyPixels);
            _model._currentRoom!._dirtyPixels.Clear();

            if (_model._currentRoom!._isDirty)
            {
                RenderDoors();
                // Render Player
                ConsoleRenderer.DrawPlayer(
                    _model._player.GetX(),
                    _model._player.GetY());

                // Render each of the enemies
                List<Entity> enemies = _model._currentRoom._enemies;
                foreach (Entity enemy in enemies)
                {
                    ConsoleRenderer.DrawEnemy(enemy.GetX(), enemy.GetY());
                }
                _model._currentRoom._isDirty = false;
            }
        }

        private void RenderDoors()
        {
            foreach (var door in _model._currentRoom!._doors)
            {
                ConsoleRenderer.DrawDoor(door.Key.GetX(), door.Key.GetY(), door.Value._isLevelExit);
            }
        }
    }
}