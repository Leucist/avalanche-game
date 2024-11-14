using static Avalanche.Core.AppConstants;

namespace Avalanche.Core
{
    public class RoomModel
    {
        public int _id { get; set; }
        public List<GameObject> _items { get; set; }
        public List<Entity> _otherEntities { get; set; }
        public List<Enemy> _enemies { get; set; }
        private HashSet<(int, int)> _enemyPositions;
        private HashSet<(int, int)> _itemPositions;
        // private int _enemiesCount;
        public bool _isDirty { get; set; }
        public List<int[]> _dirtyPixels;
        public Dictionary<GameObject, Door> _doors { get; set; }

        public RoomModel(int id, int enemiesCount, Dictionary<DoorPositioningType, Door> doors) {
            _id = id;
            _otherEntities = new List<Entity>();
            _enemies = new List<Enemy>(enemiesCount);
            _items = [];
            _enemyPositions = new HashSet<(int, int)>();
            _itemPositions = new HashSet<(int, int)>();
            _isDirty = true;
            _dirtyPixels = [];
            _doors = [];

            // Create GameObjects for the doors
            foreach (var door in doors) {
                int x = RoomCharWidth / 2;
                int y = RoomCharHeight / 2;
                switch (door.Key) {
                    case DoorPositioningType.North:
                        y = RoomDefaultY;
                        break;
                    case DoorPositioningType.East:
                        x = RoomDefaultX + RoomCharWidth;
                        break;
                    case DoorPositioningType.South:
                        y = RoomDefaultY + RoomCharHeight;
                        break;
                    case DoorPositioningType.West:
                        x = RoomDefaultX;
                        break;
                }
                _doors[new GameObject(x, y)] = door.Value;
            }
        }

        public void Init() {
            FillEnemies();
            // FillEntities();
            FillItems();
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
                _enemies[i++] = new Enemy(
                    coords.Item1,
                    coords.Item2
                );
            }
        }

        // public void FillEntities() {
        //     for (int i = 0; i < _otherEntities.Count; i++) {

        //     }
        // }

        public void FillItems() {
            Random random = new Random();
            int mushroomsCount = random.Next(1, 2);
            int rocksCount = random.Next(1, 2);

            // Fills _itemPositions with random values within room borders
            while (_itemPositions.Count < mushroomsCount + rocksCount) {
                int x = random.Next(0, AppConstants.RoomCharWidth);
                int y = random.Next(0, AppConstants.RoomCharHeight);

                _itemPositions.Add((x, y));
            }

            // Creates items for the Room
            int i = 0;
            foreach (var coords in _itemPositions) {
                // if (i < mushroomsCount) {
                //     _items[i++] = new GameObject(
                //         coords.Item1,
                //         coords.Item2
                //     );
                // }
                // else {
                //     _items[i++] = new GameObject(
                //         coords.Item1,
                //         coords.Item2
                //     );
                // }
                _items.Add(new GameObject(
                    coords.Item1,
                    coords.Item2
                ));
            }
        }

        public void Update() {
            if (_enemies.Count == 0) {
                foreach (var door in _doors.Values)
                {
                    if (door._isLevelExit) {
                        continue;
                    }
                    door._isClosed = false;
                }
            }

            foreach (var entity in _otherEntities) {
                entity.Move();
                if (entity.GetType() == typeof(Rock)) {
                    foreach (var enemy in _enemies) {
                        if (entity.CollidesWith(enemy)) {
                            enemy.TakeDamage(entity._damage);
                            _otherEntities.Remove(entity);
                        }
                    }
                }
            }

            foreach (var enemy in _enemies) {
                enemy.RandomMovement();

                if (enemy.IsDead()) {
                    _enemies.Remove(enemy);
                }
            }
        }

        public void AttackPoint(int x, int y, int damage) {
            foreach (var enemy in _enemies) {
                if (enemy.CollidesWith(x, y)) {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}
