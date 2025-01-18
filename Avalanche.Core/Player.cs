using static Avalanche.Core.AppConstants;


namespace Avalanche.Core
{
    public class Player : Entity
    {
        public int _heat { get; set; }
        public int _mushrooms { get; set; }
        public int _rocks { get; set; }
        private string _name;

        private int _baseX, _baseY;
        private int _baseHealth;
        private int _baseDamage;

        
        private int _valueChangeCooldown;
        private int _valueChangeCooldownCounter;


        public string Name => _name;


        public Player(
            int x = RoomCharWidth / 2, 
            int y = RoomCharHeight / 2, 
            DirectionAxisType directionAxis = DirectionAxisType.X,
            int health = (int) (DefaultEntityHealth * 1.5), 
            int damage = (int) (DefaultEntityDamage * 1.5),
            int attackCooldown = DefaultAttackCooldown,
            int actionCooldown = 0,
            string name = DefaultPlayerName)
            : base(x, y, directionAxis, 1, health, damage, attackCooldown, actionCooldown)
        {
            _mushrooms = 0;
            _rocks = 0;
            _heat = DefaultPlayerHeat;
            _name = name;
            _valueChangeCooldown = DefaultActionCooldown;
            _valueChangeCooldownCounter = 0;

            // Set base values for the reset
            _baseX = x;
            _baseY = y;
            _baseHealth = health;
            _baseDamage = damage;
        }

        public void Reset() {
            _coords = [_baseX, _baseY];
            _health = _baseHealth;
            _damage = _baseDamage;
            _mushrooms = 0;
            _rocks = 0;
            _heat = DefaultPlayerHeat;
            _attackCooldown = DefaultAttackCooldown;
            _attackCooldownCounter = 0;
            _valueChangeCooldown = DefaultActionCooldown;
            _valueChangeCooldownCounter = 0;
        }

        public void ConsumeMushroom()
        {

            if (_mushrooms >= 1)
            {
                _mushrooms--;

                Random random = new Random();

                int HPChange = random.Next(0, 2) == 0 ? DefaultMushroomsMinimalHpChange : DefaultMushroomsMaximalHpChange;
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
            if (_name.Equals("dead", StringComparison.CurrentCultureIgnoreCase)) base._health = 0;
        }
    }
}
    
