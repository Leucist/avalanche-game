using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Core
{
    public class Entity : GameObject
    {
        public int _health { get; set; }
        public int _damage;
        public int _direction { set; get; }  // direction of moving 1 0 -1
        DirectionType _currentDirection { get; set; }



        public Entity(int x, int y, DirectionType directionAxis = DirectionType.East, 
            int health = DefaultEntityHealth, int damage = DefaultEntityDamage) 
            : base(x, y)
        {
            _health = health;
            _damage = damage;
            _currentDirection = directionAxis;
            _direction = 1;
        }

        void Move(int speed, DirectionType currentDirection, int direction = 1)
        {
            switch (currentDirection)
            {
                case DirectionType.North:
                    _y += (speed * direction);
                    break;
                case DirectionType.East:
                    _x += (speed * direction);
                    break;
                case DirectionType.South:
                    _y += (-speed * direction);
                    break;
                case DirectionType.West:
                    _x += (-speed * direction);
                    break;
            }
        }

        void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }
}
