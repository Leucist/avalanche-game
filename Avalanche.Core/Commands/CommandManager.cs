namespace Avalanche.Core
{
    public class CommandManager
    {
        private static CommandManager? _instance;
        private readonly List<ICommand> _commands;

        public static CommandManager Instance => _instance ??= new CommandManager();

        private CommandManager() {
            _commands = new List<ICommand>();
        }

        public void AddCommand(ICommand command) {
            _commands.Add(command);
        }

        public void AddCommands(IEnumerable<ICommand> commands) {
            foreach (var command in commands) {
                _commands.Add(command);
            }
        }

        public void ExecuteAll() {
            foreach (var command in _commands) {
                command.Execute();
            }
            _commands.Clear();
        }
    }
}