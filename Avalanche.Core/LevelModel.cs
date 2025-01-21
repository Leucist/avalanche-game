namespace Avalanche.Core
{
    public class LevelModel
    {
        public Player _player { get; set; }
        int _levelNumber;
        int _enemiesCount;
        private int _enemySightDistance;
        public Dictionary<int, RoomProxy> _rooms;
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
            _rooms = [];

            // - Params initialisation, get rewritten in ResetParams()
            _enemiesCount = 0;
            _enemySightDistance = 1;

            _isPaused = false;

            Reset(levelNumber);
        }

        private void ResetLevelParams(int levelNumber) {
            _levelNumber = levelNumber;
            _rooms = new Dictionary<int, RoomProxy>();
            _currentRoomID = 0;

            // Use Game Difficulty level to enhance the challenge
            int difficultyModifier = (int) GameState._difficulty;
            _enemiesCount = (4 * levelNumber + 3) * difficultyModifier;
            _enemySightDistance = AppConstants.DefaultEnemySightDistance * difficultyModifier;

            _isPaused = false;
        }

        public void Reset(int levelNumber) {
            ResetLevelParams(levelNumber);
            _player.Reset(levelNumber == 0);    // full player reset if the level number is 0

            Random random = new Random();
            // Generate random number of rooms in boundaries
            int roomsCount = random.Next(1, _levelNumber+1) * 2 + 2;

            // Generate Doors
            int bufferDoorID = 0;
            Dictionary<int, Dictionary<CardinalDirectionType, Door>> roomDoors = [];
            for (int roomNumber = 0; roomNumber < roomsCount-1; roomNumber++) {
                Door newDoor = new Door(bufferDoorID++, roomNumber, roomNumber+1, false);

                if (!roomDoors.ContainsKey(roomNumber))
                    roomDoors[roomNumber] = new Dictionary<CardinalDirectionType, Door>();
                roomDoors[roomNumber][CardinalDirectionType.South] = newDoor;

                if (!roomDoors.ContainsKey(roomNumber+1))
                    roomDoors[roomNumber+1] = new Dictionary<CardinalDirectionType, Door>();
                roomDoors[roomNumber+1][CardinalDirectionType.North] = newDoor;
            }
            Door levelExit = new Door(bufferDoorID, roomsCount-1, roomsCount-1, true);
            roomDoors[roomsCount-1][CardinalDirectionType.South] = levelExit;
            _levelExit = levelExit;



            // Create rooms
            int enemiesToAdd, enemiesAdded = 0;
            for (int i = roomsCount - 1; i >= 0; i--) {
                enemiesToAdd = _enemiesCount - enemiesAdded > 0 ? _enemiesCount / (roomsCount - 1) : 0;
                _rooms[i] = new RoomProxy(i, enemiesToAdd, roomDoors[i], _player);

                enemiesAdded -= enemiesToAdd;
            }

            _currentRoomID = 0;
            _currentRoom = _rooms[_currentRoomID].Room;
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
            _currentRoom!.AddDirtyPixel(prevCoords);
            _currentRoom.MarkDirty();

            // Change player position
            _player.Move();

            // Check doors-player collisions
            foreach (var door in _currentRoom.Doors) {
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
                        _currentRoom = _rooms[_currentRoomID].Room;

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

            for (int i = _currentRoom.Items.Count - 1; i >= 0; i--) {
                var item = _currentRoom.Items[i];
                if (_player.CollidesWith(item)) {
                    switch (item) {
                        case LayingRock _:
                            _player.AddItem(ItemType.Rock);
                            break;
                        case Mushroom _:
                            _player.AddItem(ItemType.Mushroom);
                            break;
                    }
                    _currentRoom.RemoveItem(item);
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
                _currentRoom!.OtherEntities.Add(new Rock(_player));
            }
        }

        public void SetPlayerIdle() {
            _player._direction = 0;
        }

        private void CheckCollisions(Entity movingEntity, int[] prevCoords) {
            foreach (var enemy in _currentRoom!.Enemies) {
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
            for (int i = _currentRoom.Enemies.Count - 1; i >= 0; i--) {
                var enemy = _currentRoom.Enemies[i];
                // - Mark dirty pixels and turn on the 'isDirty' indicator
                _currentRoom!.AddDirtyPixel([enemy.GetX(), enemy.GetY()]);
                _currentRoom.MarkDirty();
                
                // If enemy is dead, it's being erased from the list
                if (enemy.IsDead()) {
                    _currentRoom.Enemies.Remove(enemy);
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
            _currentRoom!.MarkDirty();
        }

        private void ResetGame(CutsceneType cutscene) {
            GameState._state = GameStateType.Cutscene;
            GameState._cutscene = cutscene;
            Reset(0);
        }
    }
}
