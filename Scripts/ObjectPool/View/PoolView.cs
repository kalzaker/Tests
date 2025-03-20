using ObjectPool.Presenter;
using UnityEngine;
using Zenject;

namespace ObjectPool.View
{
    public class PoolView : MonoBehaviour
    {
        [Inject] private PoolPresenter _poolPresenter;

        [SerializeField] private float spawnInterval = 2f;
        
        public float SpawnInterval => spawnInterval;

        private void Start()
        {
            _poolPresenter.Enable();
        }

        private void OnDestroy()
        {
            _poolPresenter.Disable();
        }
    }
}