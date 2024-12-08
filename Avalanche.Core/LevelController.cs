namespace Avalanche.Core
{
    public class LevelController : ISceneController
    {

        LevelModel _model;
        // RoomController _roomController;
        public LevelController(Player player, int levelNumber)
        {
            // if (levelNumber == 0) player.Reset();
            _model = new LevelModel(player, levelNumber);
            _model.Reset(0);
            // _roomController = new RoomController();
        }

        void ISceneController.Handle(ActionType action)
        {
            if (_model.IsPaused && action != ActionType.Escape) return; // Quick hack
            switch (action) {
                // case ActionType.NullAction:
                //     _model.SetPlayerIdle();
                //     break;
                case ActionType.Up:
                    _model.MovePlayer(DirectionAxisType.Y, -1);
                    break;
                case ActionType.Down:
                    _model.MovePlayer(DirectionAxisType.Y, 1);
                    break;
                case ActionType.Right:
                    _model.MovePlayer(DirectionAxisType.X, 1);
                    break;
                case ActionType.Left:
                    _model.MovePlayer(DirectionAxisType.X, -1);
                    break;
                case ActionType.StraightAttack:
                    _model.PlayerAttack();
                    break;
                case ActionType.ConsumeMushroom:
                    _model.ConsumeMushroom();
                    break;
                case ActionType.Shoot:
                    _model.Shoot();
                    break;
                case ActionType.Escape:
                    _model.SwitchPause();
                    break;
            }
            _model.Update();
        }

        object ISceneController.GetModel()
        {
            return _model;
        }
        
    }
}
