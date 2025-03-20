using System.Collections;
using CoreInterfaces;
using Enemy.Enum;
using ObjectPool.CustomObjectPool;
using ObjectPool.Facade;
using ObjectPool.View;
using UnityEngine;
using Zenject;

namespace ObjectPool.Presenter
{
    public class PoolPresenter : IPresenter
    {
        private IGetEnemyFacade _poolFactory;
        private PoolView _poolView;
        private PoolConfig _poolConfig;
        
        [Inject]
        public void Construct(IGetEnemyFacade poolFactory,PoolView poolView, PoolConfig poolConfig)
        {
            _poolFactory = poolFactory;
            _poolView = poolView;
            _poolConfig = poolConfig;
        }
        
        public void Enable()
        {
            _poolView.StartCoroutine(SpawnRoutine());
        }

        public void Disable()
        {
            _poolView.StopAllCoroutines();
        }
        
        public void SpawnChildAsteroids(EnemyType destroyedEnemyType, Vector2 spawnPosition)
        {
            EnemyType childType = GetChildByType(destroyedEnemyType);
            if (childType is EnemyType.None) return;

            SpawnShards(childType, spawnPosition);
        }

        private EnemyType GetChildByType(EnemyType destroyedEnemyType)
        {
            switch (destroyedEnemyType)
            {
                case EnemyType.BigAsteroid:
                    return EnemyType.MediumAsteroid;
                case EnemyType.MediumAsteroid:
                    return EnemyType.LittleAsteroid;
                default:
                    return EnemyType.None;
            }
        }

        private void SpawnShards(EnemyType childType, Vector2 spawnPosition)
        {
            for (int i = 0; i < _poolConfig.shardsAmount; i++)
            {
                GameObject shard = _poolFactory.GetNewEnemy(childType);
                if (shard is null) continue;

                ShardState(shard, spawnPosition);
            }
        }

        private void ShardState(GameObject shard, Vector2 spawnPosition)
        {
            shard.transform.position = spawnPosition;
            shard.SetActive(true);
        }
        
        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_poolView.SpawnInterval);
                SpawnRandomEnemy();
            }
        }

        private void SpawnRandomEnemy()
        {
            int firstType = (int)EnemyType.BigAsteroid;
            int lastType = (int)EnemyType.None;
            
            EnemyType enemyType = (EnemyType)Random.Range(firstType, lastType); 
            GameObject enemyObject = _poolFactory.GetNewEnemy(enemyType);

            if (enemyObject != null)
                enemyObject.SetActive(true);
        }
        
    }
}