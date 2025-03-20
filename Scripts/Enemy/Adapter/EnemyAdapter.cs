using Enemy.Abstract;
using Enemy.Enum;
using UnityEngine;

namespace Enemy.Adapter
{
    public class EnemyAdapter : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType;

        private global::Enemy.Abstract.Enemy _enemy;

        public EnemyType EnemyType => enemyType;

        public void Initialize(global::Enemy.Abstract.Enemy enemy, Transform enemyTransform)
        {
            _enemy = enemy;

            switch (enemy)
            {
                case AbstractAsteroid asteroid:
                    asteroid.Initialize(enemyTransform);
                    break;
                case AbstractUfo ufo:
                    ufo.Initialize(enemyTransform);
                    break;
            }
        }

        private void Update()
        {
            if (_enemy == null) return;

            EnemyGetPhysics();
            UpdateEnemyPosition();
        }
        
        private void UpdateEnemyPosition()
        {
            transform.position = _enemy.GetPosition();
        }
        
        private void EnemyGetPhysics()
        {
            switch (_enemy)
            {
                case AbstractAsteroid asteroid:
                    asteroid.MoveAsteroid();
                    break;
                case AbstractUfo ufo:
                    ufo.MoveUfo();
                    break;
            }
        }
    }
}