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
            
            int gameScreenStartY = 2; 
            ConsoleRenderer.DrawBox(
                0, gameScreenStartY, 
                RoomCharWidth, RoomCharHeight);

            
        }
    }
}