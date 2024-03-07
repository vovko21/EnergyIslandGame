using System;
using System.Collections.Generic;
using UnityEngine;

public class BottomBarUI : MonoBehaviour
{
    [Header("Order settings")]
    [SerializeField] private List<OrderItemUI> _orderItems = new List<OrderItemUI>();
    [SerializeField] private GameObject _orderContainer;

    [Header("Building Upgrade settings")]
    [SerializeField] private BuildingUpgradesUI _buildingUpgrades;

    [Header("Workers settings")]
    [SerializeField] private WorkersUI _workersUI;

    public event Action<OrderResourceSO> OnOrderBuyPress;
    public BuildingUpgradesUI BuildingUpgradesUI => _buildingUpgrades;
    public WorkersUI WorkersUI => _workersUI;

    private void OnEnable()
    {
        foreach (var item in _orderItems)
        {
            item.BuyButton.onClick.AddListener(() => OnBuyOrder(item));
        }
    }

    private void OnDisable()
    {
        foreach (var item in _orderItems)
        {
            item.BuyButton.onClick.RemoveAllListeners();
        }
    }

    private void Start()
    {
        HideOrders();
        HideUpgrades();
        HideWorkers();
    }

    public void ShowOrders()
    {
        _orderContainer.SetActive(true);
    }

    public void HideOrders()
    {
        _orderContainer.SetActive(false);
    }

    public void ShowUpgrades(string buildingId, BuildingStats buildingStats)
    {
        _buildingUpgrades.Initialize(buildingId, buildingStats);
        _buildingUpgrades.gameObject.SetActive(true);
    }

    public void HideUpgrades()
    {
        _buildingUpgrades.gameObject.SetActive(false);
    }

    public void ShowWorkers(int carrierPrice, int servicePrice)
    {
        _workersUI.Initialize(carrierPrice, servicePrice);
        _workersUI.gameObject.SetActive(true);
    }

    public void HideWorkers()
    {
        _workersUI.gameObject.SetActive(false);
    }

    //Events

    public void OnBuyOrder(OrderItemUI item)
    {
        OnOrderBuyPress?.Invoke(item.OrderSO);
    }

    public void OnButtonClose_Upgrades()
    {
        HideUpgrades();
    }

    public void OnButtonClose_Orders()
    {
        HideOrders();
    }

    public void OnButtonClose_Workers()
    {
        HideWorkers();
    }
}
