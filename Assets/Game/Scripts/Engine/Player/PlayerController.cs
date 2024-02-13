using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    private Player _player;
    private Vector3 _joystickDirection;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        HandleMovementInput();

        HandleAimInput();
    }

    private void HandleMovementInput()
    {
        float horizontalMovement = UIEvents.JoystickInput.x;
        float verticalMovement = UIEvents.JoystickInput.y;

        var direction = new Vector3(horizontalMovement, 0, verticalMovement);

        if (direction != Vector3.zero)
        {
            _joystickDirection = direction;

            _player.CharacterController.Move(_joystickDirection * _player.SO.Speed * Time.fixedDeltaTime);
        }
    }

    private void HandleAimInput()
    {
        if (_joystickDirection == Vector3.zero) return;

        _player.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_joystickDirection), Time.fixedDeltaTime * _player.SO.RotationSpeed);
    }
}
