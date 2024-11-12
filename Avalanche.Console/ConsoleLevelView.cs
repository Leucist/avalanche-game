using Avalanche.Core;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class ConsoleLevelView : IView
    {
        private readonly LevelModel _model;
        private bool _wasNeverDrawn;
        
        public ConsoleLevelView(LevelModel model) {
            _model = model;
            _wasNeverDrawn = true;
        }

        public void Render() {
            if (_wasNeverDrawn) {
                // Clear screen and draw room borders
                ConsoleRenderer.ClearScreen();
                ConsoleRenderer.DrawBox(RoomCharWidth, RoomCharHeight);
                _wasNeverDrawn = false;
                RenderDoors();
            }

            ConsoleRenderer.ClearDirtyPixels(_model._currentRoom!._dirtyPixels);
            _model._currentRoom!._dirtyPixels.Clear();

            if (_model._currentRoom!._isDirty) {
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

        private void RenderDoors() {
            foreach (var door in _model._currentRoom!._doors) {
                ConsoleRenderer.DrawDoor(door.Key.GetX(), door.Key.GetY(), door.Value._isLevelExit);
            }
        }
    }
}