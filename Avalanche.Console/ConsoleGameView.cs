using Avalanche.Core;
using SadConsole;
using SadConsole.Configuration;
using Microsoft.Xna.Framework;

using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class ConsoleGameView : IGameView
    {
        private ScreenObject _screenContainer;
        private SadConsole.Console _screenConsole;

        private int _counter = 0;

        public ConsoleGameView() {
            _screenContainer = new ScreenObject();
            // _screenConsole = new SadConsole.Console(ScreenCharWidth, ScreenCharHeight);
        }

        public void Initialize() {
            SadConsole.Settings.WindowTitle = AppName;
            Builder configuration = new Builder()
                .SetScreenSize(ScreenCharWidth, ScreenCharHeight)
                .OnStart(StartUp);
            SadConsole.Game.Create(configuration);

            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private void StartUp(object? sender, GameHost host)
        {
            _screenConsole = new GameConsole(ScreenCharWidth, ScreenCharHeight);
            SadConsole.Game.Instance.Screen = _screenConsole;
        }

        public void Render() {}
        
    }
}