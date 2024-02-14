using System.Collections;
using UnityEngine;

public class ChangeDollarsArea : InteractableArea
{
    [SerializeField] private float _dollarPerEnergy = 0.1f;
    [SerializeField] private int _changePerQuadSecond = 10;

    private const float CHANGE_RATE = 0.25f;

    private bool _isSuccessed;
    private IEnumerator _coroutine;

    public float ChangeRate => CHANGE_RATE;

    protected override void ContactWithPlayer(Collider other)
    {
        if (_isSuccessed) return;

        if (_coroutine == null)
        {
            _coroutine = StartTakePrice(other.GetComponent<Player>());

            StartCoroutine(_coroutine);
        }
    }

    protected override void PlayerExit(Collider other)
    {
        if (_coroutine == null) return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator StartTakePrice(Player player)
    {
        bool isFinished = false;

        var energyToExchange = player.CarrySystem.StuckValue;

        if(energyToExchange <= 0)
        {
            isFinished = true;
        }

        while (!isFinished)
        {
            yield return new WaitForSeconds(CHANGE_RATE);

            energyToExchange -= _changePerQuadSecond;

            player.CarrySystem.UpdateEnergy(energyToExchange);

            ProgressionManager.Instance.Wallet.AddDollars((int)(_changePerQuadSecond * _dollarPerEnergy));

            if (energyToExchange <= 0)
            {
                isFinished = true;
            }
        }
    }
}
