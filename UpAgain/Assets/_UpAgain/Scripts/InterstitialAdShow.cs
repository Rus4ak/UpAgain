using GoogleMobileAds.Api;
using UnityEngine;

public class InterstitialAdShow : MonoBehaviour
{
    private InterstitialAd _interstitial;
    //private const string adUnitId = "ca-app-pub-1047420423867770/7966008160";
    private const string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //testID

    private Finish _finish;
    private string _loadSceneName;

    private void Start()
    {
        _finish = GetComponent<Finish>();

        if (AdsData.isShow)
            MobileAds.Initialize(_ => { LoadInterstitial(); });
    }

    private void LoadInterstitial()
    {
        AdRequest request = new AdRequest();

        InterstitialAd.Load(adUnitId, request, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                return;
            }

            _interstitial = ad;

            _interstitial.OnAdFullScreenContentClosed += HandleAdClosed;
        });
    }

    public void Show(string loadScene)
    {
        _loadSceneName = loadScene;

        if (_interstitial != null && AdsData.isShow)
        {
            AudioListener.pause = true;
            _interstitial.Show();
        }
        else
        {
            HandleAdClosed();
        }
    }

    private void HandleAdClosed()
    {
        AudioListener.pause = false;

        _interstitial?.Destroy();
        _interstitial = null;

        _finish.LoadScene(_loadSceneName);

        LoadInterstitial();
    }
}
