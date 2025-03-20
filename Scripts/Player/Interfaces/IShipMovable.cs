using UnityEngine;

namespace Player.Interfaces
{
    public interface IShipMovable : IShipPhysics
    {
        void MoveForward(float acceleration, float maxSpeed);
        void RotateTowards(Vector2 targetPosition);
        void SetPosition(Vector2 newPosition);
        public void Decelerate(float deceleration);
    }
}