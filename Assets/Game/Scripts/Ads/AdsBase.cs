using UnityEngine;

public abstract class AdsBase : MonoBehaviour
{
    [SerializeField] private string _androidAdUnitId;
    [SerializeField] private string _iosGameAdUnitId;

    protected bool _isLoaded;
    protected string _adUnitId;

    public bool IsLoaded => _isLoaded;

    private void Awake()
    {
        #if UNITY_IOS
                _adUnitId = _iosGameAdUnitId;
        #elif UNITY_ANDROID
                _adUnitId = _androidAdUnitId;
        #endif
    } 

    public abstract void LoadAd();

    public abstract void ShowAd();
}
