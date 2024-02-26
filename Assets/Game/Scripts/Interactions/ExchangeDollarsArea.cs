using System.Collections;
using UnityEngine;

public class ExchangeDollarsArea : InteractableArea
{
    [SerializeField] private float _energyPrice = 0.1f;
    [SerializeField] private int _changePerQuadSecond = 10;

    private const float CHANGE_RATE = 0.25f;
    private IEnumerator _coroutine;

    public float ChangeRate => CHANGE_RATE;

    protected override void ContactWithPlayer(Player player)
    {
        if (_coroutine == null)
        {
            _coroutine = StartTakingPrice(player);

            StartCoroutine(_coroutine);
        }
    }

    protected override void PlayerExit(Player player)
    {
        if (_coroutine == null) return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator StartTakingPrice(Player player)
    {
        bool isFinished = false;

        var energyToExchange = player.Hands.StuckValue;

        if (energyToExchange <= 0)
        {
            isFinished = true;
        }

        while (!isFinished)
        {
            yield return new WaitForSeconds(CHANGE_RATE);

            energyToExchange -= _changePerQuadSecond;

            var result = player.Hands.UpdateStack(EnergyResourceType.Energy, energyToExchange);

            if(result == -1)
            {
                isFinished = true;
            }
            else
            {
                ProgressionManager.Instance.Wallet.AddDollars((int)(_changePerQuadSecond * _energyPrice));

                if (energyToExchange <= 0)
                {
                    isFinished = true;
                    player.Hands.ClearStack();
                }
            }
        }
    }
}
