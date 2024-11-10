using System.Reflection.Metadata;

namespace Avalanche.Core
{
    public class LevelController : ISceneController
    {

        LevelModel _model;
        RoomController _roomController;
        public LevelController(Player player)
        {
            _model = new LevelModel(player);
            _roomController = new RoomController();
        }

        void ISceneController.Handle(ActionType action)
        {
            
        }

        object ISceneController.GetModel()
        {
            return _model;
        }
        
    }
}
