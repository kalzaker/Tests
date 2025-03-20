using Player.Interfaces;
using UnityEngine;

namespace Player.Physics
{
    public class CustomShipPhysics : IShipMovable
    {
        private Vector2 _position;
        private Vector2 _velocity;
        
        private float _rotation;
        
        public float Rotation => _rotation;
        public Vector2 Position => _position;

        public void SetPosition(Vector2 newPosition)
        {
            _position = newPosition;
        }

        public void MoveForward(float acceleration, float maxSpeed)
        {
            float radians = _rotation * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(-Mathf.Sin(radians), Mathf.Cos(radians));

            _velocity += direction * (acceleration * Time.deltaTime);
            _velocity = Vector2.ClampMagnitude(_velocity, maxSpeed);

            _position += _velocity * Time.deltaTime;
        }

        public void RotateTowards(Vector2 targetPosition)
        {
            float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
            _rotation = angle - 90f;
        }

        public void Decelerate(float deceleration)
        {
            _velocity = Vector2.MoveTowards(_velocity, Vector2.zero, deceleration * Time.deltaTime);
            _position += _velocity * Time.deltaTime;
        }

        public Vector2 GetVelocity()
        {
            return _velocity;
        }
        
    }
}