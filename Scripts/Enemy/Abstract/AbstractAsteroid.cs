using Enemy.Config;
using Enemy.Physics;

namespace Enemy.Abstract
{
    public abstract class AbstractAsteroid : Enemy
    {
        protected AbstractAsteroid() : base(new CustomPhysics(),new EnemySpawnConfig()) { }
        protected readonly AsteroidConfig AsteroidConfig = new();
        public abstract void MoveAsteroid();
    }
}