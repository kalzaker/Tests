using UnityEngine;

namespace Enemy.Interfaces
{
    public interface IEnemyPhysics
    {
        Vector2 Position { get; set; }
        Vector2 Direction { set; }
        void MoveForward(float speed);
    }
}