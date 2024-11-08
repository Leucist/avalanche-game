namespace Avalanche.Core;

public class GameController {
    IGameView _gameView;
    IInputController _inputController;
    ISceneController _currentSceneController;
    bool _isRunning;

    public GameController(IGameView gameView, IInputController inputController) {
        _gameView = gameView;
        _inputController = inputController;
        // _currentSceneController = new MainMenuController();
        _isRunning = true;
    }

    public void StartGame() {
        _gameView.Initialize();

        // while (_isRunning) {
        //     Update();
        // }
    }

    private void Update() {
        _gameView.Render();
        // ActionType action = _inputController.GetKeyboardInput();
        // _currentSceneController.Handle(action);
        Thread.Sleep(200);
    }
}
