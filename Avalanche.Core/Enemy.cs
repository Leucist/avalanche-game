using System.Reflection.Metadata.Ecma335;
using static Avalanche.Core.AppConstants;

namespace Avalanche.Core
{
    public class Enemy : Entity
    {

        public Enemy(
            int x = RoomCharWidth / 2,
            int y = RoomCharHeight / 2,
            DirectionAxisType directionAxis = DirectionAxisType.X,
            int health = DefaultEntityHealth,
            int damage = DefaultEntityDamage,
            int attackCooldown = DefaultAttackCooldown
            ) : base(x, y, directionAxis, 1, health, damage, attackCooldown)
        {
            
        }

        /*
        int CheckEnemyColliders()
        {
            if (GetX() >= (RoomCharWidth - 1))  // Right bound
            {
                SetX(GetX()-1);
                return 1;
            }
            else if (GetX() <= 0)  // Left bound
            {
                SetX(GetX() + 1);
                return 2;
            }
            else if (GetY() >= (RoomCharHeight - 1))  // Lower bound
            {
                SetY(GetY() - 1);
                return 3;
            }
            else if (GetY() <= 0 - 1)  // Upper bound
            {
                SetY(GetY() + 1);
                return 4;
            }
            return 0;
        }
        */

        /*public Enemy ShallowCopy()
        {
            return (Enemy) this.MemberwiseClone();
        } */

        public Enemy Copy()
        {
            return new Enemy(
                this._coords[0],
                this._coords[1],
                this._directionAxis,
                this._health,
                this._damage,
                this._attackCooldown);
        }

        public void RandomMovement()
        {
            Random random = new Random();
            int randomIntInRange = random.Next(1, 5);

            switch (randomIntInRange)
            {
                case 1:
                    Move(DirectionAxisType.X, 1);
                    break;
                case 2:
                    Move(DirectionAxisType.X, -1);
                    break;
                case 3:
                    Move(DirectionAxisType.Y, 1);
                    break;
                case 4:
                    Move(DirectionAxisType.Y, -1);
                    break;
            }
        }

        public void ManageAction() {
            if (_isAlerted) {
                if (_target != null && CanAttack()) {
                    Attack();
                    _target.TakeDamage(_damage);
                }
                SetTargetFollowing();
                Move();
                return;
            }
            else {
                RandomMovement();
            }
        }

        // public void ManageAction() {
        //     switch()
        // }

        // // Preset enemy action
        // public void ManageFutureAction() {
        //     if (_isAlerted) {
        //         if (_target != null && CanAttack()) {
        //             Attack();
        //             _target.TakeDamage(_damage);
        //         }
        //         SetTargetFollowing();
        //         Move();
        //         return;
        //     }
        //     else {
        //         RandomMovement();
        //     }
        // }

        public void SetTargetFollowing() {
            if (_target == null) return;    // just in case*
            // Get X and Y gap size (may be negative -> raw) between Enemy and the Target
            int[] deltaCoords = {
                _target.GetX() - this.GetX(),   // diff on X axis
                _target.GetY() - this.GetY()    // diff on Y axis
            };
            // Sets direction axis inclining on the axis with the farther gap
            int directionAxis = Math.Abs(deltaCoords[0]) > Math.Abs(deltaCoords[1]) ? 0 : 1;
            _directionAxis = (DirectionAxisType) directionAxis;

            // Sets direction towards the target
            _direction = Math.Sign(deltaCoords[directionAxis]);
        }

        private bool CanAttack() {
            return Reaches(_target!.GetX(), _target!.GetY())
                && IsReadyToAttack();
        }
    }
}
