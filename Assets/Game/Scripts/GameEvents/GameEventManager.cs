using System.Collections.Generic;
using UnityEngine;

public enum GameEventType
{
    None = 0,
    BuildingBroken = 1,
    GoodDeal = 2
}

[System.Serializable]
public struct GameEvent
{
    public GameEventType type;
    [Range(0f, 1f)]
    public float chance;
}

public class GameEventManager : MonoBehaviour
{
    [SerializeField] private List<GameEvent> _events;

    protected InGameDateTime _nextTime;

    public void Initialize()
    {
        _nextTime = GameTimeManager.Instance.CurrentDateTime;
        _nextTime.AdvanceMinutes(60);
        GameTimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDestroy()
    {
        GameTimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (_nextTime == dateTime)
        {
            OnHourPassed();
            _nextTime.AdvanceMinutes(60);
        }
    }

    private void OnHourPassed()
    {
        var randomPick = Random.Range(0f, 1f);
        List<GameEvent> matches = new List<GameEvent>();   
        foreach (GameEvent e in _events)
        {
            if (e.chance >= randomPick)
            {
                matches.Add(e);
            }
        }

        GameEvent minChanceEvent = new GameEvent();
        minChanceEvent.chance = 1.1f;
        foreach (GameEvent e in matches)
        {
            if(minChanceEvent.chance > e.chance)
            {
                minChanceEvent = e;
            }
        }

        ThrowEvent(minChanceEvent.type);
    }

    private void ThrowEvent(GameEventType type)
    {
        EventManager.TriggerEvent(new GameEvent() { type = type });
    }
}
