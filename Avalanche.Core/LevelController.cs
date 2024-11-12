namespace Avalanche.Core
{
    public class LevelController : ISceneController
    {

        LevelModel _model;
        // RoomController _roomController;
        public LevelController(Player player, int levelNumber)
        {
            _model = new LevelModel(player, levelNumber);
            _model.Reset(0);
            // _roomController = new RoomController();
        }

        void ISceneController.Handle(ActionType action)
        {
            switch (action) {
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
                    _model.Attack();
                    break;
                case ActionType.NullAction:
                    _model.SetPlayerIdle();
                    break;
            }
        }

        object ISceneController.GetModel()
        {
            return _model;
        }
        
    }
}
