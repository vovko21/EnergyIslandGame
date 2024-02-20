using System.Collections.Generic;
using UnityEngine;

public class UpgradeArea : BuyArea
{
    [Header("Upgrade settings")]
    [SerializeField] private ProductionBuilding _building;
    [SerializeField] private List<int> _prices;

    private void Awake()
    {
        if(_building.LevelsCount > _prices.Count)
        {
            Debug.LogError("Building levels count more then upgrade area");
        }

        MaxLevelCheck();

        _valueToSpend = _prices[0]; 
    }

    protected override void OnBought()
    {
        MaxLevelCheck();

        _building.Upgrade();

        _valueToSpend = _prices[_building.CurrentLevelIndex];
    }

    private void MaxLevelCheck()
    {
        if(_building.IsMaxLevel == true)
        {
            Destroy(gameObject);
        }
    }
}
