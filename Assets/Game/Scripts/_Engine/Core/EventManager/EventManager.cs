using System;
using System.Collections.Generic;

//public struct GameEvent
//{
//    public string EventName;
//    public GameEvent(string newName)
//    {
//        EventName = newName;
//    }
//    static GameEvent e;
//    public static void Trigger(string newName)
//    {
//        e.EventName = newName;
//        EventManager.TriggerEvent(e);
//    }
//}

public static class EventManager
{
    private static Dictionary<Type, List<IEventListenerBase>> _subscribersList;

    static EventManager()
    {
        _subscribersList = new Dictionary<Type, List<IEventListenerBase>>();
    }

    public static void AddListener<Event>(IEventListener<Event> listener) where Event : struct
    {
        Type eventType = typeof(Event);

        if (!_subscribersList.ContainsKey(eventType))
        {
            _subscribersList[eventType] = new List<IEventListenerBase>();
        }

        if (!SubscriptionExists(eventType, listener))
        {
            _subscribersList[eventType].Add(listener);
        }
    }

    public static void RemoveListener<Event>(IEventListener<Event> listener) where Event : struct
    {
        Type eventType = typeof(Event);

        if (!_subscribersList.ContainsKey(eventType))
        {
            return;
        }

        List<IEventListenerBase> subscriberList = _subscribersList[eventType];

        for (int i = subscriberList.Count - 1; i >= 0; i--)
        {
            if (subscriberList[i] == listener)
            {
                subscriberList.Remove(subscriberList[i]);

                if (subscriberList.Count == 0)
                {
                    _subscribersList.Remove(eventType);
                }

                return;
            }
        }
    }

    public static void TriggerEvent<Event>(Event newEvent) where Event : struct
    {
        List<IEventListenerBase> list;
        if (!_subscribersList.TryGetValue(typeof(Event), out list))
            return;

        for (int i = list.Count - 1; i >= 0; i--)
        {
            (list[i] as IEventListener<Event>).OnEvent(newEvent);
        }
    }

    private static bool SubscriptionExists(Type type, IEventListenerBase receiver)
    {
        List<IEventListenerBase> receivers;

        if (!_subscribersList.TryGetValue(type, out receivers)) return false;

        bool exists = false;

        for (int i = receivers.Count - 1; i >= 0; i--)
        {
            if (receivers[i] == receiver)
            {
                exists = true;
                break;
            }
        }

        return exists;
    }
}

public static class EventRegister
{
    public delegate void Delegate<T>(T eventType);

    public static void StartListeningEvent<EventType>(this IEventListener<EventType> caller) where EventType : struct
    {
        EventManager.AddListener<EventType>(caller);
    }

    public static void StopListeningEvent<EventType>(this IEventListener<EventType> caller) where EventType : struct
    {
        EventManager.RemoveListener<EventType>(caller);
    }
}

public interface IEventListenerBase { };

public interface IEventListener<T> : IEventListenerBase
{
    void OnEvent(T eventType);
}