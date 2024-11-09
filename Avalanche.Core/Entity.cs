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
        int _health;
        int _damage;
        int _direction { set; get; }  // direction of moving 1 0 -1
        DirectionType _currentDirection { get; set; }



        public Entity(int x, int y, int direction, DirectionType _directionAxis, int health = Health, int damage = Damage) 
            : base(x, y)
        {
            this._health = health;
            this._damage = damage;
            this._direction = direction;
            this._currentDirection = _directionAxis;

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
