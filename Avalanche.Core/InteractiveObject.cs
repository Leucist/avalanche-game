using static Avalanche.Core.AppConstants;

namespace Avalanche.Core
{
    public class InteractiveObject : GameObject
    {
        ItemType _type;
        public InteractiveObject(
            ItemType itemType,
            int x = RoomCharWidth / 2, 
            int y = RoomCharHeight / 2) 
            : base(x, y)
        {
            _type = itemType;
        }

        // public void Update(Player player)
        // {
        //     if ( (player.GetX() == this.GetX()) && (player.GetY() == this.GetY()) )
        //     {
        //         if (false)  // IF BUTTON PRESSED
        //         {
        //             player.UseIteractiveObject(_consumableType);
        //         }
                
        //     }
        // }
    }
}
