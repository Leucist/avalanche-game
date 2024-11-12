using Avalanche.Core;

namespace Avalanche.Console
{
    public class ConsoleInputController : IInputController
    {
        private Dictionary<ConsoleKey, ActionType> _keyValuePairs;
        private ConsoleKey _pressedKey;

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
                { ConsoleKey.X, ActionType.ConsumeMushroom },
                { ConsoleKey.Enter, ActionType.Enter },
                { ConsoleKey.UpArrow, ActionType.Up },
                { ConsoleKey.DownArrow, ActionType.Down },
                { ConsoleKey.LeftArrow, ActionType.Left },
                { ConsoleKey.RightArrow, ActionType.Right },
                { ConsoleKey.None, ActionType.NullAction }
            };
        }
        
        private ConsoleKey GetKeyboardInput() {
            return System.Console.ReadKey(intercept: true).Key;
        }

        private void GetKeyboardInputAsync(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) 
            {
                if (System.Console.KeyAvailable)
                {
                    _pressedKey = System.Console.ReadKey(intercept: true).Key;
                    break;
                }
            }
        }

        public ActionType GetUserAction()
        {
            // Set pressedKey to Null
            _pressedKey = ConsoleKey.None;

            // Init cancelation token
            var cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            // Creat new Thread
            Thread inputThread = new Thread(() => GetKeyboardInputAsync(cancellationToken));
            inputThread.Start();

            // Wait for 1 default frame time unit
            Thread.Sleep(AppConstants.DefaultFrameTime);

            // Cancel the child process and wait for its execution
            cancellationTokenSource.Cancel();
            inputThread.Join();

            // Return matching to the key ActionType
            if (_keyValuePairs.ContainsKey(_pressedKey))
                return _keyValuePairs[_pressedKey];
            else
                return ActionType.DefaultAction;
        }

    }
}
