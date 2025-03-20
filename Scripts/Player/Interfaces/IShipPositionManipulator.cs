using System;
using CoreInterfaces;
using UnityEngine;

namespace Player.Interfaces
{
    public interface IShipPositionManipulator : IPresenter
    {
        public event Action<float> MoveEvent;
        public event Action<Vector2> LookEvent;
        public event Action ShotEvent;

    }
    
}