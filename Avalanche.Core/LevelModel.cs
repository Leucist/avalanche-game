using Avalanche.Core.Enums;

namespace Avalanche.Core
{
    public class LevelModel
    {
        public Player _player { get; set; }
        int _levelNumber;
        int _enemiesCount;
        private int _enemySightDistance;
        public Dictionary<int, RoomController> _rooms;
        public int _currentRoomID;
        public RoomModel? _currentRoom;
        public Door? _levelExit;
        private bool _isPaused;

        public bool IsPaused => _isPaused;

        public LevelModel(Player player, int levelNumber)
        {
            _player = player;
            _levelNumber = levelNumber;
            _enemiesCount = 4 * levelNumber + 3;
            _rooms = new Dictionary<int, RoomController>();
            _currentRoomID = 0;
            _isPaused = false;
            Reset(levelNumber);
        }

        public void Reset(int _levelNumber) {
            Random random = new Random();
            bool isForkedLastTime = false;
            // Generate random number of rooms in boundaries
            int roomsCount = random.Next(1, _levelNumber+1) * 2 + 2;

            // Generate Doors
            Dictionary<int, Dictionary<DoorPositioningType, Door>> roomDoors = [];
            for (int i = 0; i < roomsCount - 1; i++)
            {
                Door newDoor = new Door(i, i, i + 1, false);

                int chancesOfNextRoom = random.Next(1, 101);
                if ((i < roomsCount - 1) && chancesOfNextRoom < 50 && !isForkedLastTime)
                // with 50% chance we add a room that is not last, so we can add a fork in our path
                {
                    if (random.Next(1, 3) == 1)  // creates a right fork
                    {
                        if (!roomDoors.ContainsKey(i))
                            roomDoors[i] = new Dictionary<DoorPositioningType, Door>();
                        roomDoors[i][DoorPositioningType.East] = newDoor;

                        if (!roomDoors.ContainsKey(i + 1))
                            roomDoors[i + 1] = new Dictionary<DoorPositioningType, Door>();
                        roomDoors[i + 1][DoorPositioningType.West] = newDoor;
                        isForkedLastTime = true;
                    }
                    else  // creates a left fork
                    {
                        if (!roomDoors.ContainsKey(i))
                            roomDoors[i] = new Dictionary<DoorPositioningType, Door>();
                        roomDoors[i][DoorPositioningType.West] = newDoor;

                        if (!roomDoors.ContainsKey(i + 1))
                            roomDoors[i + 1] = new Dictionary<DoorPositioningType, Door>();
                        roomDoors[i + 1][DoorPositioningType.East] = newDoor;
                        isForkedLastTime = true;
                    }

                }
                else if (isForkedLastTime)
                // it means that new room always creates under previous 'main branch' room and takes 
                // penultimate id of door if last was created WITH a fork
                {
                    if (!roomDoors.ContainsKey(i - 1))
                        roomDoors[i - 1] = new Dictionary<DoorPositioningType, Door>();
                    roomDoors[i - 1][DoorPositioningType.South] = newDoor;

                    if (!roomDoors.ContainsKey(i + 1))
                        roomDoors[i + 1] = new Dictionary<DoorPositioningType, Door>();
                    roomDoors[i + 1][DoorPositioningType.North] = newDoor;
                    isForkedLastTime = false;
                }
                else
                {
                    // it means that new room always creates under previous 'main branch' room and takes 
                    // last id of door because last room was created WITHOUT a fork
                    if (!roomDoors.ContainsKey(i))
                        roomDoors[i] = new Dictionary<DoorPositioningType, Door>();
                    roomDoors[i][DoorPositioningType.South] = newDoor;

                    if (!roomDoors.ContainsKey(i + 1))
                        roomDoors[i + 1] = new Dictionary<DoorPositioningType, Door>();
                    roomDoors[i + 1][DoorPositioningType.North] = newDoor;
                    isForkedLastTime = false;


                }
            }

            if (isForkedLastTime)  // if we had a fork
            {
                Door levelExit = new Door(roomsCount-2, roomsCount-2, roomsCount-2, true);  // last room
                roomDoors[roomsCount-2][DoorPositioningType.South] = levelExit;
                _levelExit = levelExit;
            }
            else // // if we didn't have a fork
            {
                Door levelExit = new Door(roomsCount-1, roomsCount - 1, roomsCount - 1, true);  // last room
                roomDoors[roomsCount - 1][DoorPositioningType.South] = levelExit;
                _levelExit = levelExit;
            }



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

        public void MovePlayer(DirectionAxisType axis, int direction) {
            _player._directionAxis = axis;
            _player._direction = direction;

            // - Mark dirty pixels and turn on the 'isDirty' indicator
            _currentRoom!._dirtyPixels.Add([_player.GetX(), _player.GetY()]);
            _currentRoom._isDirty = true;

            // Change player position
            _player.Move();

            // Check doors-player collisions
            foreach (var door in _currentRoom._doors) {
                if (_player.CollidesWith(door.Key)) {
                    // - If door is level exit
                    if (door.Value._isLevelExit) {
                        // If it's the last level
                        if (_levelNumber == AppConstants.LevelsCount) {
                            GameState._cutscene = CutsceneType.GameFinish;
                            GameState._state = GameStateType.Cutscene;
                        }
                        Reset(++_levelNumber);
                        return;
                    }

                    // - If door isn't closed
                    if (!door.Value._isClosed) {
                        // - Player gets into another room
                        int[] roomCouple = door.Value._betweenRoomsOfID;
                        _currentRoomID = (roomCouple[0] == _currentRoomID) ? roomCouple[1] : roomCouple[0];
                        _currentRoom = _rooms[_currentRoomID]._model;
                        _currentRoom.Init();

                        // // Reset Player position
                        // GameObject nextDoor = _currentRoom._doors.First(d => d.Value._ID == door.Value._ID).Key;
                        // _player.MoveTo(
                        //     nextDoor.GetX() + 1,
                        //     nextDoor.GetY() + 1
                        // );
                    }
                }
            }

            // Handle walls collision
            _player.CheckColliders();
        }
        
        public void PlayerAttack() {
            int[] attackPoint = _player.GetFocusPoint();
            _currentRoom!.AttackPoint(attackPoint[0], attackPoint[1], _player._damage);
        }

        public void ConsumeMushroom() {
            _player.ConsumeMushroom();
        }

        public void Shoot() {
            if (_player.ThrowRock()) {
                _currentRoom!._otherEntities.Add(new Rock(_player));
            }
        }

        public void SetPlayerIdle() {
            _player._direction = 0;
        }

        public void Update() {
            // Do nothing if game is paused
            if (_isPaused) return;

            // Update in-room events except direct attacks
            _currentRoom!.Update();

            // - Handle Enemy attacks
            foreach (var enemy in _currentRoom._enemies) {
                // - Mark dirty pixels and turn on the 'isDirty' indicator
                _currentRoom!._dirtyPixels.Add([enemy.GetX(), enemy.GetY()]);
                _currentRoom._isDirty = true;
                
                // If enemy is dead, it's being erased from the list
                if (enemy.IsDead()) {
                    _currentRoom._enemies.Remove(enemy);
                }
                if (!enemy.IsAlerted) {
                    // Set enemy's focus on the player if player can be noticed
                    if (enemy.HasInSight(_player, _enemySightDistance)) {
                        enemy.SetFocusOn(_player);
                    }
                }
                // Manages enemy logic
                enemy.ManageAction();
                enemy.UpdateCooldown();
            }

            // Update player's attack cooldown counter
            _player.UpdateCooldown();

            // If the player is dead, game is over.
            if (_player.IsDead()) {
                GameState._cutscene = CutsceneType.GameOver;
                GameState._state = GameStateType.Cutscene;
            }

            // Player freezes
            _player.UpdateHeat();
        }

        public void SwitchPause() {
            _isPaused = !_isPaused;
            _currentRoom!._isDirty = true;
        }
    }
}
