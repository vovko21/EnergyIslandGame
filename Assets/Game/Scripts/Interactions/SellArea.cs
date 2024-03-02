using UnityEngine;

public class SellArea : ProgressArea
{
    [Header("Sell settings")]
    [SerializeField] private EnergyBank _energyBank;

    protected override void ContactWithPlayer(Player player)
    {
        if(_energyBank.Energy > 0)
        {
            base.ContactWithPlayer(player);
        }
    }

    protected override void OnProgressed()
    {
        var summary = StockMarket.Instance.EnergyPrice * _energyBank.Energy;

        _energyBank.ClearEnergy();

        ProgressionManager.Instance.Wallet.AddDollars((int)summary);
    }
}
