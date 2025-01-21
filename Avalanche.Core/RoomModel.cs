using static Avalanche.Core.AppConstants;

namespace Avalanche.Core
{
    public class RoomModel
    {
        private Player _player;
        private int _id;
        private List<GameObject> _items;
        private List<Entity> _otherEntities;
        private List<Enemy> _enemies;
        private int _initialEnemiesCount;
        private HashSet<(int, int)> _enemyPositions;
        private HashSet<(int, int)> _itemPositions;
        private bool _isDirty;
        private List<int[]> _dirtyPixels;
        private Dictionary<GameObject, Door> _doors;
        private Campfire? _campfire;



        public Dictionary<GameObject, Door> Doors   =>  _doors;
        public List<GameObject> Items               =>  _items;
        public List<Entity> OtherEntities           =>  _otherEntities;
        public List<Enemy> Enemies                  =>  _enemies;
        public Campfire Campfire                    => _campfire;
        
        public int GetPlayerHealth()    =>  _player._health;
        public int GetPlayerHeat()      =>  _player._heat;
        public int GetPlayerMushrooms() =>  _player._mushrooms;
        public int GetPlayerRocks()     =>  _player._rocks;
        
        public List<int[]> GetDirtyPixels() => _dirtyPixels;
        public bool IsDirty() => _isDirty;
        public void MarkDirty() { _isDirty = true; }
        public void MarkClean() { _isDirty = false; }
        public void ClearDirtyPixels() { _dirtyPixels.Clear(); }

        public void RemoveItem(GameObject item) { _items.Remove(item); }


        public RoomModel(
            int id, 
            int enemiesCount, 
            Dictionary<CardinalDirectionType, Door> doors,
            Player player,
            Campfire? campfire = null
        ) {
            _id = id;
            _otherEntities = new List<Entity>();
            _enemies = new List<Enemy>(enemiesCount);
            _initialEnemiesCount = enemiesCount;
            _items = [];
            _enemyPositions = new HashSet<(int, int)>();
            _itemPositions = new HashSet<(int, int)>();
            _isDirty = true;
            _dirtyPixels = [];
            _doors = [];
            _campfire = campfire;
            _player = player;

            // Create GameObjects for the doors
            foreach (var door in doors) {
                int x = RoomCharWidth / 2;
                int y = RoomCharHeight / 2;
                switch (door.Key) {
                    case CardinalDirectionType.North:
                        y = -1;
                        break;
                    case CardinalDirectionType.East:
                        x = RoomCharWidth;
                        break;
                    case CardinalDirectionType.South:
                        y = RoomCharHeight;
                        break;
                    case CardinalDirectionType.West:
                        x = -1;
                        break;
                }
                _doors[new GameObject(x, y)] = door.Value;
            }

            Init();
        }

        private void Init() {
            FillEnemies();
            // FillEntities();
            FillItems();
        }

        private void FillEnemies() {
            Random random = new Random();

            // Fills _enemyPositions with random values within room borders
            while (_enemyPositions.Count < _initialEnemiesCount) {
                int x = random.Next(1, AppConstants.RoomCharWidth - 1);
                int y = random.Next(1, AppConstants.RoomCharHeight - 1);

                _enemyPositions.Add((x, y));
            }

            // - Creates enemies for the Room
            
            //Enemy enemyPrototype = new Enemy();

            //foreach (var coords in _enemyPositions)
            //{
            //    Enemy enemy = enemyPrototype.ShallowCopy();
            //    enemy.SetX(coords.Item1); enemy.SetY(coords.Item2);
            //    _enemies.Add(enemy);

            //}

            int difficultyModifier = (int) GameState._difficulty;

            Enemy enemyPrototype = new Enemy();
            enemyPrototype.SetDamage(DefaultEntityDamage * difficultyModifier);
            enemyPrototype.SetHealth(DefaultEntityHealth * difficultyModifier);

            foreach (var coords in _enemyPositions)
            {
                Enemy enemy = enemyPrototype.Copy();
                enemy.SetX(coords.Item1); enemy.SetY(coords.Item2);
                _enemies.Add(enemy);
            }
        }

        // public void FillEntities() {
        //     for (int i = 0; i < _otherEntities.Count; i++) {

        //     }
        // }

        private void FillItems()
        {
            Random random = new Random();
            int mushroomsCount = random.Next(1, 3);
            int rocksCount = random.Next(1, 3);

            while (_itemPositions.Count < mushroomsCount + rocksCount)
            {
                int x = random.Next(1, RoomCharWidth - 1);
                int y = random.Next(1, RoomCharHeight - 1);
                _itemPositions.Add((x, y));
            }

            int i = 0;
            foreach (var coords in _itemPositions)
            {
                if (i < mushroomsCount)
                {
                    _items.Add(new Mushroom(coords.Item1, coords.Item2));
                }
                else if (i < mushroomsCount + rocksCount)
                {
                    _items.Add(new LayingRock(coords.Item1, coords.Item2));
                }
                i++;
            }

            if (_campfire == null)
            {
                int x = random.Next(1, RoomCharWidth - 1);
                int y = random.Next(1, RoomCharHeight - 1);
                _campfire = new Campfire(x, y, _player);
                //     coords.Item2
                // ));
            }
        }

        public void AddDirtyPixel(int[] coords) {
            _dirtyPixels.Add(coords);
        }


        public void Update() {
            // If campfire is present in the room, then update it
            if (_campfire != null)
            {
                _campfire.UpdateCampfireState();
            }

            // - Open all doors (except level exit) in the room if there're no enemies left
            if (_enemies.Count == 0) {
                foreach (var door in _doors.Values)
                {
                    if (door._isLevelExit) {
                        continue;
                    }
                    door._isClosed = false;
                }
            }



            // - Move other entities
            for (int i = _otherEntities.Count-1; i >= 0; i--) {
                // - Mark dirty pixels and turn on the 'isDirty' indicator
                _dirtyPixels.Add([_otherEntities[i].GetX(), _otherEntities[i].GetY()]);
                _isDirty = true;

                _otherEntities[i].Move();

                // - Manage thrown Rock
                if (_otherEntities[i].GetType() == typeof(Rock)) {
                    Rock rock = (Rock) _otherEntities[i];
                    // if rock hits the wall
                    if (rock.CollidesWithWalls() != null) {
                        RemoveOtherEntity(rock);
                    } else {    // if rock hasn't been deleted
                        foreach (var enemy in _enemies) {
                            if (rock.CollidesWith(enemy)) {
                                enemy.TakeDamage(rock._damage);
                                RemoveOtherEntity(rock);
                            }
                        }
                    }
                }
            }
        }

        private void RemoveOtherEntity(Entity entity) {
            _otherEntities.Remove(entity);
        }

        public void AttackPoint(int x, int y, int damage) {
            foreach (var enemy in _enemies) {
                if (enemy.CollidesWith(x, y)) {
                    enemy.TakeDamage(damage);
                    enemy._wasHit = true;
                }
            }
        }

        public void AttackPoints(List<int[]> attackPoints, int damage) {
            foreach (var enemy in _enemies) {
                foreach (int[] point in attackPoints) {
                    if (enemy.CollidesWith(point[0], point[1])) {
                        enemy.TakeDamage(damage);
                        enemy._wasHit = true;
                    }
                }
            }
        }
    }
}
