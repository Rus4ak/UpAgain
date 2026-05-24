using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class InterstitialAdShow : MonoBehaviour
{
    private InterstitialAd _interstitial;
    private const string adUnitId = "ca-app-pub-1047420423867770/7966008160";
    //private const string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //testID

    public event Action ProcessAd;

    public static InterstitialAdShow Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
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
            _interstitial.OnAdFullScreenContentFailed += HandleAdFailed;
        });
    }

    public void Show()
    {
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

        if (_interstitial != null)
        {
            _interstitial.OnAdFullScreenContentClosed -= HandleAdClosed;
            _interstitial.Destroy();
            _interstitial = null;
        }

        ProcessAd?.Invoke();

        if (AdsData.isShow)
            LoadInterstitial();
    }

    private void HandleAdFailed(AdError error)
    {
        HandleAdClosed();
    }
}
