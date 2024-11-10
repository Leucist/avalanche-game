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
        int _levelNumber;
        int _enemiesCount;

        public LevelModel(Player player, int levelNumber, int enemiesCount)
        {
            Player = player;
            _levelNumber = levelNumber;
            _enemiesCount = enemiesCount;
        }

        Dictionary<int, RoomController> ages = new Dictionary<int, RoomController>();




    }
}
