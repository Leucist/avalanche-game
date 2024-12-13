using Avalanche.Core;
using SFML.Graphics;

namespace Avalanche.Graphics
{
    public class GraphicsNameInputView : GraphicsView
    {
        private readonly NameInputModel _model;

        public GraphicsNameInputView(NameInputModel model, GraphicsRenderer renderer) : base(renderer) {
            _model = model;
        }

        public override void Render() {
            
        }
    }
}