namespace Avalanche.Core
{
    public interface ICommandFactoryProvider
    {
        ICommandFactory CreateFactory(GameStateType state, ISceneController controller);
    }
}