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
    private bool _noAds;

    public bool IsTesting => _isTesting;
    public bool NoAds => _noAds;
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

    public void SetNoAds(bool noAds)
    {
        _noAds = noAds;
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Ads initialized...");

        _interstitialAds.LoadAd();
        _rewardedAds.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
    }
}