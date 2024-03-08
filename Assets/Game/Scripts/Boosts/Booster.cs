using UnityEngine;

public class Booster : InteractableArea
{
    [SerializeField] private Player _player;
    [SerializeField] private UserInterface _ui;
    [SerializeField] private BoostSO _boostSO;

    public BoostSO BoostSO => _boostSO;

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
        //WATCH ADS
        Debug.Log("DKKJHDKJHSKhdkj");
        UseBoost();
    }

    private void OnDiamondsSpend()
    {
        //bost
        //ProgressionManager.Instance.Wallet.TrySpendDiamands();
        UseBoost();
    }

    private void UseBoost()
    {
        _player.Stats.AddSpeed(_boostSO.Speed);
    }
}
