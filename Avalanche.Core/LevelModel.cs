using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Avalanche.Core
{
    public class LevelModel
    {
        public Player Player { get; set; }
        public LevelModel(Player player)
        {
            Player = player;
        }

        Dictionary<int, RoomController> ages = new Dictionary<int, RoomController>();
    }
}
