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
        private int _speed;
        public DirectionAxisType _directionAxis { get; set; }



        public Entity(
            int x, int y,
            DirectionAxisType directionAxis = DirectionAxisType.X, 
            int health = DefaultEntityHealth,
            int damage = DefaultEntityDamage) 
            : base(x, y)
        {
            _health = health;
            _damage = damage;
            _directionAxis = directionAxis;
            _direction = 1;
            _speed = 1;
        }

        public void CheckColliders()
        {
            if (_coords[0] >= (RoomCharWidth-1))  // Right bound
            {
                _coords[0] -= 1;
            }
            else if (_coords[0] <= 0)  // Left bound
            {
                _coords[0] += 1;
            }
            else if(_coords[1] >= (RoomCharHeight-1))  // Lower bound
            {
                _coords[1] -= 1;
            }
            else if(_coords[1] <= 0-1)  // Upper bound
            {
                _coords[1] += 1;
            }
        }

        public void Move()
        {
            _coords[(int)_directionAxis] += _speed * _direction;
        }

        public void Move(DirectionAxisType directionAxis, int direction)
        {
            _coords[(int)directionAxis] += _speed * direction;
            CheckColliders();
        }

        void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }
}
