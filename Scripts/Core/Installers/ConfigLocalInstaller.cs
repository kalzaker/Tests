using ObjectPool.CustomObjectPool;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class ConfigLocalInstaller : MonoInstaller
    {
        [SerializeField] private PoolConfig poolConfig;

        public override void InstallBindings()
        {
            BindConfigService();
        }

        private void BindConfigService()
        {
            Container.Bind<PoolConfig>().FromScriptableObject(poolConfig).AsSingle();
        }
    }
}