using System;
using System.Collections.Generic;

namespace Avalanche.Console
{
    public class MainMenuView
    {
        public void DisplayMenu(MainMenuModel model)
        {
            System.Console.Clear();

            for (int i = 0; i < model.Options.Count; i++)
            {
                if (i == model.CurrentIndex)
                {
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine($"> {model.Options[i]} <");
                    System.Console.ResetColor();
                }
                else
                {
                    System.Console.WriteLine(model.Options[i]);
                }
            }
        }
    }
}
