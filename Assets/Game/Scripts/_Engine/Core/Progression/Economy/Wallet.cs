using System;

public class Wallet
{
    private int _dollars;
    private int _energy;
    private int _diamands;

    public int Dollars => _dollars;

    public event Action<int> OnDollarsChanged;

    public bool TrySpend(int dollars)
    {
        if (_dollars - dollars < 0)
        {
            return false;
        }

        _dollars -= dollars;

        OnDollarsChanged?.Invoke(_dollars);

        return true;
    }

    public void AddDollars(int dollars)
    {
        if (dollars <= 0)
        {
            return;
        }

        _dollars += dollars;

        OnDollarsChanged?.Invoke(_dollars);
    }

}
