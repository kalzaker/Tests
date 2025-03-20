using Enemy.Physics;
using UnityEngine;

namespace Gun.Cannon
{
    public class BulletHandler
    {
        private readonly Bullet _bullet;
        private readonly CustomPhysics _physics;
        private readonly GameObject _bulletObject;

        public BulletHandler(Bullet bullet, GameObject bulletObject)
        {
            _bullet = bullet;
            _bulletObject = bulletObject;
            _physics = new CustomPhysics
            {
                Position = _bullet.StartPosition,
                Direction = _bullet.Direction
            };
        }

        public void Initialize()
        {
            MoveForward();
        }

        private void MoveForward()
        {
            _physics.MoveForward(_bullet.Speed);
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            _bulletObject.transform.position = _physics.Position;
        }
    }
}

