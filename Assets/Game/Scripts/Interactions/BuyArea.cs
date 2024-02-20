using System.Collections;
using UnityEngine;

public abstract class BuyArea : InteractableArea
{
    [Header("Price parameters")]
    [SerializeField] protected int _valueToSpend;
    [SerializeField] protected int _spendPerTick;

    protected const float SPEND_RATE = 0.1f;

    protected int _spended;
    protected bool _isSuccessed;
    private IEnumerator _coroutine;

    public int ValueToSpend => _valueToSpend;
    public int ValueLeft => _valueToSpend - _spended;
    public float SpendRate => SPEND_RATE;

    protected override void ContactWithPlayer(Player other)
    {
        if (_coroutine == null)
        {
            _coroutine = StartTakePrice();

            StartCoroutine(_coroutine);
        }
    }

    protected override void PlayerExit(Player other)
    {
        if (_coroutine == null) return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator StartTakePrice()
    {
        bool isFinished = false;
        
        while (!isFinished)
        {
            if (_spended >= _valueToSpend)
            {
                isFinished = true;
                _isSuccessed = true;
            }

            yield return new WaitForSeconds(SPEND_RATE);

            var spendPerTick = _spendPerTick;
            if (_spended + spendPerTick > _valueToSpend)
            {
                spendPerTick = _valueToSpend - _spended;
            }

            var success = ProgressionManager.Instance.Wallet.TrySpend(spendPerTick);

            if (!success)
            {
                isFinished = true;
                _isSuccessed = false;
            }
            else
            {
                _spended += spendPerTick;
            }
        }

        if (_isSuccessed)
        {
            OnBought();
            _isSuccessed = false;
            _spended = 0;
        }
    }

    protected abstract void OnBought();
}
