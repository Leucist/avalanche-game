﻿using System;
using System.Reflection.Emit;
using System.Threading;
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
            string name = DefaultPlayerName) : base(x, y, directionAxis, health, damage)
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

        void RandomMovement()
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

        
    }
}
