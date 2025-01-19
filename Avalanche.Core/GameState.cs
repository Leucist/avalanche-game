namespace Avalanche.Core
{
    public static class GameState
    {
        // private static GameState? _instance;

        // private GameStateType _state;
        // private CutsceneType _cutscene;
        // private List<IView> _views;
        // private List<ISceneController> _controllers;
        // private int _currentController;
        // private int _currentView;

        // public ISceneController GetController => _controllers[_currentController];
        // public IView GetView => _views[_currentView];

        // private GameState() {
        //     _currentController = 0;
        //     _currentView = 0;
        // }

        // public void SetState (GameStateType state) {
        //     _state = state;
        // }

        // public void SetCutscene (CutsceneType scene) {
        //     _cutscene = scene;
        // }

        // public static GameState GetInstance () {
        //     if (_instance == null) {
        //         _instance = new GameState();
        //     }
        //     return _instance;
        // }
        
        public static GameStateType _state { get; set; }
        public static CutsceneType _cutscene { get; set; }


        // NOTE: Added for the NameInput proper work in console mode after changing the controller for graphics
        public static GameModeType _mode { get; set; }

        public static DifficultyLevelType _difficulty = DifficultyLevelType.Normal;
    }
}