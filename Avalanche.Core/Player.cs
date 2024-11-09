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
        int _heat { get; set; }
        int _mushrooms { get; set; }
        int _rocks { get; set; }


        public Player(int x, int y, DirectionType directionAxis,
                      int health, int damage)
                      : base(x, y, directionAxis, health, damage)
        {
            _mushrooms = 0;
            _rocks = 0;
            _heat = DefaultPlayerHeat;
        }

        void ConsumeMushroom()
        {
            Random random = new Random();

            int HpChange = random.Next(DefaultMushroomsMinimalHpChange, DefaultMushroomsMaximalHpChange);
            base._health += HpChange;

        }

        void UpdateHeat(int delta)
        {
            _heat -= delta;
        }
    }

    
}
