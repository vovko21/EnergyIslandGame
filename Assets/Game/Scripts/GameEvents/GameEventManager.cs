using System.Collections.Generic;
using UnityEngine;

public enum GameEventType
{
    None = 0,
    BuildingBroken = 1
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

    private void Start()
    {
        _nextTime = GameTimeManager.Instance.CurrentDateTime;
        _nextTime.AdvanceMinutes(60);
        GameTimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
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
        foreach (GameEvent e in _events)
        {
            var random = Random.Range(0f, 1f);
            if(e.chance >= random)
            {
                ThrowEvent(e.type);
            }
        }
    }

    private void ThrowEvent(GameEventType type)
    {
        EventManager.TriggerEvent(new GameEvent() { type = type });
    }
}
