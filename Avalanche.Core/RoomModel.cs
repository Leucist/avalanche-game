using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalanche.Core
{
    public class RoomModel
    {
        public int _id { get; set; }
        public List<Entity> _enemies { get; set; }
        private HashSet<(int, int)> _enemyPositions;
        private int _enemiesCount;
        // public int _doorsNumber;
        // public List<Door> _doors { get; set; }

        public RoomModel(int id, int enemiesCount/*, int doorsNumber*/) {
            _id = id;
            _enemies = new List<Entity>(enemiesCount);
            _enemyPositions = new HashSet<(int, int)>();
            //_doors = new List<Door>(doorsNumber);       
    }

        public void Init() {
            FillEnemies();
        }

        private void FillEnemies() {
            Random random = new Random();

            // Fills _enemyPositions with random values within room borders
            while (_enemyPositions.Count < _enemies.Count) {
                int x = random.Next(0, AppConstants.RoomCharWidth);
                int y = random.Next(0, AppConstants.RoomCharHeight);

                _enemyPositions.Add((x, y));
            }

            // Creates enemies for the Room
            int i = 0;
            foreach (var coords in _enemyPositions) {
                _enemies[i++] = new Entity(
                    coords.Item1,
                    coords.Item2
                );
            }
        }
    }
}
