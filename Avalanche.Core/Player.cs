using Avalanche.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
            string name = DefaultPlayerName)
            : base(x, y, directionAxis, health, damage)
        {
            _mushrooms = 0;
            _rocks = 0;
            _heat = DefaultPlayerHeat;
            _name = name;
        }

        void ConsumeMushroom()
        {

            if (_mushrooms >= 1)
            {
                _mushrooms--;

                Random random = new Random();

                int HPChange = random.Next(DefaultMushroomsMinimalHpChange, DefaultMushroomsMaximalHpChange);
                base._health += HPChange;
            }

        }

        void ThrowRock()
        {

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
        }
    }
}
    
