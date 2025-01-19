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
        public int LevelNumber => _levelNumber;

        public LevelModel(Player player, int levelNumber)
        {
            _player = player;
            _levelNumber = levelNumber;
            _enemiesCount = 4 * levelNumber + 3;
            _enemySightDistance = AppConstants.DefaultEnemySightDistance;
            _rooms = new Dictionary<int, RoomController>();
            _isPaused = false;
            Reset(levelNumber);
        }

        public void Reset(int levelNumber) {
            _levelNumber = levelNumber;
            _player.Reset(levelNumber == 0);    // full player reset if the level number is 0

            // - Damn repetative -
            // TODO Level Controller manages level instances or whatever, but not this monstrousity.
            _enemiesCount = 4 * levelNumber + 3;
            _enemySightDistance = AppConstants.DefaultEnemySightDistance;
            _rooms = new Dictionary<int, RoomController>();
            _currentRoomID = 0;
            _isPaused = false;
            // - - -

            Random random = new Random();
            bool isForkedLastTime = false;
            // Generate random number of rooms in boundaries
            int roomsCount = random.Next(1, _levelNumber+1) * 2 + 2;

            // Generate Doors
            Dictionary<int, Dictionary<CardinalDirectionType, Door>> roomDoors = [];
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
                            roomDoors[i] = new Dictionary<CardinalDirectionType, Door>();
                        roomDoors[i][CardinalDirectionType.East] = newDoor;

                        if (!roomDoors.ContainsKey(i + 1))
                            roomDoors[i + 1] = new Dictionary<CardinalDirectionType, Door>();
                        roomDoors[i + 1][CardinalDirectionType.West] = newDoor;
                        isForkedLastTime = true;
                    }
                    else  // creates a left fork
                    {
                        if (!roomDoors.ContainsKey(i))
                            roomDoors[i] = new Dictionary<CardinalDirectionType, Door>();
                        roomDoors[i][CardinalDirectionType.West] = newDoor;

                        if (!roomDoors.ContainsKey(i + 1))
                            roomDoors[i + 1] = new Dictionary<CardinalDirectionType, Door>();
                        roomDoors[i + 1][CardinalDirectionType.East] = newDoor;
                        isForkedLastTime = true;
                    }

                }
                else if (isForkedLastTime)
                // it means that new room always creates under previous 'main branch' room and takes 
                // penultimate id of door if last was created WITH a fork
                {
                    if (!roomDoors.ContainsKey(i - 1))
                        roomDoors[i - 1] = new Dictionary<CardinalDirectionType, Door>();
                    roomDoors[i - 1][CardinalDirectionType.South] = newDoor;

                    if (!roomDoors.ContainsKey(i + 1))
                        roomDoors[i + 1] = new Dictionary<CardinalDirectionType, Door>();
                    roomDoors[i + 1][CardinalDirectionType.North] = newDoor;
                    isForkedLastTime = false;
                }
                else
                {
                    // it means that new room always creates under previous 'main branch' room and takes 
                    // last id of door because last room was created WITHOUT a fork
                    if (!roomDoors.ContainsKey(i))
                        roomDoors[i] = new Dictionary<CardinalDirectionType, Door>();
                    roomDoors[i][CardinalDirectionType.South] = newDoor;

                    if (!roomDoors.ContainsKey(i + 1))
                        roomDoors[i + 1] = new Dictionary<CardinalDirectionType, Door>();
                    roomDoors[i + 1][CardinalDirectionType.North] = newDoor;
                    isForkedLastTime = false;


                }
            }

            if (isForkedLastTime)  // if we had a fork
            {
                Door levelExit = new Door(roomsCount-2, roomsCount-2, roomsCount-2, true);  // last room
                roomDoors[roomsCount-2][CardinalDirectionType.South] = levelExit;
                _levelExit = levelExit;
            }
            else // // if we didn't have a fork
            {
                Door levelExit = new Door(roomsCount-1, roomsCount - 1, roomsCount - 1, true);  // last room
                roomDoors[roomsCount - 1][CardinalDirectionType.South] = levelExit;
                _levelExit = levelExit;
            }



            // Create rooms
            int enemiesToAdd, enemiesAdded = 0;
            for (int i = roomsCount - 1; i >= 0; i--) {
                enemiesToAdd = _enemiesCount - enemiesAdded > 0 ? _enemiesCount / (roomsCount - 1) : 0;
                _rooms[i] = new RoomController(i, enemiesToAdd, roomDoors[i], _player);

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

            // Get player coords before actual movement
            int[] prevCoords = _player.GetCoords();

            // - Mark dirty pixels and turn on the 'isDirty' indicator
            _currentRoom!._dirtyPixels.Add(prevCoords);
            _currentRoom._isDirty = true;

            // Change player position
            _player.Move();

            // Check doors-player collisions
            foreach (var door in _currentRoom._doors) {
                if (_player.CollidesWith(door.Key)) {

                    // If door isn't closed
                    if (!door.Value._isClosed) {

                        // If door is Level Exit
                        if (door.Value._isLevelExit) {

                            // If it's the Last Level
                            if (_levelNumber == AppConstants.LevelsCount) {
                                ResetGame(CutsceneType.GameFinish);
                            }
                            else {
                                Reset(++_levelNumber);
                            }
                            
                            return;
                        }

                        // - Player gets into another room
                        int[] roomCouple = door.Value._betweenRoomsOfID;
                        _currentRoomID = (roomCouple[0] == _currentRoomID) ? roomCouple[1] : roomCouple[0];
                        _currentRoom = _rooms[_currentRoomID]._model;
                        _currentRoom.Init();

                        if (_player.Name.ToLower() == "engineer") {
                            GameState._state = GameStateType.Cutscene;
                            GameState._cutscene = CutsceneType.GameFinish;
                            return;
                        }

                        // // Reset Player position
                        // GameObject nextDoor = _currentRoom._doors.First(d => d.Value._ID == door.Value._ID).Key;
                        // _player.MoveTo(
                        //     nextDoor.GetX() + 1,
                        //     nextDoor.GetY() + 1
                        // );
                    }
                }
            }

            for (int i = _currentRoom._items.Count - 1; i >= 0; i--) {
                var item = _currentRoom._items[i];
                if (_player.CollidesWith(item)) {
                    switch (item) {
                        case LayingRock _:
                            _player.AddItem(ItemType.Rock);
                            break;
                        case Mushroom _:
                            _player.AddItem(ItemType.Mushroom);
                            break;
                    }
                    _currentRoom._items.Remove(item);
                }
            }

            CheckCollisions(_player, prevCoords);

            // Handle walls collision
            _player.CheckColliders();
        }
        
        public void PlayerAttack() {
            // int[] attackPoint = _player.GetFocusPoint();
            List<int[]> attackPoints = new List<int[]>();
            for (int y = _player.GetY()-1; y <= _player.GetY()+1; y++) {
                for (int x = _player.GetX()-1; x <= _player.GetX()+1; x++) {
                    attackPoints.Add([x, y]);
                }
            }
            
            _currentRoom!.AttackPoints(attackPoints, _player._damage);
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

        private void CheckCollisions(Entity movingEntity, int[] prevCoords) {
            foreach (var enemy in _currentRoom!._enemies) {
                // To avoid checking collision with itself
                if (enemy == movingEntity) continue;

                // If coords are the same (entities collide)
                if (movingEntity.CollidesWith(enemy)) {
                    movingEntity.MoveTo(prevCoords[0], prevCoords[1]);
                }
            }

            // Check collision with the player
            if (movingEntity != _player) {
                // If coords are the same (entities collide)
                if (movingEntity.CollidesWith(_player)) {
                    movingEntity.MoveTo(prevCoords[0], prevCoords[1]);
                }
            }
        }

        public void Update() {
            // Do nothing if game is paused
            if (_isPaused) return;

            // Update in-room events except direct attacks
            _currentRoom!.Update();

            // - Handle Enemy attacks
            for (int i = _currentRoom._enemies.Count - 1; i >= 0; i--) {
                var enemy = _currentRoom._enemies[i];
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

                // Get enemy coords before possible movement
                int[] prevCoords = enemy.GetCoords();

                // Manages enemy logic
                enemy.ManageAction();

                CheckCollisions(enemy, prevCoords);

                enemy.UpdateCooldown();
            }

            // If the player is dead, game is over.
            if (_player.IsDead()) {
                ResetGame(CutsceneType.GameOver);
                return;
            }

            else {
                // Update player's attack cooldown counter
                _player.UpdateCooldown();

                // Player freezes
                _player.UpdateHeat();
                _player.Regenerate();
            }
        }

        public void SwitchPause() {
            _isPaused = !_isPaused;
            _currentRoom!._isDirty = true;
        }

        private void ResetGame(CutsceneType cutscene) {
            GameState._state = GameStateType.Cutscene;
            GameState._cutscene = cutscene;
            Reset(0);
        }
    }
}
