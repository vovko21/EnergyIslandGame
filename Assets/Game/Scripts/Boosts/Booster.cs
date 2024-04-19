using System;
using UnityEngine;

public class Booster : InteractableArea
{
    [Header("Refferences")]
    [SerializeField] private Player _player;
    [SerializeField] private UserInterface _ui;
    [SerializeField] private BoostSO _boostSO;
    [Header("Settings")]
    [SerializeField] private float _time;
    [SerializeField] private float _speedBoost;

    public BoostSO BoostSO => _boostSO;
    public event Action OnUsed;

    private void OnEnable()
    {
        _ui.BoosterUI.DiamondsButton.onClick.AddListener(OnDiamondsSpend);
        _ui.BoosterUI.WatchButton.onClick.AddListener(OnWatch);
    }

    private void OnDisable()
    {
        _ui.BoosterUI.DiamondsButton.onClick.RemoveAllListeners();
        _ui.BoosterUI.WatchButton.onClick.RemoveAllListeners();
    }

    protected override void ContactWithPlayer(Player player)
    {
        _ui.ShowBooster(_boostSO);
    }

    protected override void PlayerExit(Player player)
    {
        _ui.HideBooster();
    }

    private void OnWatch()
    {
        if (!AdsManager.Instance.NoAds)
        {
            if (!AdsManager.Instance.RewardedAds.IsLoaded) return;

            AdsManager.Instance.RewardedAds.OnAdComplete += RewardedAds_OnAdComplete;

            AdsManager.Instance.RewardedAds.ShowAd();

            _ui.HideBooster();
        }
    }

    private void RewardedAds_OnAdComplete()
    {
        UseBoost();
    }

    private void OnDiamondsSpend()
    {
        var success = ProgressionManager.Instance.Wallet.TrySpendDiamands(_boostSO.Diamands);

        if(success)
        {
            UseBoost();
        }
    }

    private void UseBoost()
    {
        _player.Stats.ApplySpeedBuff(_speedBoost, _time, this);

        OnUsed?.Invoke();
    }
}