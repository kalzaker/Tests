using Gun.Interfaces;
using Gun.View;
using ObjectPool.Facade;
using UnityEngine;

namespace Gun.Cannon
{
    public class BaseCannonController : ICannonController
    {
        private readonly GunView _gunView;
        private readonly IGetBulletFacade _poolFactory;
        private readonly CannonModel _cannonModel;
        
        public BaseCannonController(GunView gunView, IGetBulletFacade poolFactory, CannonModel cannonModel)
        {
            _gunView = gunView;
            _poolFactory = poolFactory;
            _cannonModel = cannonModel;
        }
        
        public void SpawnBullet()
        {
            Transform shotPoint = _gunView.ShotPoint;
            if (shotPoint != null)
            {
                Vector2 spawnPosition = shotPoint.position;
                Vector2 direction = shotPoint.up;

                GameObject bulletObject = _poolFactory.GetNewBullet();

                Bullet bullet = new Bullet
                {
                    StartPosition = spawnPosition,
                    Direction = direction,
                    Speed = _cannonModel.BaseCannonSpeed
                };

                BulletHandler bulletHandler = new BulletHandler(bullet, bulletObject);
                bulletHandler.Initialize();

                Rigidbody2D bulletRb = bulletObject.GetComponent<Rigidbody2D>();
                bulletRb.velocity = direction * _cannonModel.BaseCannonSpeed;
            }
        }
    }
}