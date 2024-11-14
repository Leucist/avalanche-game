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
        protected bool _isAlerted;
        protected int _attackCooldown;
        protected int _attackCooldownCounter;
        protected Entity? _target;



        public Entity(
            int x, int y,
            DirectionAxisType directionAxis = DirectionAxisType.X, 
            int health = DefaultEntityHealth,
            int damage = DefaultEntityDamage,
            int attackCooldown = DefaultAttackCooldown,
            int direction = 1) 
            : base(x, y)
        {
            _health = health;
            _damage = damage;
            _attackCooldown = attackCooldown;
            _attackCooldownCounter = 0;
            _directionAxis = directionAxis;
            _direction = direction;
            _speed = 1;
            _isAlerted = false;
        }

        public void CheckColliders()
        {
            if (_coords[0] > RoomCharWidth - 1)  // Right bound
            {
                _coords[0] = RoomCharWidth - 1;
            }
            else if (_coords[0] < 0)  // Left bound
            {
                _coords[0] = 0;
            }
            else if(_coords[1] > RoomCharHeight - 1)  // Lower bound
            {
                _coords[1] = RoomCharHeight - 1;
            }
            else if(_coords[1] < 0)  // Upper bound
            {
                _coords[1] = 0;
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

        public void UpdateCooldown() {
            if (_attackCooldownCounter > 0)
                _attackCooldownCounter--;
        }

        public void Attack() {
            _attackCooldownCounter = _attackCooldown;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }

        public bool IsDead() {
            return _health <= 0;
        }

        public int[] GetFocusPoint() {
            int[] point = [this.GetX(), this.GetY()];
            point[(int)_directionAxis] += _direction;

            return point;
        }

        public void SetFocusOn(Entity target) {
            _isAlerted = true;
            _target = target;
        }

        protected bool Reaches(int x, int y) {
            int[] focusPoint = GetFocusPoint();
            return focusPoint[0] == x
                && focusPoint[1] == y;
        }

        protected bool IsReadyToAttack() {
            return _attackCooldownCounter == 0;
        }
        
        public bool IsAlerted => _isAlerted;
    }
}
