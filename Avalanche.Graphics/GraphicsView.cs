using Avalanche.Core;

namespace Avalanche.Graphics
{
    public abstract class GraphicsView : IView
    {
        private readonly GraphicsRenderer _renderer;

        protected GraphicsRenderer Renderer => _renderer;

        public GraphicsView(GraphicsRenderer renderer) {
            _renderer = renderer;
        }

        public abstract void Render();
    }
}