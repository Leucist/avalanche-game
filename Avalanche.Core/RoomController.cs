using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalanche.Core
{
    public class RoomController : ISceneController
    {
        public RoomModel _model;

        public RoomController(int id, int enemiesCount) {
            _model = new RoomModel(id, enemiesCount);
        }

        public void Handle(ActionType action) {

        }

        public object GetModel() {
            return _model;
        }
    }
}
