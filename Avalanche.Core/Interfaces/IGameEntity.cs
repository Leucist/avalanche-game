using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalanche.Core.Interfaces
{
    public interface IGameEntity : IGameObject
    {
        void Move();
        void TakeDamage(int damage);

    }
}
