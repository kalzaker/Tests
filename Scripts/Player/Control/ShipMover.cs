using System.Collections;
using Gun.Cannon;
using Gun.Interfaces;
using Gun.View;
using ObjectPool.Facade;
using Player.Balance;
using Player.Interfaces;
using Player.Physics;
using UnityEngine;
using Zenject;

namespace Player.Control
{
    public class ShipMover : MonoBehaviour
    {

        private IGetBulletFacade _poolFactory;
        private IShipPositionManipulator _shipPresenter;
        private IShipMovable _shipMovable;
        private ICannonController _baseCannonController;

        private GunView _gunView;
        private ShipModel _model;
        private CannonModel _cannonModel;

        private Camera _mainCamera;
        private Vector2 _lookDirection;

        private float _currentSpeed;

        [Inject]
        private void Construct(IGetBulletFacade pollFactory)
        {
            _poolFactory = pollFactory;
        }
        
        private void Awake()
        {
            _mainCamera = Camera.main;

            _gunView = GetComponentInChildren<GunView>();

            _shipMovable = new CustomShipPhysics();
            _model = new ShipModel();
            _shipPresenter = new ShipPresenter();

            _cannonModel = new CannonModel();
            _baseCannonController = new BaseCannonController(_gunView, _poolFactory, _cannonModel);

            _shipPresenter.Enable();

            _shipPresenter.MoveEvent += OnMove;
            _shipPresenter.LookEvent += OnLook;
            _shipPresenter.ShotEvent += OnShot;
        }

        private void FixedUpdate()
        {
            WrapAroundScreen();
        }

        private void OnDestroy()
        {
            _shipPresenter.Disable();
            _shipPresenter.MoveEvent -= OnMove;
            _shipPresenter.LookEvent -= OnLook;
            _shipPresenter.ShotEvent -= OnShot;
        }

        public float GetCurrentSpeed()
        {
            return _shipMovable.GetVelocity().magnitude;
        }

        private IEnumerator Accelerate()
        {
            float currentSpeed = 0f;
            while (currentSpeed <= _currentSpeed)
            {
                currentSpeed += _model.Thrust * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0f, _currentSpeed);

                _shipMovable.MoveForward(_model.Thrust, _model.MaxSpeed);
                transform.position = _shipMovable.Position;
                yield return null;
            }
        }

        private IEnumerator Decelerate()
        {
            while (_shipMovable.GetVelocity().magnitude > 0.1f)
            {
                _shipMovable.Decelerate(_model.Thrust);
                transform.position = _shipMovable.Position;
                yield return null;
            }
        }

        private void OnMove(float input)
        {
            _currentSpeed = Mathf.Clamp(input, 0f, 1f) * _model.MaxSpeed;

            if (_currentSpeed > 0f && gameObject != null && gameObject.activeInHierarchy)
            {
                StartCoroutine(Accelerate());
                return;
            }

            if (_currentSpeed <= 0f && gameObject != null && gameObject.activeInHierarchy)
                StartCoroutine(Decelerate());
        }

        private void OnLook(Vector2 lookDirection)
        {
            if (lookDirection.sqrMagnitude <= 0.1f)
                return;

            _lookDirection = lookDirection;
            _shipMovable.RotateTowards(_lookDirection.normalized);
            transform.rotation = Quaternion.Euler(0, 0, _shipMovable.Rotation);
        }

        private void OnShot()
        {
            _baseCannonController.SpawnBullet();
        }

        private void WrapAroundScreen()
        {
            Vector3 viewportPosition = _mainCamera.WorldToViewportPoint(transform.position);
            Vector3 newPosition = transform.position;

            if (viewportPosition.x < 0)
                newPosition.x = _mainCamera.ViewportToWorldPoint(new Vector3(1, viewportPosition.y, viewportPosition.z))
                    .x;
            else if (viewportPosition.x > 1)
                newPosition.x = _mainCamera.ViewportToWorldPoint(new Vector3(0, viewportPosition.y, viewportPosition.z))
                    .x;

            if (viewportPosition.y < 0)
                newPosition.y = _mainCamera.ViewportToWorldPoint(new Vector3(viewportPosition.x, 1, viewportPosition.z))
                    .y;
            else if (viewportPosition.y > 1)
                newPosition.y = _mainCamera.ViewportToWorldPoint(new Vector3(viewportPosition.x, 0, viewportPosition.z))
                    .y;

            _shipMovable.SetPosition(newPosition);
            transform.position = newPosition;
        }
    }
}