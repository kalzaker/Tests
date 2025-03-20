using System.Collections.Generic;
using Enemy.Adapter;
using Enemy.Enum;
using ObjectPool.Presenter;
using UnityEngine;
using Zenject;

namespace Score
{
    public class ScorePresenter
    {
        private PoolPresenter _poolPresenter;
        private PlayerState _playerState;
        
        [Inject] 
        public void Construct(PoolPresenter poolPresenter,PlayerState playerState)
        {
            _poolPresenter = poolPresenter;
            _playerState = playerState;
        }
        
        private readonly Dictionary<EnemyType, int> _enemyValues = new()
        {
            { EnemyType.BigAsteroid, 50 },
            { EnemyType.MediumAsteroid, 100 },
            { EnemyType.LittleAsteroid, 150 },
            { EnemyType.Ufo, 200 }
        };

        public void ScoreCount(EnemyType enemyType)
        {
            if (_enemyValues.TryGetValue(enemyType, out int points))
                _playerState.AddScore(points);
        }

        public void OnEnemyDestroyed(GameObject enemyObject)
        {
            EnemyType enemyType = enemyObject.GetComponent<EnemyAdapter>().EnemyType;
            Vector2 enemyPosition = enemyObject.transform.position;
            _poolPresenter.SpawnChildAsteroids(enemyType,enemyPosition);
            
            enemyObject.SetActive(false);
        }
        
        
    }
}