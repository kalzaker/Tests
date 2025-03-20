using UnityEngine;

namespace ObjectPool.CustomObjectPool
{
    [CreateAssetMenu(fileName = "PoolConfig", menuName = "Config/PoolConfig")]
    public class PoolConfig : ScriptableObject
    {
        public int bigAsteroids = 3;
        public int mediumAsteroids = 3;
        public int littleAsteroids = 6;
        public int shardsAmount = 2;
        public int ufos = 2;
        public int maxBullets = 20;
    }
}