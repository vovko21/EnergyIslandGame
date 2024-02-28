using System;
using UnityEngine;

public class BuildingUpgradesUI : MonoBehaviour, IEventListener<BuildingUpdatedEvent>
{
    [SerializeField] private UpgradeItemUI _prodctionItem;
    [SerializeField] private UpgradeItemUI _maxSupplyItem;

    private BuildingStats _buildingStats;

    public event Action<BuildingStat> OnUpgradeProductionPress;
    public event Action<BuildingStat> OnUpgradeSupplyPress;

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

        this.StartListeningEvent();
    }

    private void OnDisable()
    {
        _prodctionItem.BuyButton.onClick.RemoveAllListeners();
        _maxSupplyItem.BuyButton.onClick.RemoveAllListeners();

        this.StopListeningEvent();
    }

    private void OnUpgrade_Production()
    {
        OnUpgradeProductionPress?.Invoke(_prodctionItem.BuildingStat);
    }

    private void OnUpgrade_Supply()
    {
        OnUpgradeSupplyPress?.Invoke(_maxSupplyItem.BuildingStat);
    }

    public void OnEvent(BuildingUpdatedEvent eventType)
    {
        if (!eventType.upgraded) return;

        _prodctionItem.Initialize(_buildingStats.NextProductionLevel);
        _maxSupplyItem.Initialize(_buildingStats.NextSupplyLevel);
    }
}
