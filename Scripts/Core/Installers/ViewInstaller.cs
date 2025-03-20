using Gun.View;
using Player.Control;
using Player.View;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class ViewInstaller : MonoInstaller
    {
        private CannonView _cannonView;
        private ShipView _shipView;

        public override void InstallBindings()
        {
            BindShipService();
        }

        private void BindShipService()
        {
            _shipView = GetComponent<ShipView>();

            Container.Bind<CannonView>().FromComponentInNewPrefab(_shipView.ShipPrefab).AsSingle();
            Container.Bind<GameObject>().WithId("BulletPrefab").FromMethod(context =>
                {
                    var cannonView = context.Container.Resolve<CannonView>();
                    return cannonView.BulletPrefab;
                })
                .AsSingle();

            Container.Bind<GunView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LaserView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ShipMover>().FromComponentInHierarchy().AsSingle();
        }
    }
}