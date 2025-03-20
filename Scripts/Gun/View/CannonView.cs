using UnityEngine;

namespace Gun.View
{
    public class CannonView : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        public GameObject BulletPrefab => bulletPrefab;
        
    }
}