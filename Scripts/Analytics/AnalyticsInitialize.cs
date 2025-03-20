using Firebase;
using UnityEngine;

namespace Analytics
{
    namespace Analytics
    {
        public class AnalyticsInitialize : MonoBehaviour
        {
            private bool _canUseAnalytics;

            public bool CanUseAnalytics => _canUseAnalytics;

            private void Awake()
            {
                FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
                {
                    FirebaseApp app = FirebaseApp.DefaultInstance;
                    _canUseAnalytics = true;
                    Debug.Log("Firebase is initialized successfully.");
                });
            }
        }
    }
}