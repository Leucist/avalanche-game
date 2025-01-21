namespace Avalanche.Core.Interfaces
{
    public interface IGameEntity : IGameObject
    {
        void Move();
        void TakeDamage(int damage);

    }
}
