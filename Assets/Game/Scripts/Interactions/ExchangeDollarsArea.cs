using System.Collections;
using UnityEngine;

public class ExchangeDollarsArea : InteractableArea
{
    [SerializeField] private float _energyPrice = 0.1f;
    [SerializeField] private int _changePerQuadSecond = 10;

    private const float CHANGE_RATE = 0.25f;
    private IEnumerator _coroutine;

    public float ChangeRate => CHANGE_RATE;

    protected override void ContactWithPlayer(Collider other)
    {
        if (_coroutine == null)
        {
            _coroutine = StartTakingPrice(other.GetComponent<Player>());

            StartCoroutine(_coroutine);
        }
    }

    protected override void PlayerExit(Collider other)
    {
        if (_coroutine == null) return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator StartTakingPrice(Player player)
    {
        bool isFinished = false;

        var energyToExchange = player.CarrySystem.StuckValue;

        if (energyToExchange <= 0)
        {
            isFinished = true;
        }

        while (!isFinished)
        {
            yield return new WaitForSeconds(CHANGE_RATE);

            energyToExchange -= _changePerQuadSecond;

            player.CarrySystem.UpdateEnergyStack(energyToExchange);

            ProgressionManager.Instance.Wallet.AddDollars((int)(_changePerQuadSecond * _energyPrice));

            if (energyToExchange <= 0)
            {
                isFinished = true;
                player.CarrySystem.ClearAll();
            }
        }
    }
}
