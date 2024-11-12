using static Avalanche.Core.AppConstants;

namespace Avalanche.Core
{
    public class Fireplace : GameObject
    {
        Player player;
        bool isUsed;
        public Fireplace(int x, int y, Player p) : base(x, y)
        {
            player = p;
            isUsed = true;
        }

        public void UseCampfire()
        {
            if (isUsed)
            {
                player._heat = DefaultPlayerHeat;
            }
            isUsed = false;
        }
    }
}
