using UnityEngine;

namespace Gun.Laser
{
    public class LaserGun
    {
        private readonly LineRenderer _lineRenderer;
        private readonly BoxCollider2D _boxCollider;
        private readonly float _laserWidth;
        
        public LaserGun(LineRenderer lineRenderer, BoxCollider2D boxCollider, float laserWidth)
        {
            _lineRenderer = lineRenderer;
            _boxCollider = boxCollider;
            _laserWidth = laserWidth;
        }

        public void SetLaser(Vector3 startPoint, Vector3 endPoint)
        {
            _lineRenderer.SetPosition(0, startPoint);
            _lineRenderer.SetPosition(1, endPoint);

            UpdateCollider(startPoint, endPoint);
        }

        public void EnableLaser(bool isEnabled)
        {
            _lineRenderer.enabled = isEnabled;
            _boxCollider.enabled = isEnabled;
        }

        private void UpdateCollider(Vector3 startPoint, Vector3 endPoint)
        {
            float distance = Vector3.Distance(startPoint, endPoint);
            Vector2 direction = (endPoint - startPoint).normalized;

            _boxCollider.size = new Vector2(distance, _laserWidth);
            _boxCollider.transform.position = startPoint + (endPoint - startPoint) / 2;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _boxCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}