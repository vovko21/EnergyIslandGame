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

    public event Action<OrderResourceSO> OnBuyPress;
    public BuildingUpgradesUI Upgrades => _buildingUpgrades;

    private void OnEnable()
    {
        foreach (var item in _orderItems)
        {
            item.BuyButton.onClick.AddListener(() => BuyOrder(item));
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
    }

    public void ShowOrders()
    {
        _orderContainer.SetActive(true);
    }

    public void HideOrders()
    {
        _orderContainer.SetActive(false);
    }

    public void ShowUpgrades(BuildingStats buildingStats)
    {
        _buildingUpgrades.Initialize(buildingStats);
        _buildingUpgrades.gameObject.SetActive(true);
    }

    public void HideUpgrades()
    {
        _buildingUpgrades.gameObject.SetActive(false);
    }

    public void BuyOrder(OrderItemUI item)
    {
        OnBuyPress?.Invoke(item.OrderSO);
    }
}
