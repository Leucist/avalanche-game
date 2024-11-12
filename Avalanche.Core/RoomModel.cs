using static Avalanche.Core.AppConstants;

namespace Avalanche.Core
{
    public class RoomModel
    {
        public int _id { get; set; }
        public List<Entity> _enemies { get; set; }
        private HashSet<(int, int)> _enemyPositions;
        private int _enemiesCount;
        public bool _isDirty { get; set; }
        public List<int[]> _dirtyPixels;
        public Door _levelExit;
        public Dictionary<GameObject, Door> _doors { get; set; }

        public RoomModel(int id, int enemiesCount, Dictionary<DoorPositioningType, Door> doors) {
            _id = id;
            _enemies = new List<Entity>(enemiesCount);
            _enemyPositions = new HashSet<(int, int)>();
            _isDirty = true;
            _dirtyPixels = [];
            _doors = [];
            _levelExit = doors.Values.First(d => d._isLevelExit == true);

            // Create GameObjects for the doors
            foreach (var door in doors) {
                int x = RoomCharWidth / 2;
                int y = RoomCharHeight / 2;
                switch (door.Key) {
                    case DoorPositioningType.North:
                        y = RoomDefaultY;
                        break;
                    case DoorPositioningType.East:
                        x = RoomDefaultX;
                        break;
                    case DoorPositioningType.South:
                        y = RoomDefaultY + RoomCharHeight;
                        break;
                    case DoorPositioningType.West:
                        x = RoomDefaultX + RoomCharWidth;
                        break;
                }
                _doors[new GameObject(x, y)] = door.Value;
            }
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

        public void Update() {
            if (_enemiesCount == 0) {
                foreach (var door in _doors.Values)
                {
                    if (door._isLevelExit) {
                        continue;
                    }
                    door._isClosed = false;
                }
            }
        }
    }
}
