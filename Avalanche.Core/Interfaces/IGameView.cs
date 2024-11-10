namespace Avalanche.Core
{
    public interface IGameView
    {
        void Initialize();
        void Render();
        void AddView(ISceneController controller);
    }
}