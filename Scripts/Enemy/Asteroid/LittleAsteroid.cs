using Enemy.Abstract;


namespace Enemy.Asteroid
{
    public class LittleAsteroid : AbstractAsteroid
    {
        public override void MoveAsteroid()
        {
            CustomPhysics.MoveForward(AsteroidConfig.LittleAsteroidSpeed);
        }
    }
}