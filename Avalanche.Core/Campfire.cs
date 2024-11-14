using static Avalanche.Core.AppConstants;

namespace Avalanche.Core
{
    public class Campfire : GameObject
    {
        Player player;
        bool isBurning;
        int timeCounter;
        public Campfire(int x, int y, Player p) : base(x, y)
        {
            player = p;
            isBurning = true;
            timeCounter = DefaultFireTime;
        }

        public void UpdateCampfireState()
        {
            if (isBurning)
            {
                if (Math.Pow((GetX()-player.GetX()), 2) + Math.Pow((GetY()-player.GetY()), 2) 
                    <= Math.Pow(DefaultCampfireRange, 2))
                {
                    player._heat++;
                }
                timeCounter--;
                if (timeCounter < 0)
                    isBurning = false;
            }
            
        }
    }
}
