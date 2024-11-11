using Avalanche.Core;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class ConsoleLevelView : IView
    {
        private readonly LevelModel _model;
        public ConsoleLevelView(LevelModel model) {
            _model = model;
        }

        public void Render() {
            ConsoleRenderer.DrawBox(
                RoomCharWidth, RoomCharHeight);
            ConsoleRenderer.DrawPlayer(_model._player.GetX(), _model._player.GetY());

            foreach (Entity enemy in _model._rooms[_model._currentRoom]._model._enemies)
            {
                ConsoleRenderer.DrawEnemy(enemy.GetX(), enemy.GetY());

            }

        }
    }
}