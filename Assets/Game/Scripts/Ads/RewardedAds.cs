using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAds : AdsBase, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public event Action OnAdComplete;

    public override void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public override void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
        LoadAd();
    }

    #region LoadCallbacks
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Rewarded Ads loaded");
        _isLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        _isLoaded = false;
    }
    #endregion

    #region ShowCallbacks
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (_adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            OnAdComplete?.Invoke();
        }
    }
    #endregion
}
