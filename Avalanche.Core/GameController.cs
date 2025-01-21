namespace Avalanche.Core;

public class GameController {
    IGameView _gameView;
    IInputController _inputController;
    ISceneController? _currentSceneController;
    CommandFactory _commandFactory;

    public GameController(IGameView gameView, IInputController inputController) {
        _gameView = gameView;
        _inputController = inputController;
        _currentSceneController = null;
        _commandFactory = new();
    }

    public void StartGame() {
        Dictionary<GameStateType, ISceneController> controllers = new Dictionary<GameStateType, ISceneController>();

        _gameView.Initialize();

        Player player = new Player();  

        // Initialise Scene Controllers
        controllers[GameStateType.MainMenu]     =   new MainMenuController();
        controllers[GameStateType.OptionsMenu]  =   new OptionsController();
        controllers[GameStateType.NameInput]    =   new NameInputController(player);
        controllers[GameStateType.Game]         =   new LevelController(player, 0);
        controllers[GameStateType.Cutscene]     =   new CutsceneController();

        _commandFactory.AddCommandFactory(GameStateType.MainMenu, controllers[GameStateType.MainMenu]);
        // Initialise Scene Views & Link them to the main View
        foreach (GameStateType state in Enum.GetValues(typeof(GameStateType))) {
            if (state != GameStateType.Exit) {
                // _commandFactory.AddCommandFactory   (state, controllers[state]);
                _gameView.AddView                   (state, controllers[state]);
            }
        }

        // Clear input buffer
        _inputController.ClearBuffer();

        // Main Game loop
        GameState._state = GameStateType.MainMenu;                  // sets MainMenu as start controller
        SoundManager.Instance.PlayMusic(SoundType.MainMenuBackground, true);
        //SoundGameManager.Play(SoundType.MainMenuBackground, true);  // starts playing the first song on the loop
        while (GameState._state != GameStateType.Exit) {
            // _currentSceneController = controllers[GameState._state];
            Update();
        }
    }

    private void Update() {
        _gameView.Render();
        ActionType action = _inputController.GetUserAction();
        ICommand command = _commandFactory.CreateCommand(action);
        CommandManager.Instance.AddCommand(command);
        CommandManager.Instance.ExecuteAll();
        // _currentSceneController!.Handle(action);
    }
}
