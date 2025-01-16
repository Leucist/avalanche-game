namespace Avalanche.Core
{
    public class NameInputModel
    {
        private readonly Player _player;


        public NameInputModel(Player player) {
            _player = player;
        }

        public void Submit(string playerName) {
            _player.SetName(playerName);
        }
    }
}