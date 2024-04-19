using System.Collections.Generic;
using UnityEngine;

public class SpawnBoosts : MonoBehaviour
{
    [SerializeField] private List<Booster> _boosters;
    [SerializeField] private int _spawnIntervalMinutes;

    private Booster _currentBooster;
    protected InGameDateTime _nextTime;

    public void StartListen()
    {
        DeactiveAll();
        _nextTime = GameTimeManager.Instance.CurrentDateTime;
        _nextTime.AdvanceMinutes(_spawnIntervalMinutes);
        GameTimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        GameTimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (_nextTime == dateTime)
        {
            if(_currentBooster == null)
            {
                _currentBooster = _boosters[Random.Range(0, _boosters.Count)];

                _currentBooster.gameObject.SetActive(true);

                _currentBooster.OnUsed += CurrentBooster_OnUsed;
            }

            _nextTime.AdvanceMinutes(_spawnIntervalMinutes);
        }
    }

    private void CurrentBooster_OnUsed()
    {
        _currentBooster.gameObject.SetActive(false);
        _currentBooster.OnUsed -= CurrentBooster_OnUsed;
        _currentBooster = null;
    }

    private void DeactiveAll()
    {
        foreach (var boster in _boosters)
        {
            boster.gameObject.SetActive(false);
        }
    }
}
