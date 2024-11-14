using static Avalanche.Core.AppConstants;


namespace Avalanche.Core
{
    public class Player : Entity
    {
        public int _heat { get; set; }
        public int _mushrooms { get; set; }
        public int _rocks { get; set; }
        private string _name;


        public Player(
            int x = RoomCharWidth / 2, 
            int y = RoomCharHeight / 2, 
            DirectionAxisType directionAxis = DirectionAxisType.X,
            int health = DefaultEntityHealth, 
            int damage = DefaultEntityDamage,
            int attackCooldown = DefaultAttackCooldown,
            string name = DefaultPlayerName)
            : base(x, y, directionAxis, health, damage, attackCooldown)
        {
            _mushrooms = 0;
            _rocks = 0;
            _heat = DefaultPlayerHeat;
            _name = name;
        }

        public void ConsumeMushroom()
        {

            if (_mushrooms >= 1)
            {
                _mushrooms--;

                Random random = new Random();

                int HPChange = random.Next(DefaultMushroomsMinimalHpChange, DefaultMushroomsMaximalHpChange);
                base._health += HPChange;
            }

        }

        public bool ThrowRock()
        {
            if (_rocks > 0) {
                _rocks--;
                return true;
            }
            return false;
        }

        void UpdateHeat(int delta)
        {
            _heat -= delta;
        }

        void AddItem(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Mushroom:
                    _mushrooms++;
                    break;
                case ItemType.Rock:
                    _rocks++;
                    break;
            }
        }

        public void SetName(string name)
        {
            _name = name;
        }
    }
}
    
