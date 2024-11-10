using Avalanche.Core;

namespace Avalanche.Console
{
    public class ConsoleLevelView : IView
    {
        private readonly LevelModel _model;
        public ConsoleLevelView(LevelModel model) {
            _model = model;
        }

        public void Render() {}
    }
}