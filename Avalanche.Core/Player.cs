using static Avalanche.Core.AppConstants;


namespace Avalanche.Core
{
    public class Player : Entity
    {
        public int _heat { get; set; }
        public int _mushrooms { get; set; }
        public int _rocks { get; set; }
        private string _name;

        
        private int _valueChangeCooldown;
        private int _valueChangeCooldownCounter;


        public Player(
            int x = RoomCharWidth / 2, 
            int y = RoomCharHeight / 2, 
            DirectionAxisType directionAxis = DirectionAxisType.X,
            int health = (int) (DefaultEntityHealth * 1.5), 
            int damage = (int) (DefaultEntityDamage * 1.5),
            int attackCooldown = DefaultAttackCooldown,
            // int actionCooldown = DefaultActionCooldown,
            string name = DefaultPlayerName)
            : base(x, y, directionAxis, health, damage, attackCooldown, 0)
        {
            _mushrooms = 0;
            _rocks = 0;
            _heat = DefaultPlayerHeat;
            _name = name;
            _valueChangeCooldown = DefaultActionCooldown;
            _valueChangeCooldownCounter = 0;
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

        public void UpdateHeat()
        {
            if (IsReadyToAct() && _heat > 0) {
                _heat -= DefaultFreezeDelta;
                if (_heat < 0) {_heat = 0;}
            }
        }

        public void Regenerate() {
            if (_health < MaxPlayerHealth && mayBeChanged()) {
                if (_heat <= 0) {
                    _health--;
                }
                else {
                    _health++;
                }
                _valueChangeCooldownCounter = _valueChangeCooldown;
            }
            UpdateRegenCooldown();
        }

        public void UpdateRegenCooldown() {
            if (_valueChangeCooldownCounter > 0)
                _valueChangeCooldownCounter--;
        }

        private bool mayBeChanged() {
            return _valueChangeCooldownCounter == 0;
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
    
