using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private float _time;
    private float _remainingTime;

    private IEnumerator _countdown;

    private MonoBehaviour _context;

    public float RemainingTime => _remainingTime;

    public Timer(MonoBehaviour context) => _context = context;

    public event Action<float> HasBeenUpdated;
    public event Action TimeIsOver;

    public void Set(float time)
    {
        _time = time;
        _remainingTime = _time;
    }

    public void StartCountingTime()
    {
        _countdown = Countdown();
        _context.StartCoroutine(_countdown);
    }

    public void StopCountingTime()
    {
        if (_countdown != null) _context.StopCoroutine(_countdown);
    }

    private IEnumerator Countdown()
    {
        while (_countdown != null)
        {
            _remainingTime -= Time.deltaTime;

            if (_remainingTime <= 0)
            {
                _remainingTime = 0;
                TimeIsOver?.Invoke();
                StopCountingTime();
            }

            HasBeenUpdated?.Invoke(_remainingTime / _time);

            yield return null;
        }
    }
}