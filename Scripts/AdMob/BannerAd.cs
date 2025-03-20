using GoogleMobileAds.Api;
using UnityEngine;

namespace AdMob
{
    public class BannerAd : MonoBehaviour, IBannerAd
    {
        private BannerView _bannerAd;
        private const string BannerId = "ca-app-pub-3940256099942544/6300978111";
        
        private void OnEnable()
        {
            InitializeAd();
        }

        public void InitializeAd()
        {
            _bannerAd = new BannerView(BannerId, AdSize.Banner, AdPosition.Top);
            AdRequest adRequest = new AdRequest.Builder().Build();
            _bannerAd.LoadAd(adRequest);
        }
    }
}