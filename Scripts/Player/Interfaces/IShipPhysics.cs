using UnityEngine;

namespace Player.Interfaces
{
    public interface IShipPhysics
    {
        float Rotation { get; }
        Vector2 Position { get; }
        public Vector2 GetVelocity();
    }
}