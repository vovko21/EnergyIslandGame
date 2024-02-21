using System.Collections.Generic;
using UnityEngine;

public class UpgradeArea : BuyArea
{
    [Header("Upgrade settings")]
    [SerializeField] private ProductionBuilding _building;
    [SerializeField] private List<int> _prices;

    private void Awake()
    {
        if(_building.LevelsCount > _prices.Count + 1)
        {
            Debug.LogError("Building levels count more then upgrade area");
        }

        if(_building.IsMaxLevel) Destroy(this.gameObject);

        _valueToSpend = _prices[0]; 
    }

    protected override void Bought()
    {
        _building.Upgrade();

        if(!_building.IsMaxLevel)
        {
            _valueToSpend = _prices[_building.CurrentLevelIndex];
        }
        else
        {
            Destroy(this.gameObject);
        }

        EventManager.TriggerEvent(new BuildingUpdatedEvent() { productionBuilding = _building, upgraded = true });
    }
}
