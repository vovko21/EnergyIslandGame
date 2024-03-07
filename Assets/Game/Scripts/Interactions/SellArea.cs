using UnityEngine;

public struct SellEvent
{
    public int energySold;
    public int dollarsGet;
}

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

        EventManager.TriggerEvent(new SellEvent() { energySold = _energyBank.Energy, dollarsGet = (int)summary });
        
        _energyBank.ClearEnergy();

        ProgressionManager.Instance.Wallet.AddDollars((int)summary);
    }
}
