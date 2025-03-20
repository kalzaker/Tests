using Enemy.Adapter;
using Enemy.Enum;
using Score;
using UnityEngine;
using Zenject;

namespace Gun.Detectors
{
    public class BulletDetector : MonoBehaviour
    { 
        private ScorePresenter _scorePresenter;
        
        [Inject]
        public void Construct(ScorePresenter scorePresenter)
        {
            _scorePresenter = scorePresenter;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyAdapter enemyAdapter = other.GetComponent<EnemyAdapter>();
            if (enemyAdapter != null)
            {
                EnemyType enemyType = enemyAdapter.EnemyType;
                _scorePresenter.ScoreCount(enemyType);
                _scorePresenter.OnEnemyDestroyed(other.gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}