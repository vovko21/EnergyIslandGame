using UnityEngine;

public class SellArea : ProgressArea
{
    [Header("Sell settings")]
    [SerializeField] private ResourceStack _stack;

    protected override void ContactWithPlayer(Player player)
    {
        if(_stack.StuckValue > 0)
        {
            base.ContactWithPlayer(player);
        }
    }

    protected override void OnProgressed()
    {
        var value = _stack.StuckValue;

        var summary = StockMarket.Instance.EnergyPrice * value;

        _stack.ClearStack();

        Debug.Log("Added " + summary);

        ProgressionManager.Instance.Wallet.AddDollars((int)summary);
    }
}
