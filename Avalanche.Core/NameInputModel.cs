namespace Avalanche.Core
{
    public class NameInputModel
    {
        private readonly Player _player;
        public bool isNicknameNull {get; set; }


        public NameInputModel(Player player) {
            _player = player;
            isNicknameNull = true;            
        }

        public void Submit(string playerName) {
            isNicknameNull = false;
            _player.SetName(playerName);
        }
    }
}