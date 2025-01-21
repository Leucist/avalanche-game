using Avalanche.Core.Commands;

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

        public DateTime _buttonPressed;

        void ISceneController.Handle(ActionType action)
        {
            if (_model.IsPaused && action != ActionType.Escape) return; // Quick hack


            if ((DateTime.Now - _buttonPressed).TotalSeconds >= 0.08)
            {
                List<ICommand> commands = new List<ICommand>();
                switch (action)
                {
                    case ActionType.NullAction:
                        // _model.SetPlayerIdle();
                        break;
                    case ActionType.Up:
                        // commands.Add(new MoveEntity(_model.Player));
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
                        _buttonPressed = DateTime.Now;
                        break;
                    case ActionType.Shoot:
                        _model.Shoot();
                        _buttonPressed = DateTime.Now;
                        break;
                    case ActionType.Escape:
                        _model.SwitchPause();
                        break;
                }
                CommandManager.Instance.AddCommands(commands);
                CommandManager.Instance.ExecuteAll();

                _model.Update();
            }
        }

        object ISceneController.GetModel()
        {
            return _model;
        }
        
    }
}
