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

    private bool _carrierHired;
    private bool _serviceHired;

    public bool IsCarrierHired => _carrierHired;
    public bool IsServiceHired => _serviceHired;

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

    public void Initialilze(bool carrierHired, bool serviceHired)
    {
        _carrierHired = carrierHired;
        _serviceHired = serviceHired;

        if (_carrierHired)
        {
            _carrier.SetActive(true);

            _carrierHired = true;
        }

        if(_serviceHired)
        {
            _service.SetActive(true);

            _serviceHired = true;
        }

        AllHiredCheck();
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
        if (_carrierHired) return;
        
        bool result = ProgressionManager.Instance.Wallet.TrySpendDollars(_carrierStats.Price[0]);

        if (result) 
        {
            _carrier.SetActive(true);

            _carrierHired = true;
        }

        AllHiredCheck();
    }

    private void OnBuyServicePress()
    {
        if (_serviceHired) return;

        bool result = ProgressionManager.Instance.Wallet.TrySpendDollars(_serviceStats.Price[0]);

        if (result)
        {
            _service.SetActive(true);

            _serviceHired = true;
        }

        AllHiredCheck();
    }

    private void AllHiredCheck()
    {
        if(_carrierHired & _serviceHired)
        {
            this.gameObject.SetActive(false);

            _ui.BottomBar.HideWorkers();
        }
    }
}
