using Avalanche.Core;

namespace Avalanche.Console
{
    public class ConsoleMainMenuView
    {
        private MainMenuModel _model;
        public ConsoleMainMenuView(MainMenuModel model) {
            _model = model;
        }

        public void Render()
        {
            for (int i = 0; i < _model._options.Count; i++)
            {
                if (i == _model._currentIndex)
                {
                    ConsoleRenderer.SetTextColor(ConsoleColor.Green);
                    System.Console.WriteLine($"> {_model._options[i].Item1} <");
                    System.Console.ResetColor();
                }
                else
                {
                    System.Console.WriteLine(_model._options[i].Item1);
                }
            }
        }
    }
}
