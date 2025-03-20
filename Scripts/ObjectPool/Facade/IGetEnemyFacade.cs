using Enemy.Enum;
using UnityEngine;

namespace ObjectPool.Facade
{
    public interface IGetEnemyFacade
    {
        GameObject GetNewEnemy(EnemyType enemyType);
    }
}