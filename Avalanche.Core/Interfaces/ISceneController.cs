namespace Avalanche.Core
{
    public interface ISceneController
    {
        void Handle(ActionType action);
        object GetModel();
    }
}