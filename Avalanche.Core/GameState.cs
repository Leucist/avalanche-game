using Avalanche.Core.Enums;

namespace Avalanche.Core
{
    public static class GameState
    {
        // private static GameState? _instance;
        public static GameStateType _state { get; set; }
        public static CutsceneType _cutscene { get; set; }

        // private GameState() {
        //     _state = GameStateType.MainMenu;
        // }

        // public void SetState(GameStateType state) {
        //     _state = state;
        // }

        // public static GameState GetInstance() {
        //     if (_instance == null) {
        //         _instance = new GameState();
        //     }
        //     return _instance;
        // }
    }
}