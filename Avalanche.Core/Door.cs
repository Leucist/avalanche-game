namespace Avalanche.Core
{
    public class Door
    {
        public bool _isClosed;
        public readonly bool _isLevelExit;
        public int[] _betweenRoomsOfID;

        public Door(int id1, int id2, bool isLevelExit) {
            _isClosed = false;
            _isLevelExit = isLevelExit;
            _betweenRoomsOfID = [id1, id2];
        }
    }
}