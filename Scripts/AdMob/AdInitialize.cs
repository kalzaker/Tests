using GoogleMobileAds.Api;
using UnityEngine;

namespace AdMob
{
    public class AdInitialize : MonoBehaviour
    {
        private void Awake()
        {
            MobileAds.Initialize(initStatus => { });
        }
    }
}