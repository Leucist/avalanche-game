namespace Avalanche.Core
{
    public class RoomController : ISceneController
    {
        public RoomModel _model;

        public RoomController(int id, int enemiesCount, Dictionary<DoorPositioningType, Door> doors) {
            _model = new RoomModel(id, enemiesCount, doors);
        }

        public void Handle(ActionType action) {

        }

        public object GetModel() {
            return _model;
        }
    }
}
