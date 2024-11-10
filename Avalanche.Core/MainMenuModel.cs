namespace Avalanche.Core
{
    public class MainMenuModel
    {
        public List<(string, GameStateType)> _options { get; set; }
        public int _currentIndex { get; set; }

        public MainMenuModel()
        {
            _options = new List<(string, GameStateType)> { 
                ("New Game", GameStateType.Game), 
                ("Options", GameStateType.OptionsMenu), 
                ("Exit", GameStateType.Exit)};
            _currentIndex = 0;
        }

        public void ChangeSelection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _currentIndex = (_currentIndex - 1 + _options.Count) % _options.Count;
                    break;
                case ConsoleKey.DownArrow:
                    _currentIndex = (_currentIndex + 1) % _options.Count;
                    break;
            }
        }

        public void MoveCursorUp() {
            _currentIndex = (_currentIndex - 1 + _options.Count) % _options.Count;
        }

        public void MoveCursorDown() {
            _currentIndex = (_currentIndex + 1) % _options.Count;
        }

        public GameStateType GetCurrentKey() {
            return _options[_currentIndex].Item2;
        }
    }
}
