using Enemy.Config;
using Enemy.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Abstract
{
    public abstract class Enemy
    {
        protected readonly IEnemyPhysics CustomPhysics;
        private readonly Camera _mainCamera = Camera.main;
        private readonly EnemySpawnConfig _config;

        protected Enemy(IEnemyPhysics physics, EnemySpawnConfig config)
        {
            CustomPhysics = physics;
            _config = config;
        }
        
        public void Initialize(Transform enemyTransform)
        {
            SpawnOutsideCameraView();
            SetDirectionTowardsCamera();
            enemyTransform.position = CustomPhysics.Position;
        }

        public Vector3 GetPosition()
        {
            return CustomPhysics.Position;
        }

        private void SetDirectionTowardsCamera()
        {
            Vector2 direction = ((Vector2)_mainCamera.transform.position - CustomPhysics.Position).normalized;
            CustomPhysics.Direction = direction;
        }

        private void SpawnOutsideCameraView()
        {
            Vector3 viewportPosition;
            float randomXSpawnPosition = Random.Range(_config.MinXSpawn, _config.MaxXSpawn);
            float randomYSpawnPosition = Random.Range(_config.MinYSpawn, _config.MaxYSpawn);

            if (randomXSpawnPosition > _config.ThresholdX)
                viewportPosition = new Vector3(_config.OutsideRightX, randomYSpawnPosition, _mainCamera.nearClipPlane);
            else if (randomXSpawnPosition < _config.ThresholdY)
                viewportPosition = new Vector3(_config.OutsideLeftX, randomYSpawnPosition, _mainCamera.nearClipPlane);
            else if (randomYSpawnPosition > _config.ThresholdY)
                viewportPosition = new Vector3(randomXSpawnPosition, _config.OutsideTopY, _mainCamera.nearClipPlane);
            else
                viewportPosition = new Vector3(randomXSpawnPosition, _config.OutsideBottomY, _mainCamera.nearClipPlane);

            Vector3 worldPosition = _mainCamera.ViewportToWorldPoint(viewportPosition);
            CustomPhysics.Position = worldPosition;
        }
    }
}
