namespace Avalanche.Core
{
    public class LevelModel
    {
        public Player _player { get; set; }
        int _levelNumber;
        int _enemiesCount;
        public Dictionary<int, RoomController> _rooms;
        public int _currentRoomID;
        public RoomModel? _currentRoom;
        public Door? _levelExit;

        public LevelModel(Player player, int levelNumber)
        {
            _player = player;
            _levelNumber = levelNumber;
            _enemiesCount = 4 * levelNumber + 3;
            _rooms = new Dictionary<int, RoomController>();
            _currentRoomID = 0;
            Reset(levelNumber);
        }

        public void Reset(int _levelNumber) {
            Random random = new Random();
            // Generate random number of rooms in boundaries
            int roomsCount = random.Next(1, _levelNumber+1) * 2 + 2;

            // Generate Doors
            Dictionary<int, Dictionary<DoorPositioningType, Door>> roomDoors = [];
            for (int i = 0; i < roomsCount-1; i++) {
                Door newDoor = new Door(i, i+1, false);

                if (!roomDoors.ContainsKey(i))
                    roomDoors[i] = new Dictionary<DoorPositioningType, Door>();
                roomDoors[i][DoorPositioningType.South] = newDoor;

                if (!roomDoors.ContainsKey(i+1))
                    roomDoors[i+1] = new Dictionary<DoorPositioningType, Door>();
                roomDoors[i+1][DoorPositioningType.North] = newDoor;
            }
            Door levelExit = new Door(roomsCount-1, roomsCount-1, true);
            roomDoors[roomsCount-1][DoorPositioningType.South] = levelExit;
            _levelExit = levelExit;


            // Create rooms
            int enemiesToAdd, enemiesAdded = 0;
            for (int i = roomsCount - 1; i >= 0; i--) {
                enemiesToAdd = _enemiesCount - enemiesAdded > 0 ? _enemiesCount / (roomsCount - 1) : 0;
                _rooms[i] = new RoomController(i, enemiesToAdd, roomDoors[i]);
                enemiesAdded -= enemiesToAdd;
            }

            _currentRoomID = 0;
            _currentRoom = _rooms[_currentRoomID]._model;

            _currentRoom.Init();
        }

        // private void generateRoomConnections(int roomsCount) {
        //     Dictionary
        //     Random random = new Random();

        //     for (int i = 0; i < roomsCount; i++) {
        //         List<Door> doors = new();

        //         int doorsInRoom = random.Next(1, 4);
        //         for (int j = 0; i < doorsInRoom; i++) {

        //         }
                
        //     }
        // }



        // public void BuildMap() {
        //     int[][] map = new int[AppConstants.RoomCharHeight][];
        // }

        public void MovePlayer(DirectionAxisType axis, int direction) {
            _player._directionAxis = axis;
            _player._direction = direction;

            // Mark dirty pixels and turn on the 'isDirty' indicator
            _currentRoom!._dirtyPixels.Add([_player.GetX(), _player.GetY()]);
            _currentRoom._isDirty = true;

            // Change player position
            _player.Move();

            // Check door collision
            foreach (var door in _currentRoom._doors) {
                if (_player.collidesWith(door.Key)) {
                    // If door is level exit
                    if (door.Value._isLevelExit) {
                        Reset(++_levelNumber);
                        return;
                    }

                    // If door isn't closed
                    if (!door.Value._isClosed) {
                        // Player gets into another room
                        int[] roomCouple = door.Value._betweenRoomsOfID;
                        _currentRoomID = roomCouple[0] == _currentRoomID ? roomCouple[1] : roomCouple[0];
                        _currentRoom = _rooms[_currentRoomID]._model;
                        _currentRoom.Init();
                    }
                }
            }

            // Handle walls collision
            _player.CheckColliders();
        }
        
        public void Attack() {
            _currentRoom!.Update();
        }

        public void ConsumeMushroom() {
            _player.ConsumeMushroom();
        }

        public void Shoot() {
            _player.ThrowRock();
            _currentRoom._entities.Add(new Rock(_player));
        }

        public void SetPlayerIdle() {
            _player._direction = 0;
        }
    }
}
