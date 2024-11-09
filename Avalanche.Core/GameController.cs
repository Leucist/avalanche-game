namespace Avalanche.Core;

public class GameController {
    IGameView _gameView;
    IInputController _inputController;
    // ISceneController _currentSceneController;
    bool _isRunning;

    public GameController(IGameView gameView, IInputController inputController) {
        _gameView = gameView;
        _inputController = inputController;
        // _currentSceneController = new InputNameMenuController(_gameView);
        _isRunning = true;
    }

    public void StartGame() {
        _gameView.Initialize();

        // ActionType action = _inputController.GetUserAction();
        // System.Console.WriteLine(action.ToString());
        // System.Console.Write("\x1B[?25h");

        // while (_isRunning) {
        //     Update();
        // }
    }

    private void Update() {
        _gameView.Render();
        ActionType action = _inputController.GetUserAction();
        // _currentSceneController.Handle(action);
        Thread.Sleep(200);
    }
}
