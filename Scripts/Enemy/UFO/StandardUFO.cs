using Enemy.Abstract;

namespace Enemy.UFO
{
    public class StandardUfo : AbstractUfo
    {
        public override void MoveUfo()
        {
            CustomPhysics.MoveForward(UfoConfig.UfoSpeed);
        }
    }
}