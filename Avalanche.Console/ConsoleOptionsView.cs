using Avalanche.Core;

namespace Avalanche.Console
{
    public class ConsoleOptionsView : IView
    {
        private readonly OptionsModel _model;
        private readonly string[] _title;
        private bool _wasNeverDrawn;
        private int _lastDrawnIndex;

        public ConsoleOptionsView(OptionsModel model) {
            _model = model;
            _title =
            [
                "█████ █████ █████ ███ █████ ██  █ █████",
                "█   █ █   █   █    █  █   █ ██  █ █",
                "█   █ █████   █    █  █   █ █ █ █ █████",
                "█   █ ██      █    █  █   █ █  ██     █",
                "█████ ██      █   ███ █████ █  ██ █████",
            ];
            _wasNeverDrawn = true;
            _lastDrawnIndex = -1;
        }

        public void Reset() {
            _wasNeverDrawn = true;
        }

        public void Render() {
            if (_wasNeverDrawn) {
                // TODO / Draw:
                //
                //                                  _title
                //
                //  Press Escape to get back to the Main Menu
                //  
                //
                //
                //              Difficulty:      < (DifficultyLevelType) _model.Difficulty >
                //
                //  
                //  Use arrows (Left, Right) to change difficulty
                //  Use Enter to save your choise
                //
            }

            if (_lastDrawnIndex != _model.Difficulty) {
                // TODO / Redraw the chosen difficulty
                //
                //* for this, something like "int _optionValueStartX" or etc. may come in handy
                //* this way, refreshing will be just:
                //* - Set cursor to {_optionValueStartX, someY}
                //* - Write n spaces, where n = ((DifficultyLevelType) _lastDrawnIndex).Length
                //* - (once again) Set cursor to {_optionValueStartX, someY}
                //* - Write (DifficultyLevelType) _model.Difficulty
                //
                _lastDrawnIndex = _model.Difficulty;
            }
        }
    }
}
