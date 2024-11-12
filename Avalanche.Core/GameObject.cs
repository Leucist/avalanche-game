using Avalanche.Core.Interfaces;

namespace Avalanche.Core
{
    public class GameObject : IGameObject
    {
        protected int[] _coords;

        public GameObject(int x, int y)
        {
            _coords = [x, y];
        }

        public int[] GetCoords() {
            return _coords;
        }

        public int GetX() {
            return _coords[0];
        }

        public int GetY() {
            return _coords[1];
        }

        public bool collidesWith(GameObject obj) {
            return _coords == obj.GetCoords();
        }
        public bool collidesWith(int x, int y) {
            return _coords[0] == x && _coords[1] == y;
        }
    }
}
