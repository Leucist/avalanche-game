using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Avalanche.Core;
using SadConsole.Input;
using SadRogue.Primitives;


namespace Avalanche.Console
{
    class InputController : IInputController
    {
        public ActionType getKeyboardInput()
        {
            if (SadConsole.GameHost.Instance.Keyboard.IsKeyPressed(Keys.W))
            {
                return ActionType.Up;   
            }
            else if (SadConsole.GameHost.Instance.Keyboard.IsKeyPressed(Keys.A))
            {
                return ActionType.Left;
            }
            else if (SadConsole.GameHost.Instance.Keyboard.IsKeyPressed(Keys.S))
            {
                return ActionType.Down;
            }
            else if (SadConsole.GameHost.Instance.Keyboard.IsKeyPressed(Keys.D))
            {
                return ActionType.Right;
            }
            else if (SadConsole.GameHost.Instance.Keyboard.IsKeyPressed(Keys.Space))
            {
                return ActionType.StraightAttack;
            }
            else if (SadConsole.GameHost.Instance.Keyboard.IsKeyPressed(Keys.F))
            {
                return ActionType.SplashAttack;
            }
            else if (SadConsole.GameHost.Instance.Keyboard.IsKeyPressed(Keys.R))
            {
                return ActionType.Shoot;
            }
            else if (SadConsole.GameHost.Instance.Keyboard.IsKeyPressed(Keys.E))
            {
                return ActionType.Interact;
            }
            else if (SadConsole.GameHost.Instance.Keyboard.IsKeyPressed(Keys.Enter))
            {
                return ActionType.Enter;
            }

            return ActionType.NullAction;

        }

    }
}
