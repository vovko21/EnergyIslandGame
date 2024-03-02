using System;
using UnityEngine;

public class WorkersUI : MonoBehaviour
{
    [SerializeField] private WorkerItemUI _carrierWorker;
    [SerializeField] private WorkerItemUI _serviceWorker;

    public event Action OnUpgradeCarrierPress;
    public event Action OnUpgradeServicePress;

    public void Initialize(int carrierPrice, int servicePrice)
    {
        _carrierWorker.Initialize(carrierPrice);
        _serviceWorker.Initialize(servicePrice);
    }

    private void OnEnable()
    {
        _carrierWorker.BuyButton.onClick.AddListener(OnUpgrade_Carrier);
        _serviceWorker.BuyButton.onClick.AddListener(OnUpgrade_Service);
    }

    private void OnDisable()
    {
        _carrierWorker.BuyButton.onClick.RemoveAllListeners();
        _serviceWorker.BuyButton.onClick.RemoveAllListeners();
    }

    private void OnUpgrade_Carrier()
    {
        OnUpgradeCarrierPress?.Invoke();
    }

    private void OnUpgrade_Service()
    {
        OnUpgradeServicePress?.Invoke();
    }
}
