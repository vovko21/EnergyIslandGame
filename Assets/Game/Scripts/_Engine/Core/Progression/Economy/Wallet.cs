using UnityEngine;

public class Wallet
{
    private int _coins;

    public int Coins => _coins;

    public bool TrySpend(int coins)
    {
        if(_coins - coins < 0)
        {
            return false;
        }

        _coins -= coins;

        return true;
    }

    public void AddCoins(int coins)
    {
        if(coins <= 0)
        {
            return;
        }

        _coins += coins;
    }

}
