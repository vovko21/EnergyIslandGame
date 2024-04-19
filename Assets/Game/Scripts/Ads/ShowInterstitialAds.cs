using System.Collections;
using UnityEngine;

public class ShowInterstitialAds : MonoBehaviour
{
    [SerializeField] private UserInterface _ui;
    [SerializeField] private int _cycleTimeMinutes;
    #region ReadOnly
    #if UNITY_EDITOR
        [ReadOnly]
        [SerializeField]
    #endif
    #endregion
    private float _realSecondsTime;

    private InGameDateTime _nextHourTime;

    public void StartListen()
    {
        if (AdsManager.Instance.NoAds) return;

        _nextHourTime = GameTimeManager.Instance.CurrentDateTime;
        _nextHourTime.AdvanceMinutes(_cycleTimeMinutes);
        GameTimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;

        _realSecondsTime = GameTimeManager.Instance.InGameMinutesToRealSeconds(_cycleTimeMinutes);
    }

    private void OnDisable()
    {
        GameTimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (_nextHourTime == dateTime)
        {
            if (AdsManager.Instance.NoAds) return;
            StartCoroutine(ShowAd());
            _nextHourTime.AdvanceMinutes(_cycleTimeMinutes);
        }
    }

    private IEnumerator ShowAd()
    {
        if(!AdsManager.Instance.InterstitialAds.IsLoaded)
        {
            AdsManager.Instance.InterstitialAds.LoadAd();
        }

        _ui.ShowAdTimer();
        yield return new WaitForSeconds(_ui.AdTimerUI.ShowInTime);
        AdsManager.Instance.InterstitialAds.ShowAd();
        _ui.HideAdTimer();
    }
}