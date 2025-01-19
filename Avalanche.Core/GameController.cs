namespace Avalanche.Core;

public class GameController {
    IGameView _gameView;
    IInputController _inputController;
    ISceneController? _currentSceneController;
    // bool _isRunning;

    public GameController(IGameView gameView, IInputController inputController) {
        _gameView = gameView;
        _inputController = inputController;
        _currentSceneController = null;
        // _isRunning = true;
    }

    public void StartGame() {
        Dictionary<GameStateType, ISceneController> controllers = new Dictionary<GameStateType, ISceneController>();

        _gameView.Initialize();

        Player player = new Player();  

        // Initialise Scene Controllers
        controllers[GameStateType.MainMenu] = new MainMenuController();
        controllers[GameStateType.NameInput] = new NameInputController(player);
        controllers[GameStateType.Game] = new LevelController(player, 0);
        controllers[GameStateType.Cutscene] = new CutsceneController();

        // Initialise Scene Views & Link them to the main View
        _gameView.AddView(GameStateType.MainMenu, controllers[GameStateType.MainMenu]);
        _gameView.AddView(GameStateType.NameInput, controllers[GameStateType.NameInput]);
        _gameView.AddView(GameStateType.Game, controllers[GameStateType.Game]);
        _gameView.AddView(GameStateType.Cutscene, controllers[GameStateType.Cutscene]);

        // Clear input buffer
        _inputController.ClearBuffer();

        // Main Game loop
        GameState._state = GameStateType.MainMenu;      // sets MainMenu as start controller
        SoundGameManager.Play("TestAudio.wav", true);   // starts playing the first song on the loop
        while (GameState._state != GameStateType.Exit) {
            _currentSceneController = controllers[GameState._state];
            Update();
        }
    }

    private void Update() {
        _gameView.Render();
        ActionType action = _inputController.GetUserAction();
        _currentSceneController!.Handle(action);
    }
}
