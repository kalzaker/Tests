using Enemy.Abstract;

namespace Enemy.Asteroid
{
    public class BigAsteroid : AbstractAsteroid
    {
        public override void MoveAsteroid()
        {
            CustomPhysics.MoveForward(AsteroidConfig.BigAsteroidSpeed);
        }
    }
}