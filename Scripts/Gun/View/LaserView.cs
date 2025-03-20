using UnityEngine;

namespace Gun.View
{
    [RequireComponent(typeof(LineRenderer))]
    public class LaserView : MonoBehaviour
    {
        [SerializeField] private Material laserMaterial;
        [SerializeField] private float laserWidth = 0.5f;
        [SerializeField] private float laserLength = 7f;
        [SerializeField] private float maxCharge = 100f;
        [SerializeField] private float cooldownDuration = 3f;

        public Material LaserMaterial => laserMaterial;
        public float LaserWidth => laserWidth;
        public float LaserLength => laserLength;
        public float MaxCharge => maxCharge;
        public float CooldownDuration => cooldownDuration;

        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.material = laserMaterial;
            _lineRenderer.startWidth = laserWidth;
            _lineRenderer.endWidth = laserWidth;
        }
    }
}