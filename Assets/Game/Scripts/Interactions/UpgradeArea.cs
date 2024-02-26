using UnityEngine;

public class UpgradeArea : InteractableArea
{
    [SerializeField] private ProductionBuilding _productionBuilding;
    [SerializeField] private UserInterface _ui;

    private void OnEnable()
    {
        _ui.BottomBar.Upgrades.OnUpgradProductionPress += OnUpgradProduction;
        _ui.BottomBar.Upgrades.OnUpgradSupplyPress += OnUpgradSupply;
    }

    private void OnDisable()
    {
        _ui.BottomBar.Upgrades.OnUpgradProductionPress -= OnUpgradProduction;
        _ui.BottomBar.Upgrades.OnUpgradSupplyPress -= OnUpgradSupply;
    }

    protected override void ContactWithPlayer(Player player)
    {
        _ui.BottomBar.ShowUpgrades(_productionBuilding.CurrentStats);
    }

    protected override void PlayerExit(Player player)
    {
        _ui.BottomBar.HideUpgrades();
    }

    private void OnUpgradProduction(BuildingStat stat)
    {
        if (stat == null) return;

        var result = ProgressionManager.Instance.Wallet.TrySpend(stat.Price);

        if (result)
        {
            _productionBuilding.CurrentStats.UpgradeProduction();
        }
    }

    private void OnUpgradSupply(BuildingStat stat)
    {
        if (stat == null) return;

        var result = ProgressionManager.Instance.Wallet.TrySpend(stat.Price);

        if (result)
        {
            _productionBuilding.CurrentStats.UpgradeSupply();
        }
    } 
}
