using Avalanche.Core;
using SFML.Graphics;

namespace Avalanche.Graphics
{
    public class GraphicsCutsceneView : GraphicsView
    {
        private readonly CutsceneModel _model;

        public GraphicsCutsceneView(CutsceneModel model, GraphicsRenderer renderer) : base(renderer) {
            _model = model;
        }

        public override void Render() {
            
        }
    }
}