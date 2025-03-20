using Enemy.Adapter;
using Enemy.Asteroid;
using Enemy.Enum;
using Enemy.UFO;
using ObjectPool.Facade;
using UnityEngine;
using Zenject;

namespace ObjectPool.Factory
{
    public class PoolFactory : IGetEnemyFacade, IGetBulletFacade
    {
        private CustomObjectPool.CustomObjectPool _objectPool;
        private DiContainer _container;
        
        [Inject]
        private void Construct(CustomObjectPool.CustomObjectPool objectPool, DiContainer container)
        {
            _objectPool = objectPool;
            _container = container;
        }

        public GameObject GetNewEnemy(EnemyType enemyType)
        {
            GameObject enemyObject = _objectPool.GetEnemy(enemyType);
            if (enemyObject is null)
            {
                Debug.LogError($"Ошибка при получении типа врага {enemyType} из пулла");
                return null;
            }

            Enemy.Abstract.Enemy enemyInstance = GetEnemyByType(enemyType);
            InitializeEnemy(enemyObject, enemyInstance);
            return enemyObject;
        }

        public GameObject GetNewBullet()
        {
            GameObject bullet = _objectPool.GetBullet();
            _container.InjectGameObject(bullet);
            return bullet;
        }

        private Enemy.Abstract.Enemy GetEnemyByType(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.BigAsteroid:
                    return new BigAsteroid();
                case EnemyType.MediumAsteroid:
                    return new MediumAsteroid();
                case EnemyType.LittleAsteroid:
                    return new LittleAsteroid();
                case EnemyType.Ufo:
                    return new StandardUfo();
                default:
                    throw new System.ArgumentException($"Неизвестный тип врага: {enemyType}");
            }
        }

        private void InitializeEnemy(GameObject enemyObject, Enemy.Abstract.Enemy enemy)
        {
            if (enemyObject is null || enemy is null) return;

            EnemyAdapter adapter = enemyObject.GetComponent<EnemyAdapter>();
            if (adapter != null)
                adapter.Initialize(enemy, enemyObject.transform);
            else
                Debug.LogError("EnemyAdapter не найден у объекта.");
        }
    }
}