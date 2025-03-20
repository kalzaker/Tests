using Analytics.Analytics;
using Firebase.Analytics;
using Zenject;

namespace Analytics
{
    public class EventAnalytics : IAnalytics
    {
        private AnalyticsInitialize _analyticsInitialize;

        [Inject]
        private void Construct(AnalyticsInitialize analyticsInitialize)
        {
            _analyticsInitialize = analyticsInitialize;
        }

        public void LogEvent(string eventName)
        {
            if (!_analyticsInitialize.CanUseAnalytics)
                return;
            FirebaseAnalytics.LogEvent(eventName);
        }
    }
}