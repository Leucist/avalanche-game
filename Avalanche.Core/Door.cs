namespace Avalanche.Core
{
    public class Door
    {
        public int _ID;
        public bool _isClosed;
        public readonly bool _isLevelExit;
        public int[] _betweenRoomsOfID;

        public Door(int doorID, int id1, int id2, bool isLevelExit) {
            _ID = doorID;
            _isClosed = true;
            _isLevelExit = isLevelExit;
            _betweenRoomsOfID = [id1, id2];
        }
    }
}