using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalanche.Core.Interfaces
{
    public interface IPlayer : IGameEntity
    {
        void UpdateHeat(int delta);
        void ConsumeMushroom();
        void ThrowRock();
        void Pick();
    }
}
