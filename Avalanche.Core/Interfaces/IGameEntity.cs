using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalanche.Core.Interfaces
{
    public interface IGameEntity : IGameObject
    {
        void Move(int speed, DirectionType currentDirection, int direction = 1);
        void TakeDamage(int damage);

    }
}
