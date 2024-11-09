using System;
using System.Collections.Generic;

namespace Avalanche.Console
{
    public class MainMenuModel
    {
        public List<string> Options { get; private set; }
        public int CurrentIndex { get; set; }

        public MainMenuModel()
        {
            Options = new List<string> { "Start", "Options", "Exit" };
            CurrentIndex = 0;
        }

        public void ChangeSelection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    CurrentIndex = (CurrentIndex - 1 + Options.Count) % Options.Count;
                    break;
                case ConsoleKey.DownArrow:
                    CurrentIndex = (CurrentIndex + 1) % Options.Count;
                    break;
            }
        }
    }
}
