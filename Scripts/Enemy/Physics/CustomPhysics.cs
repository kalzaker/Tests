using Enemy.Interfaces;
using UnityEngine;

namespace Enemy.Physics
{
    public class CustomPhysics : IEnemyPhysics
    {
        private Vector2 _position;
        private Vector2 _direction;
        
        private float _currentSpeed;
        
        public Vector2 Position { get => _position; set => _position = value; }

        public Vector2 Direction { get => _direction; set => _direction = value; }

        public void MoveForward(float maxSpeed)
        {
            _currentSpeed += Time.fixedDeltaTime;

            if (_currentSpeed > maxSpeed)
                _currentSpeed = maxSpeed;

            _position += _direction * (_currentSpeed * Time.fixedDeltaTime);
        }
    }
}