namespace Avalanche.Core
{
    public class Rock : Entity
    {
        public Rock(Entity player) : base(
            player.GetX(), 
            player.GetY(),
            player._directionAxis,
            player._direction,
            1,  // health
            10  // damage
            ) {
            
        }

        public override void Move() {
            base.MoveInstantly();
        }
    }
}