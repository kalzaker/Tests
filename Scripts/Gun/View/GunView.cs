using Enemy.Adapter;
using Enemy.Enum;
using Gun.Laser;
using Score;
using UnityEngine;
using Zenject;

namespace Gun.View
{
    public class GunView : MonoBehaviour
    {
        [SerializeField] private Transform shotPoint;
        public Transform ShotPoint => shotPoint;
        
        private BaseLaserController _baseLaserController;
        private ScorePresenter _scorePresenter;

        [Inject]
        public void Construct(ScorePresenter scorePresenter, BaseLaserController baseLaserController)
        {
            _scorePresenter = scorePresenter;
            _baseLaserController = baseLaserController;
        }
        
        private void Awake()
        {
            var laserView = GetComponentInChildren<LaserView>();
            /*_baseLaserController = new BaseLaserController(laserView, this);*/
            
        }
        private void Update()
        {
            _baseLaserController.UpdateLaser();

            if (Input.GetMouseButton(1) && !_baseLaserController.IsOnCooldown && _baseLaserController.CurrentCharge > 0)
                StartCoroutine(_baseLaserController.OnConsumeCharge());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyAdapter enemyAdapter = other.GetComponent<EnemyAdapter>();
            if (enemyAdapter != null)
            {
                EnemyType enemyType = enemyAdapter.EnemyType;
                _scorePresenter.ScoreCount(enemyType);
                _scorePresenter.OnEnemyDestroyed(other.gameObject);
            }
        }
        
    }
}