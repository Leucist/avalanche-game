namespace Avalanche.Core
{
    public static class ViewManager
    {
        public static readonly Dictionary<GameStateType, IView> _views = new Dictionary<GameStateType, IView>();

        public static void LinkView(GameStateType state, IView view) {
            _views[state] = view;
        }
    }
}