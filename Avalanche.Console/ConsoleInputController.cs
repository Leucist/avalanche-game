using Avalanche.Core;


namespace Avalanche.Console
{
    public class ConsoleInputController : IInputController
    {
        private Dictionary<ConsoleKey, ActionType> _keyValuePairs;

        public ConsoleInputController() {
            _keyValuePairs = new Dictionary<ConsoleKey, ActionType>() {
                { ConsoleKey.W, ActionType.Up },
                { ConsoleKey.A, ActionType.Left },
                { ConsoleKey.S, ActionType.Down },
                { ConsoleKey.D, ActionType.Right },
                { ConsoleKey.Spacebar, ActionType.StraightAttack },
                { ConsoleKey.F, ActionType.SplashAttack },
                { ConsoleKey.R, ActionType.Shoot },
                { ConsoleKey.E, ActionType.Interact },
                { ConsoleKey.Enter, ActionType.Enter }
            };
        }
        
        private ConsoleKey GetKeyboardInput() {
            return System.Console.ReadKey(intercept: true).Key;
        }
        public ActionType GetUserAction()
        {
            ConsoleKey pressedKey = GetKeyboardInput();
            if (_keyValuePairs.ContainsKey(pressedKey))
                return _keyValuePairs[pressedKey];
            else
                return ActionType.NullAction;
        }

    }
}
