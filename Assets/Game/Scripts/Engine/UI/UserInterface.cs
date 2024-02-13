using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private VariableJoystick _vareiableJoystick;

    private void Update()
    {
        UIEvents.JoystickInput = _vareiableJoystick.Direction;
    }
}
