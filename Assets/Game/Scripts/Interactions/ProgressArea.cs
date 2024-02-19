using System.Collections;
using UnityEngine;

public class ProgressArea : InteractableArea
{
    [Header("Progress settings")]
    [SerializeField] private float _timeToProgress;

    private float _timeLeft;
    private IEnumerator _coroutine;

    public float TimeLeft => _timeLeft;
    public float TimeToProgress => _timeToProgress;

    protected override void ContactWithPlayer(Player player)
    {
        if (_coroutine == null)
        {
            _coroutine = StartProgress();

            StartCoroutine(_coroutine);
        }
    }

    protected override void PlayerExit(Player player)
    {
        if (_coroutine == null) return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator StartProgress()
    {
        bool isFinished = false;

        _timeLeft = _timeToProgress;

        while (!isFinished)
        {
            _timeLeft -= Time.deltaTime;

            if (_timeLeft <= 0)
            {
                isFinished = true;
            }
            yield return null;
        }

        OnProgressed();

        _timeLeft = _timeToProgress;
    }

    protected virtual void OnProgressed() { }
}
