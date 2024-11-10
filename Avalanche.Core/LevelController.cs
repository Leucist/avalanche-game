using System.Reflection.Metadata;

namespace Avalanche.Core
{
    public class LevelController : ISceneController
    {
<<<<<<< HEAD
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


   

=======
        public void Handle(ActionType action) {
>>>>>>> e51b7a0e869aed422b16db8638e6072340a1a7b2

        }

        public object GetModel() {
            return null;
        }
    }
}
