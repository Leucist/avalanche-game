using Avalanche.Core;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class ConsoleMainMenuView : IView
    {
        public MainMenuModel _model { get; }
        private string[][] _menuOptions;
        private int _prevIndex;
        private int _xArrowsOffset;
        private int _xEnterOffset;
        private int _yEnterOffset;
        private int _labelShowCountdown;
        private string _labelUseArrows;
        private string _labelPressEnter;
        int windowHeight = System.Console.WindowHeight;
        int windowWidth = System.Console.WindowWidth;

        public ConsoleMainMenuView(MainMenuModel model) {
            _model = model;
            _prevIndex = -1;
            _labelShowCountdown = DefaultFrameTime*2;
            _labelUseArrows = "USE ARROW KEYS TO NAVIGATE";
            _labelPressEnter = "PRESS ENTER TO SELECT";
            windowHeight = System.Console.WindowHeight;
            windowWidth = System.Console.WindowWidth;
            _xArrowsOffset = windowWidth / 2 - _labelUseArrows.Length / 2;
            _xEnterOffset = windowWidth / 2 - _labelPressEnter.Length / 2;
            _yEnterOffset = windowHeight / 3 + 22;  // magic number for enter label yPos
            _menuOptions =
            [
                new string[]
                {
                    "█████ █████ █████ █████ █████",
                    "█       █   █   █ █   █   █",
                    "█████   █   █████ █████   █",
                    "    █   █   █   █ █  █    █",
                    "█████   █   █   █ █  ██   █",
                },
                new string[]
                {
                    "█████ █████ █████ ███ █████ ██  █ █████",
                    "█   █ █   █   █    █  █   █ ██  █ █",
                    "█   █ █████   █    █  █   █ █ █ █ █████",
                    "█   █ ██      █    █  █   █ █  ██     █",
                    "█████ ██      █   ███ █████ █  ██ █████",
                },
                new string[]
                {
                    "█████ █    █ ███ █████",
                    "█      █  █   █    █",
                    "█████   ██    █    █",
                    "█      █  █   █    █",
                    "█████ █    █ ███   █",
                }
            ];
        }

        public void Render()
        {
            // Redraws menu if changes were commited
            if (_prevIndex != _model._currentIndex) {
                ConsoleRenderer.ClearScreen();
                DrawMenu();
                _prevIndex = _model._currentIndex;
            }

            // Draws or Erases EnterLabe
            switch (_labelShowCountdown) {
                case DefaultFrameTime*3:
                    RenderEnterLabel(_labelPressEnter);
                    break;
                case 0:
                    RenderEnterLabel(new string(' ', System.Console.WindowWidth));
                    break;
                case -20:
                    _labelShowCountdown = DefaultFrameTime*3 + 1;
                    break;
            }
            _labelShowCountdown--;
        }

        private void RenderEnterLabel(string placeholder) {
            System.Console.SetCursorPosition(_xEnterOffset, _yEnterOffset);
            System.Console.WriteLine(placeholder);
        }

        private void DrawMenu() {

            windowHeight = System.Console.WindowHeight;

            for (int height = 0; height < windowHeight / 3; height++)
            {
                System.Console.WriteLine();
            }

            windowWidth = System.Console.WindowWidth;

            for (int i = 0; i < _menuOptions.Length; i++)
            {

                if (i == _model._currentIndex)
                {
                    ConsoleRenderer.SetTextColor(ConsoleColor.DarkMagenta);
                    foreach (string option in _menuOptions[i])
                    {
                        System.Console.Write(new string(' ', (int)(windowWidth / 2.3)));
                        System.Console.WriteLine(option);
                    }
                    System.Console.ResetColor();
                    System.Console.WriteLine();
                }
                else
                {
                    foreach (string option in _menuOptions[i])
                    {
                        System.Console.Write(new string(' ', (int)(windowWidth / 2.35)));
                        System.Console.WriteLine(option);
                    }
                    System.Console.WriteLine();
                }
            }

            // Show "Use Arrows" label
            System.Console.WriteLine("\n\n");
            System.Console.Write(new string(' ', _xArrowsOffset));
            System.Console.WriteLine(_labelUseArrows);
        }
    }
}
