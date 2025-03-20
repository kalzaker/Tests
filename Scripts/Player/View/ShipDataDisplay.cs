using Gun.Laser;
using Gun.View;
using TMPro;
using UnityEngine;
using Zenject;

namespace Player.View
{
    public class ShipDataDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text coordinatesText;
        [SerializeField] private TMP_Text rotationText;
        [SerializeField] private TMP_Text speedText;
        [SerializeField] private TMP_Text chargeText;
        [SerializeField] private TMP_Text cooldownText;

        private Control.ShipMover _shipMover;
        private BaseLaserController _baseLaserController;
        private LaserView _laserView;
        private GunView _gunView;

        [Inject]
        private void Construct(BaseLaserController baseLaserController, LaserView laserView, Control.ShipMover shipMover)
        {
            _baseLaserController = baseLaserController;
            _laserView = laserView;
            _shipMover = shipMover;
        }

        private void Start()
        {
            gameObject.SetActive(true);
        }
        
        private void LateUpdate()
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (_shipMover != null)
            {
                Vector2 position = _shipMover.transform.position;
                coordinatesText.text = $"Coordinates: ({position.x:F2}, {position.y:F2})";

                float rotation = _shipMover.transform.rotation.eulerAngles.z;
                rotationText.text = $"Rotation: {rotation:F0}°";

                float speed = _shipMover.GetCurrentSpeed();
                speedText.text = $"Speed: {speed:F1} units/s";
            }
            else
                gameObject.SetActive(false); //I could have done an OnShipMoverChanged event instead, but I didn't bother with it
            
            chargeText.text = $"Charge: {_baseLaserController.CurrentCharge:F1} / {_laserView.MaxCharge:F1}";

            cooldownText.text = _baseLaserController.IsOnCooldown
                ? $"Cooldown: {_baseLaserController.RemainCooldownTime:F1} s"
                : "Cooldown: Ready";
        }
    }
}