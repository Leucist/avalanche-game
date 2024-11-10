namespace Avalanche.Core
{
    public interface IGameView
    {
        void Initialize();
        void Render();
        void AddView(GameStateType gameState, ISceneController controller);
    }
}