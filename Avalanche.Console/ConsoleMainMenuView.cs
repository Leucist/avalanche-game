using Avalanche.Core;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class ConsoleMainMenuView : IView
    {
        public MainMenuModel _model { get; }
        public ConsoleMainMenuView(MainMenuModel model) {
            _model = model;
        }

        public void Render()
        {
            
            for(int height = 0; height < ScreenCharHeight / 3; height++)
            {
                System.Console.WriteLine();
            }

            for (int i = 0; i < _model.optionsMainMenu.Length; i++)
            {

                if (i == _model._currentIndex)
                {
                    ConsoleRenderer.SetTextColor(ConsoleColor.DarkMagenta);
                    foreach (string option in _model.optionsMainMenu[i])
                    {
                        System.Console.Write(new string(' ', (int)(ScreenCharWidth / 2.35)));
                        System.Console.WriteLine(option);
                    }
                    System.Console.ResetColor();
                    System.Console.WriteLine();
                }
                else
                {
                    foreach (string option in _model.optionsMainMenu[i])
                    {
                        System.Console.Write(new string(' ', (int)(ScreenCharWidth / 2.35)));
                        System.Console.WriteLine(option);
                    }
                    System.Console.WriteLine();
                }
            }
        }
    }
}
