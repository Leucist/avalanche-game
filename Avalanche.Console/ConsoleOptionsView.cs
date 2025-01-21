using Avalanche.Core;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class ConsoleOptionsView : IView
    {
        private readonly OptionsModel _model;
        private readonly string[] _title;
        private int _lastDrawnIndex;
        private int _xArrowsOffset;
        private int _xDifficultyOffset;
        private int _yDifficultyOffset;
        private string _labelUseArrows;
        private string _labelPressEnter;
        private string _labelPressEscape;
        private int windowHeight;
        private int windowWidth;

        public ConsoleOptionsView(OptionsModel model)
        {
            _model = model;
            _title = new string[]
            {
                "█████ █████ █████ ███ █████ ██  █ █████",
                "█   █ █   █   █    █  █   █ ██  █ █",
                "█   █ █████   █    █  █   █ █ █ █ █████",
                "█   █ ██      █    █  █   █ █  ██     █",
                "█████ ██      █   ███ █████ █  ██ █████",
            };

            _labelUseArrows = "USE LEFT/RIGHT ARROWS TO CHANGE DIFFICULTY";
            _labelPressEnter = "PRESS ENTER TO SAVE YOUR CHOICE";
            _labelPressEscape = "PRESS ESCAPE TO RETURN TO THE MAIN MENU";

            windowHeight = System.Console.WindowHeight;
            windowWidth = System.Console.WindowWidth;

            _xArrowsOffset = windowWidth / 2 - _labelUseArrows.Length / 2;
            _xDifficultyOffset = windowWidth / 2 - 15; // Adjusted offset for difficulty label
            _yDifficultyOffset = windowHeight / 2 + 5;

            _lastDrawnIndex = -1;
        }

        public void Reset()
        {
            _lastDrawnIndex = -1;
        }

        public void Render()
        {
            // Draw static elements if required
            if (_lastDrawnIndex == -1)
            {
                ConsoleRenderer.ClearScreen();
                DrawTitle();
                DrawLabels();
                DrawDifficulty();
            }

            // Redraw difficulty if it has changed
            if (_lastDrawnIndex != _model.Difficulty)
            {
                UpdateDifficulty();
                _lastDrawnIndex = _model.Difficulty;
            }
        }

        private void DrawTitle()
        {
            for (int height = 0; height < windowHeight / 3; height++)
            {
                System.Console.WriteLine();
            }

            foreach (string line in _title)
            {
                System.Console.Write(new string(' ', windowWidth / 2 - line.Length / 2));
                System.Console.WriteLine(line);
            }

            System.Console.WriteLine();
        }

        private void DrawLabels()
        {
            // "Press Escape to get back to the Main Menu"
            System.Console.WriteLine(new string(' ', windowWidth / 2 - _labelPressEscape.Length / 2) + _labelPressEscape);
            System.Console.WriteLine();

            // "Use arrows (Left, Right) to change difficulty"
            System.Console.WriteLine(new string(' ', _xArrowsOffset) + _labelUseArrows);

            // "Use Enter to save your choice"
            System.Console.WriteLine(new string(' ', _xArrowsOffset) + _labelPressEnter);
        }

        private void DrawDifficulty()
        {
            System.Console.SetCursorPosition(_xDifficultyOffset, _yDifficultyOffset);
            System.Console.Write("Difficulty:      < " + ((DifficultyLevelType)_model.Difficulty).ToString() + " >");
        }

        private void UpdateDifficulty()
        {
            // Clear previous difficulty
            System.Console.SetCursorPosition(_xDifficultyOffset + 15, _yDifficultyOffset);
            System.Console.Write(new string(' ', 20)); // Assumes max length of difficulty string

            // Draw new difficulty
            System.Console.SetCursorPosition(_xDifficultyOffset + 15, _yDifficultyOffset);
            System.Console.Write("< " + ((DifficultyLevelType)_model.Difficulty).ToString() + " >");
        }
    }
}