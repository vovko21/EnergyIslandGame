using System;

public class Wallet
{
    private int _dollars;
    private int _diamands;

    public int Dollars => _dollars;
    public int Diamands => _diamands;

    public event Action<int> OnDollarsChanged;
    public event Action<int> OnDiamandsChanged;

    public bool TrySpendDollars(int dollars)
    {
        if (_dollars - dollars < 0)
        {
            return false;
        }

        _dollars -= dollars;

        OnDollarsChanged?.Invoke(_dollars);

        return true;
    }

    public bool TrySpendDiamands(int diamands)
    {
        if (_diamands - diamands < 0)
        {
            return false;
        }

        _diamands -= diamands;

        OnDiamandsChanged?.Invoke(_diamands);

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

    public void AddDiamands(int diamands)
    {
        if (diamands <= 0)
        {
            return;
        }

        _diamands += diamands;

        OnDiamandsChanged?.Invoke(_dollars);
    }
}
