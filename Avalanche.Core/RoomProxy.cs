namespace Avalanche.Core
{
    public class RoomProxy {
        private readonly Lazy<RoomModel> _room;

        // private Player _player;
        // private int _id;
        // private int _initialEnemiesCount;
        // private Dictionary<CardinalDirectionType, Door> _doors;
        // private Campfire? _campfire;

        public RoomModel Room => _room.Value;

        public RoomProxy (
            int id, 
            int enemiesCount, 
            Dictionary<CardinalDirectionType, Door> doors,
            Player player,
            Campfire? campfire = null
        ) {
            // _id = id;
            // _initialEnemiesCount = enemiesCount;
            // _doors = doors;
            // _player = player;
            // _campfire = campfire;
            _room = new Lazy<RoomModel>(() => new RoomModel(id, enemiesCount, doors, player, campfire));
        }

        // public void Init() => Room.Init();
        // public void Update() => Room.Update();

        // public void AttackPoint(int x, int y, int damage) => Room.AttackPoint(x, y, damage);

        // public void AttackPoints(List<int[]> attackPoints, int damage) => Room.AttackPoints(attackPoints, damage);
    }
}