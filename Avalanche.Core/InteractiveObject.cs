using static Avalanche.Core.AppConstants;

namespace Avalanche.Core
{
    public class InteractiveObject : GameObject
    {
        ConsumableType _consumableType;
        public InteractiveObject(ConsumableType consumableType,int x = RoomCharWidth / 2, 
            int y = RoomCharHeight / 2) 
            : base(x, y)
        {
            _consumableType = consumableType;
        }

        public void Update(Player player)
        {
            if ( (player._x == _x) && (player._y == _y) )
            {
                if (false)  // IF BUTTON PRESSED
                {
                    player.UseIteractiveObject(_consumableType);
                }
                
            }
        }
    }
}
