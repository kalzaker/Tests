using UnityEngine;

namespace Player.View
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private GameObject shipPrefab;

        public GameObject ShipPrefab => shipPrefab;
    }
}