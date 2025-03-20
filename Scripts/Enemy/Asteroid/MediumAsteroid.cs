using Enemy.Abstract;

namespace Enemy.Asteroid
{
    public class MediumAsteroid : AbstractAsteroid
    {
        public override void MoveAsteroid()
        {
            CustomPhysics.MoveForward(AsteroidConfig.MediumAsteroidSpeed);
        }
    }
}