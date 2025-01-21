namespace Avalanche.Core
{
    public class MainMenuModel
    {
        public List<(string, GameStateType)> _options { get; set; }
        public int _currentIndex { get; set; }

        public MainMenuModel()
        {
            _options = new List<(string, GameStateType)> {
                ("New Game", GameStateType.NameInput), 
                ("Options", GameStateType.OptionsMenu), 
                ("Exit", GameStateType.Exit)};
            _currentIndex = 0;
        }

        public DateTime _cursorMovedTime;

        public void MoveCursorUp() {
            if ((DateTime.Now - _cursorMovedTime).TotalSeconds >= 0.15)
            {

                _cursorMovedTime = DateTime.Now;
                _currentIndex = (_currentIndex - 1 + _options.Count) % _options.Count;

            }
        }

        public void MoveCursorDown() {
            if ((DateTime.Now - _cursorMovedTime).TotalSeconds >= 0.15)
            {

                _cursorMovedTime = DateTime.Now;
                _currentIndex = (_currentIndex + 1) % _options.Count;
            }
        }

        public GameStateType GetCurrentKey() {
            return _options[_currentIndex].Item2;
        }
    }
}
