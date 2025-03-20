using System.Collections.Generic;
using Analytics;
using Analytics.Analytics;
using Gun.Laser;
using ObjectPool.Factory;
using ObjectPool.CustomObjectPool;
using ObjectPool.Facade;
using ObjectPool.Presenter;
using ObjectPool.View;
using Player.View;
using Score;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class LocalInstaller : MonoInstaller
    {
        [SerializeField] private List<GameObject> enemyPrefabs;
        
        public override void InstallBindings()
        {
            BindPoolService();
            BindPoolViewService();
            BindFactoryService();
            BindScoreService();
            BindAnalyticsService();
            BindGunService();
        }

        private void BindPoolService()
        {
            Container.Bind<CustomObjectPool>().AsSingle();
            Container.Bind<PoolPresenter>().AsTransient();
        }

        private void BindGunService()
        {
            Container.Bind<BaseLaserController>().AsSingle();
            Container.Bind<ShipDataDisplay>().FromComponentInHierarchy().AsSingle();
        }

        private void BindFactoryService()
        {
            Container.Bind<PoolFactory>().AsTransient();
            Container.Bind<IGetEnemyFacade>().To<PoolFactory>().AsTransient();
            Container.Bind<IGetBulletFacade>().To<PoolFactory>().AsTransient();
        }

        private void BindPoolViewService()
        {
            Container.Bind<PoolView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<List<GameObject>>().FromInstance(enemyPrefabs).AsSingle();
        }

        private void BindScoreService()
        {
            Container.Bind<PlayerState>().AsSingle();
            Container.Bind<ScorePresenter>().AsSingle().NonLazy();
        }

        private void BindAnalyticsService()
        {
            Container.Bind<AnalyticsInitialize>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IAnalytics>().To<EventAnalytics>().AsSingle();
        }
    }
}