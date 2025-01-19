using Avalanche.Core;

namespace Avalanche.Graphics
{
    public class GraphicsOptionsView : GraphicsView
    {
        private OptionsModel _model;
        
        public GraphicsOptionsView(OptionsModel model, GraphicsRenderer renderer) : base(renderer) {
            _model = model;
        }

        public override void Reset() {
            Renderer.ResetEventHandlers();
        }

        public override void Render() {
            // TODO / Draw
            //* Similar as in the console
            //* ..but now in graphics :D
            //* Well, okay, here's gonna be a reminder:

            // TODO / Draw:
                //
                //                                  _model._title
                //
                //  Press Escape to get back to the Main Menu
                //  
                //
                //
                //              Difficulty:      < (DifficultyLevelType) _model.Difficulty >
                //
                //  
                //  Use arrows (Left, Right) to change difficulty
                //  Use Enter to save your choise
                //
        }
    }
}
