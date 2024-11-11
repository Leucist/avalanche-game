namespace Avalanche.Core
{
    public class LevelModel
    {
        public Player _player { get; set; }
        int _levelNumber;
        int _enemiesCount;
        Dictionary<int, RoomController> _rooms;
        private int _currentRoom;

        public LevelModel(Player player, int levelNumber)
        {
            _player = player;
            _levelNumber = levelNumber;
            _enemiesCount = 4 * levelNumber + 3;
            _rooms = new Dictionary<int, RoomController>();
            _currentRoom = 0;
        }

        public void Reset(int _levelNumber) {
            Random random = new Random();
            // Generate random number of rooms in boundaries
            int roomsCount = random.Next(1, _levelNumber) * 2 + 2;

            // Create rooms
            int enemiesToAdd, enemiesAdded = 0;
            for (int i = roomsCount; i > 0; i--) {
                enemiesToAdd = _enemiesCount - enemiesAdded > 0 ? _enemiesCount / (roomsCount - 1) : 0;
                _rooms[i] = new RoomController(i, enemiesToAdd);
                enemiesAdded -= enemiesToAdd;
            }

            _currentRoom = 0;
        }



        // public void BuildMap() {
        //     int[][] map = new int[AppConstants.RoomCharHeight][];
        // }

        public void MovePlayer(DirectionAxisType axis, int direction) {
            _player._directionAxis = axis;
            _player._direction = direction;
            _player.Move();
        }

        public void SetPlayerIdle() {
            _player._direction = 0;
        }
    }
}
