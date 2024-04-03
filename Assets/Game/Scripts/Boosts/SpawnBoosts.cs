using System.Collections.Generic;
using UnityEngine;

public class SpawnBoosts : MonoBehaviour
{
    [SerializeField] private List<Booster> _boosters;
    [SerializeField] private int _spawnIntervalMinutes;

    protected InGameDateTime _nextTime;

    private void OnEnable()
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
            var randomBooster = _boosters[Random.Range(0, _boosters.Count)];

            randomBooster.gameObject.SetActive(true);
        }
    }

    private void DeactiveAll()
    {
        foreach (var boster in _boosters)
        {
            boster.gameObject.SetActive(false);
        }
    }
}
