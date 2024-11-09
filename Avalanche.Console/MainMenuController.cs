using System;

namespace Avalanche.Console
{
    internal class MainMenuController
    {
        private MainMenuModel model;
        private MainMenuView view;

        public MainMenuController(MainMenuModel model, MainMenuView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Run()
        {
            ConsoleKey key;
            do
            {
                // Відображаємо меню
                view.DisplayMenu(model);

                // Зчитуємо натискання клавіші
                key = System.Console.ReadKey().Key;

                // Змінюємо вибір відповідно до натиснутого клавіші
                model.ChangeSelection(key);

                // Якщо натиснута клавіша Enter, обробляємо вибір
                if (key == ConsoleKey.Enter)
                {
                    HandleSelection();
                }

            } while (key != ConsoleKey.Escape);
        }

        private void HandleSelection()
        {
            switch (model.CurrentIndex)
            {
                case 0:
                    // Логіка для запуску гри
                    System.Console.Clear();
                    System.Console.WriteLine("Starting the game...");
                    break;
                case 1:
                    // Логіка для налаштувань
                    System.Console.Clear();
                    System.Console.WriteLine("Options...");
                    break;
                case 2:
                    // Логіка для виходу
                    System.Console.Clear();
                    System.Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
            }
            System.Console.WriteLine("Press any key to return to the main menu!");
            System.Console.ReadKey();
        }
    }
}
