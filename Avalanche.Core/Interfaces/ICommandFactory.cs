namespace Avalanche.Core
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(ActionType action);
    }
}