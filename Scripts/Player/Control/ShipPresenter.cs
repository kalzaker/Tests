using System;
using Player.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Control
{
    public class ShipPresenter : IShipPositionManipulator
    {
        private readonly StarshipAction _starshipAction = new();

        public event Action<float> MoveEvent;
        public event Action<Vector2> LookEvent;
        public event Action ShotEvent;

        private const float StopMovementValue = 0f;

        public void Enable()
        {
            _starshipAction.Enable();
            _starshipAction.StarInput.Move.performed += Move;
            _starshipAction.StarInput.Move.canceled += MoveCanceled;
            _starshipAction.StarInput.Look.performed += Look;
            _starshipAction.StarInput.Shot.performed += Shot;
        }

        public void Disable()
        {
            _starshipAction.Disable();
            _starshipAction.StarInput.Move.performed -= Move;
            _starshipAction.StarInput.Move.canceled -= MoveCanceled;
            _starshipAction.StarInput.Look.performed -= Look;
            _starshipAction.StarInput.Shot.performed -= Shot;
        }
        
        private void Move(InputAction.CallbackContext context)
        {
            float input = context.ReadValue<float>();
            MoveEvent?.Invoke(input);
        }

        private void MoveCanceled(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(StopMovementValue);
        }

        private void Look(InputAction.CallbackContext context)
        {
            Vector2 screenPosition = context.ReadValue<Vector2>();
            if (Camera.main != null)
            {
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
                Vector2 lookDirection = worldPosition - (Vector2)Camera.main.transform.position;
                LookEvent?.Invoke(lookDirection);
            }
        }

        private void Shot(InputAction.CallbackContext context)
        {
            ShotEvent?.Invoke();
        }
        
    }
}
