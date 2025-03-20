using Enemy.Config;
using Enemy.Physics;

namespace Enemy.Abstract
{
    public abstract class AbstractUfo : Enemy
    {
        protected AbstractUfo() : base(new CustomPhysics(),new EnemySpawnConfig()) { }
        protected readonly UfoConfig UfoConfig = new();
        public abstract void MoveUfo();
        
    }
}