using System;
using UnityEngine;

public class BuildingUpgradesUI : MonoBehaviour, IEventListener<BuildingUpdatedEvent>
{
    [SerializeField] private UpgradeItemUI _prodctionItem;
    [SerializeField] private UpgradeItemUI _maxSupplyItem;

    private BuildingStats _buildingStats;
    private string _buildingId;

    public event Action<string, BuildingStat> OnUpgradeProductionPress;
    public event Action<string, BuildingStat> OnUpgradeSupplyPress;

    public void Initialize(string buildingId, BuildingStats buildingStats)
    {
        _buildingStats = buildingStats;
        _buildingId = buildingId;

        _prodctionItem.Initialize(_buildingId, _buildingStats.NextProductionLevel);
        _maxSupplyItem.Initialize(_buildingId, _buildingStats.NextSupplyLevel);
    }

    private void OnEnable()
    {
        _prodctionItem.BuyButton.onClick.AddListener(() => OnUpgrade_Production());
        _maxSupplyItem.BuyButton.onClick.AddListener(() => OnUpgrade_Supply());

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
        OnUpgradeProductionPress?.Invoke(_buildingId, _prodctionItem.BuildingStat);
    }

    private void OnUpgrade_Supply()
    {
        OnUpgradeSupplyPress?.Invoke(_buildingId, _maxSupplyItem.BuildingStat);
    }

    public void OnEvent(BuildingUpdatedEvent eventType)
    {
        if (!eventType.upgraded) return;

        _prodctionItem.Initialize(_buildingId, _buildingStats.NextProductionLevel);
        _maxSupplyItem.Initialize(_buildingId, _buildingStats.NextSupplyLevel);
    }
}
