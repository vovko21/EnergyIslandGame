using System;
using UnityEngine;

public class BuildingUpgradesUI : MonoBehaviour
{
    [SerializeField] private UpgradeItemUI _prodctionItem;
    [SerializeField] private UpgradeItemUI _maxSupplyItem;

    private BuildingStats _buildingStats;

    public event Action<BuildingStat> OnUpgradProductionPress;
    public event Action<BuildingStat> OnUpgradSupplyPress;

    public void Initialize(BuildingStats buildingStats)
    {
        _buildingStats = buildingStats;

        _prodctionItem.Initialize(_buildingStats.NextProductionLevel);
        _maxSupplyItem.Initialize(_buildingStats.NextSupplyLevel);
    }

    private void OnEnable()
    {
        _prodctionItem.BuyButton.onClick.AddListener(OnUpgrade_Production);
        _maxSupplyItem.BuyButton.onClick.AddListener(OnUpgrade_Supply);
    }

    private void OnDisable()
    {
        _prodctionItem.BuyButton.onClick.RemoveAllListeners();
        _maxSupplyItem.BuyButton.onClick.RemoveAllListeners();
    }

    private void OnUpgrade_Production()
    {
        if(ProgressionManager.Instance.Wallet.Dollars >= _prodctionItem.BuildingStat.Price)
        {
            _prodctionItem.Initialize(_buildingStats.NextProductionLevel);
        }

        OnUpgradProductionPress?.Invoke(_prodctionItem.BuildingStat);
    }

    private void OnUpgrade_Supply()
    {
        if (ProgressionManager.Instance.Wallet.Dollars >= _prodctionItem.BuildingStat.Price)
        {
            _prodctionItem.Initialize(_buildingStats.NextSupplyLevel);
        }

        OnUpgradSupplyPress?.Invoke(_maxSupplyItem.BuildingStat);
    }
}
