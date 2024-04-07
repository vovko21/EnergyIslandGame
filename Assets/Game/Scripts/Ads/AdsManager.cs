using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : SingletonMonobehaviour<AdsManager>, IUnityAdsInitializationListener
{
    [Header("API settings")]
    [SerializeField] private string _androidGameId;
    [SerializeField] private string _iosGameId;
    [SerializeField] private bool _isTesting;

    [Header("ADS settings")]
    [SerializeField] private InterstitialAds _interstitialAds;
    [SerializeField] private RewardedAds _rewardedAds;

    private string _gameId;

    public InterstitialAds InterstitialAds  => _interstitialAds;
    public RewardedAds RewardedAds => _rewardedAds;

    protected override void Awake()
    {
        base.Awake();

        #if UNITY_IOS
            _gameId = _iosGameId;
        #elif UNITY_ANDROID
            _gameId = _androidGameId;
        #elif UNITY_EDITOR
            _gameId = _androidGameId;
        #endif

        if(!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _isTesting, this);
        }
    }


    public void OnInitializationComplete()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Ads initialized...");

        _interstitialAds.LoadAd();
        _rewardedAds.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //throw new System.NotImplementedException();
    }
}