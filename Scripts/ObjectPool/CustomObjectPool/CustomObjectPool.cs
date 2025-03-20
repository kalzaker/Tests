using System.Collections.Generic;
using Enemy.Adapter;
using Enemy.Enum;
using UnityEngine;
using Zenject;

namespace ObjectPool.CustomObjectPool
{
    public class CustomObjectPool
    {
        private Dictionary<EnemyType, Queue<GameObject>> _enemies;
        private Queue<GameObject> _bullets;

        [Inject]
        private void Construct(PoolConfig poolConfig, List<GameObject> enemyPrefabs,
            [Inject(Id = "BulletPrefab")] GameObject bulletPrefab)
        {
            _enemies = new Dictionary<EnemyType, Queue<GameObject>>();
            _bullets = new Queue<GameObject>();

            InitializeEnemyQueues(poolConfig, enemyPrefabs);
            InitializeQueue(_bullets, bulletPrefab, poolConfig.maxBullets);
        }

        public GameObject GetEnemy(EnemyType enemyType)
        {
            if (!_enemies.TryGetValue(enemyType, out var enemy))
            {
                Debug.LogError($"Тип {enemyType} не указан в пулле");
                return null;
            }

            return GetObject(enemy);
        }

        public GameObject GetBullet()
        {
            return GetObject(_bullets);
        }

        private void InitializeQueue(Queue<GameObject> queue, GameObject prefab, int size)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject obj = Object.Instantiate(prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
        }

        private void InitializeEnemyQueues(PoolConfig poolConfig, List<GameObject> enemyPrefabs)
        {
            foreach (var prefab in enemyPrefabs)
            {
                EnemyAdapter adapter = prefab.GetComponent<EnemyAdapter>();
                if (adapter == null)
                {
                    Debug.LogError($"У префаба [{prefab.name}] нету компонента EnemyAdapter");
                    continue;
                }

                EnemyType enemyType = adapter.EnemyType;
                int poolSize = PoolSize(poolConfig, enemyType);

                Queue<GameObject> queue = new Queue<GameObject>();
                InitializeQueue(queue, prefab, poolSize);
                _enemies[enemyType] = queue;
            }
        }

        private GameObject GetObject(Queue<GameObject> queue)
        {
            if (queue.Count == 0) return null;

            GameObject obj = queue.Dequeue();
            obj.SetActive(true);
            ResetObjectState(obj);
            queue.Enqueue(obj);
            return obj;
        }

        private int PoolSize(PoolConfig poolConfig, EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.BigAsteroid:
                    return poolConfig.bigAsteroids;
                case EnemyType.MediumAsteroid:
                    return poolConfig.mediumAsteroids;
                case EnemyType.LittleAsteroid:
                    return poolConfig.littleAsteroids;
                case EnemyType.Ufo:
                    return poolConfig.ufos;
                default:
                    throw new System.ArgumentException($"Неизвестный енемитайп: {enemyType}");
            }
        }

        private void ResetObjectState(GameObject obj)
        {
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
        }
    }
}