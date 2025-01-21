namespace Avalanche.Core.Commands
{
    public class MoveEntityUp : ICommand
    {
        private readonly Entity _entity;

        public MoveEntityUp(Entity entity) {
            _entity = entity;
        }

        public void Execute() {
            _entity.Move(DirectionAxisType.Y, -1);
        }
    }
}