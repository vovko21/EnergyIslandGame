using System.Collections;
using UnityEngine;

public abstract class BuyArea : InteractableArea
{
    [Header("Price parameters")]
    [SerializeField] private int _valueToSpend;
    [SerializeField] private int _spendPerQuadSecond;

    private const float SPEND_RATE = 0.25f;

    private int _spended;
    private bool _isSuccessed;
    private IEnumerator _coroutine;

    public int ValueToSpend => _valueToSpend;
    public int ValueLeft => _valueToSpend - _spended;
    public float SpendRate => SPEND_RATE;

    protected override void ContactWithPlayer(Collider other)
    {
        if (_isSuccessed) return;

        if (_coroutine == null)
        {
            _coroutine = StartTakePrice();

            StartCoroutine(_coroutine);
        }
    }

    protected override void PlayerExit(Collider other)
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
            yield return new WaitForSeconds(SPEND_RATE);

            var spendPerSecond = _spendPerQuadSecond;
            if (_spended + spendPerSecond > _valueToSpend)
            {
                spendPerSecond = _valueToSpend - _spended;
            }

            var success = ProgressionManager.Instance.Wallet.TrySpend(spendPerSecond);

            _spended += spendPerSecond;

            if (!success)
            {
                isFinished = true;
                _isSuccessed = false;
            }

            if (_spended >= _valueToSpend)
            {
                isFinished = true;
                _isSuccessed = true;
            }
        }

        if (_isSuccessed)
        {
            OnBuyed();
        }
    }

    protected abstract void OnBuyed();
}
