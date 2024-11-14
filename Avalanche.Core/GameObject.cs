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
            // return [_coords[0], _coords[1]];
        }

        public int GetX() {
            return _coords[0];
        }

        public int GetY() {
            return _coords[1];
        }

        public void SetX(int x)
        {
            _coords[0] = x;
        }

        public void SetY(int y)
        {
            _coords[1] = y;
        }

        public bool CollidesWith(GameObject obj) {
            return _coords == obj.GetCoords();
        }
        public bool CollidesWith(int x, int y) {
            return _coords[0] == x && _coords[1] == y;
        }
        public bool HasInSight(GameObject target, int sightDistance) {
            // Get target coords
            int targetX = target.GetX();
            int targetY = target.GetY();

            // Calculate the square of the distance using the Pythagorean theorem
            int dx = GetX() - targetX;
            int dy = GetY() - targetY;
            int distanceSquared = dx * dx + dy * dy;

            // Check if the squared distance is less than the square of the maximum visibility
            return distanceSquared <= sightDistance * sightDistance;
        }
    }
}
