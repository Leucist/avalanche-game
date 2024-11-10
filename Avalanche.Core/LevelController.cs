using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
