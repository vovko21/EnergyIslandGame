using System;
using UnityEngine;

public static class UIEvents
{
    public static Vector2 JoystickInput { get; set; }
    public static event Action<bool> OnShootButton;
    public static event Action OnSecondaryButton;

    public static void CallShootButtonEvent(bool down)
    {
        OnShootButton?.Invoke(down);
    }

    public static void CallSecondaryButtonEvent()
    {
        OnSecondaryButton?.Invoke();
    }

}
