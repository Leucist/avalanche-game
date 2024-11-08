using Avalanche.Core;
using SadConsole;
using SadRogue.Primitives;

using static Avalanche.Core.AppConstants;

namespace Avalanche.Console
{
    public class GameConsole : SadConsole.Console
    {
        private int _counter = 0;

        public GameConsole(int width, int height) : base(width, height)
        {
            // Title is showed
            string textMsg = AppName.ToUpper();
            this.Print(
                Width / 2 - (textMsg.Length / 2),
                Height / 2 - 1,
                textMsg);
            // if ()
        }

        // Переопределяем метод обновления, который вызывается на каждом кадре
        public override void Update(TimeSpan delta)
        {
            base.Update(delta);

            this.Clear();
            this.Print(10, 10, _counter++.ToString());
        }
    }
}