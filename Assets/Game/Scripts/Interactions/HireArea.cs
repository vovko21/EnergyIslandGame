using UnityEngine;

public class HireArea : InteractableArea
{
    [Header("UI")]
    [SerializeField] private UserInterface _ui;

    [Header("Data")]
    [SerializeField] private WorkersStatsSO _carrierStats;
    [SerializeField] private WorkersStatsSO _serviceStats;

    [SerializeField] private GameObject _carrier;
    [SerializeField] private GameObject _service;

    private bool _carrierBuyed;
    private bool _serviceBuyed;

    private void OnEnable()
    {
        _ui.BottomBar.WorkersUI.OnUpgradeCarrierPress += OnBuyCarrierPress;
        _ui.BottomBar.WorkersUI.OnUpgradeServicePress += OnBuyServicePress;
    }

    private void OnDisable()
    {
        _ui.BottomBar.WorkersUI.OnUpgradeCarrierPress -= OnBuyCarrierPress;
        _ui.BottomBar.WorkersUI.OnUpgradeCarrierPress -= OnBuyServicePress;
    }

    protected override void ContactWithPlayer(Player player)
    {
        _ui.BottomBar.ShowWorkers(_carrierStats.Price[0], _serviceStats.Price[0]);
    }

    protected override void PlayerExit(Player player)
    {
        _ui.BottomBar.HideWorkers();
    }

    private void OnBuyCarrierPress()
    {
        bool result = ProgressionManager.Instance.Wallet.TrySpendDollars(_carrierStats.Price[0]);

        if (result) 
        {
            _carrier.SetActive(true);

            _carrierBuyed = true;
        }
    }

    private void OnBuyServicePress()
    {
        bool result = ProgressionManager.Instance.Wallet.TrySpendDollars(_serviceStats.Price[0]);

        if (result)
        {
            _service.SetActive(true);

            _serviceBuyed = true;
        }
    }
}
