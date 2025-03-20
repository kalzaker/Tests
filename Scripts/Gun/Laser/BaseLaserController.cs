using System.Collections;
using UnityEngine;
using Gun.View;
using Zenject;

namespace Gun.Laser
{
    public class BaseLaserController
    {
        private LaserView _laserView;
        private LaserGun _laserGun;
        private GunView _gunView;

        private float _currentCharge;
        private float _remainCooldownTime;
        private bool _isOnCooldown;

        public float CurrentCharge => _currentCharge;
        public bool IsOnCooldown => _isOnCooldown;
        public float RemainCooldownTime => _remainCooldownTime;
        
        [Inject]
        private void Construct(LaserView laserView, GunView gunView)
        {
            _laserView = laserView;
            _gunView = gunView;
  
            var laserObject = InitializeLaserObject(laserView, out var lineRenderer);
            var boxCollider = InitializeCollider(laserObject);

            _laserGun = new LaserGun(lineRenderer, boxCollider, laserView.LaserWidth);
            _currentCharge = laserView.MaxCharge;
        }
        
        public void UpdateLaser()
        {
            _laserGun.EnableLaser(false);
        }
        
        public IEnumerator OnConsumeCharge()
        {
            yield return ConsumeCharge();
        }

        private IEnumerator ConsumeCharge()
        {
            while (Input.GetMouseButton(1) && _currentCharge > 0)
            {
                Transform shotPointTransform = _gunView.ShotPoint;
                Vector3 laserEndPoint = shotPointTransform.position + shotPointTransform.up * _laserView.LaserLength;

                _laserGun.SetLaser(shotPointTransform.position, laserEndPoint);
                _laserGun.EnableLaser(true);

                _currentCharge -= Time.deltaTime;
                yield return null;
            }

            if (CurrentCharge <= 0)
            {
                _laserGun.EnableLaser(false);
                yield return Cooldown();
            }
        }
        
        private IEnumerator Cooldown()
        {
            _isOnCooldown = true;
            float cooldownTime = 0;

            while (cooldownTime < _laserView.CooldownDuration)
            {
                cooldownTime += Time.deltaTime;
                _remainCooldownTime = _laserView.CooldownDuration - cooldownTime;
                yield return null;
            }

            _currentCharge = _laserView.MaxCharge;
            _isOnCooldown = false;
            _remainCooldownTime = 0;
        }
        
        private GameObject InitializeLaserObject(LaserView laserView, out LineRenderer lineRenderer)
        {
            GameObject laserObject = new GameObject("Laser");
            laserObject.transform.SetParent(laserView.transform);

            lineRenderer = laserObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = laserView.LaserWidth;
            lineRenderer.endWidth = laserView.LaserWidth;
            lineRenderer.enabled = false;
            lineRenderer.material = laserView.LaserMaterial;
            return laserObject;
        }
        
        private BoxCollider2D InitializeCollider(GameObject laserObject)
        {
            BoxCollider2D boxCollider = laserObject.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
            boxCollider.enabled = false;
            return boxCollider;
        }
        
    }
}