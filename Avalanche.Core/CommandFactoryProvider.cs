namespace Avalanche.Core
{
    public class CommandFactoryProvider : ICommandFactoryProvider
    {
        public ICommandFactory CreateFactory(GameStateType state, ISceneController controller)
        {
            return state switch
            {
                GameStateType.MainMenu       => new MainMenuCommandFactory((MainMenuController) controller),
                // GameStateType.OptionsMenu => new OptionsMenuCommandFactory((OptionsController) controller),
                // GameStateType.Game        => new GameCommandFactory((LevelController) controller),
                _                            => throw new NotImplementedException($"No factory implemented for {state}")
            };
        }
    }
}