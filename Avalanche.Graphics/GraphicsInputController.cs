using SFML.Window;
using System.Collections.Generic;
using Avalanche.Core;

namespace Avalanche.Graphics
{
    public class GraphicsInputController : IInputController
    {
        private readonly Dictionary<Keyboard.Key, ActionType> _keyValuePairs;
        private ActionType _currentAction;
        // private Keyboard.Key _pressedKey;

        public GraphicsInputController()
        {
            _keyValuePairs = new Dictionary<Keyboard.Key, ActionType>() {
                // - WASD Movements
                { Keyboard.Key.W, ActionType.Up },
                { Keyboard.Key.A, ActionType.Left },
                { Keyboard.Key.S, ActionType.Down },
                { Keyboard.Key.D, ActionType.Right },
                // - Attacks
                { Keyboard.Key.Space, ActionType.StraightAttack },
                { Keyboard.Key.F, ActionType.SplashAttack },
                { Keyboard.Key.R, ActionType.Shoot },
                // - Other interactions
                { Keyboard.Key.E, ActionType.Interact },
                { Keyboard.Key.X, ActionType.ConsumeMushroom },
                { Keyboard.Key.Enter, ActionType.Enter },
                { Keyboard.Key.Escape, ActionType.Escape },
                // - Arrow Movements
                { Keyboard.Key.Up, ActionType.Up },
                { Keyboard.Key.Down, ActionType.Down },
                { Keyboard.Key.Left, ActionType.Left },
                { Keyboard.Key.Right, ActionType.Right },
                // - If nothing was pressed
                // { Keyboard.Key.None, ActionType.NullAction }
            };

            _currentAction = ActionType.NullAction;
        }

        // public void AttachToWindow(SFML.Graphics.RenderWindow window)
        // {
        //     window.KeyPressed += (sender, args) =>
        //     {
        //         if (_keyValuePairs.ContainsKey(args.Code))
        //             _currentAction = _keyValuePairs[args.Code];
        //     };
        // }

        public void ClearBuffer()
        {
            _currentAction = ActionType.NullAction;
        }

        private void GetKeyboardInputAsync(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) 
            {
                foreach (Keyboard.Key key in _keyValuePairs.Keys) {
                    if (Keyboard.IsKeyPressed(key)) {
                        _currentAction = _keyValuePairs[key];
                        break;
                    }
                }
            }
        }

        public ActionType GetUserAction()
        {
            // Set pressedKey to Null
            _currentAction = ActionType.NullAction;

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

            // // Return matching to the key ActionType
            // if (_keyValuePairs.ContainsKey(_pressedKey))
            //     return _keyValuePairs[_pressedKey];
            // else
            //     return ActionType.DefaultAction;

            return _currentAction;
        }
    }
}
