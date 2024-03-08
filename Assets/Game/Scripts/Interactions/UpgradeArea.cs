using UnityEngine;

public class UpgradeArea : InteractableArea
{
    [SerializeField] private ProductionBuilding _productionBuilding;
    [SerializeField] private UserInterface _ui;

    private void OnEnable()
    {
        _ui.BottomBar.BuildingUpgradesUI.OnUpgradeProductionPress += OnUpgradProduction;
        _ui.BottomBar.BuildingUpgradesUI.OnUpgradeSupplyPress += OnUpgradSupply;
    }

    private void OnDisable()
    {
        _ui.BottomBar.BuildingUpgradesUI.OnUpgradeProductionPress -= OnUpgradProduction;
        _ui.BottomBar.BuildingUpgradesUI.OnUpgradeSupplyPress -= OnUpgradSupply;
    }

    protected override void ContactWithPlayer(Player player)
    {
        _ui.BottomBar.ShowUpgrades(_productionBuilding.Id, _productionBuilding.CurrentStats);
    }

    protected override void PlayerExit(Player player)
    {
        _ui.BottomBar.HideUpgrades();
    }

    private void OnUpgradProduction(string buildingId, BuildingStat stat)
    {
        if (stat == null) return;
        if(_productionBuilding.Id != buildingId)
        {
            return;
        }

        var result = ProgressionManager.Instance.Wallet.TrySpendDollars(stat.Price);

        if (result)
        {
            _productionBuilding.CurrentStats.UpgradeProduction();

            EventManager.TriggerEvent(new BuildingUpdatedEvent() { productionBuilding = _productionBuilding, upgraded = true });
        }
    }

    private void OnUpgradSupply(string buildingId, BuildingStat stat)
    {
        if (stat == null) return;
        if (_productionBuilding.Id != buildingId)
        {
            return;
        }

        var result = ProgressionManager.Instance.Wallet.TrySpendDollars(stat.Price);

        if (result)
        {
            _productionBuilding.CurrentStats.UpgradeSupply();

            EventManager.TriggerEvent(new BuildingUpdatedEvent() { productionBuilding = _productionBuilding, upgraded = true });
        }
    } 
}
