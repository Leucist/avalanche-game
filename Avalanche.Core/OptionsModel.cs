namespace Avalanche.Core
{
    public class OptionsModel
    {
        private const string _menuTitle = "Options";

        private readonly int _amountOfDifficultyLevels = Enum.GetValues(typeof(DifficultyLevelType)).Length;
        private int _difficulty = (int) GameState._difficulty;
        
        public int Difficulty => _difficulty;

        private void SwitchDifficulty(int direction) {
            // Rise or Lower the Difficulty and ensure it stays within bounds
            _difficulty = (_difficulty + direction) % (_amountOfDifficultyLevels + 1);
            if (_difficulty == 0) _difficulty = _amountOfDifficultyLevels;
        }

        public void RiseDifficulty() {
            SwitchDifficulty(1);
        }

        public void LowerDifficulty() {
            SwitchDifficulty(-1);
        }
        
        public void SubmitDifficulty() {
            GameState._difficulty = (DifficultyLevelType) _difficulty;
        }
    }
}
