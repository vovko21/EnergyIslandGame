using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    private Player _player;
    private Camera _camera;
    private Vector3 _moveDirection;
    private const float gravity = 9.81f;

    private void Start()
    {
        _player = GetComponent<Player>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        ApplyMovementInput();
        AppltAimInput();
        ApplyGravity();
    }

    private void ApplyMovementInput()
    {
        float horizontalMovement = UIEvents.JoystickInput.x;
        float verticalMovement = UIEvents.JoystickInput.y;

        var moveDirectionY = verticalMovement * new Vector3(_camera.transform.forward.x, 0, _camera.transform.forward.z);
        var moveDirectionX = horizontalMovement * new Vector3(_camera.transform.right.x, 0, _camera.transform.right.z);

        _moveDirection = moveDirectionX + moveDirectionY;   

        if (_moveDirection != Vector3.zero)
        {
            _player.CharacterController.Move(_moveDirection * _player.SO.Speed * Time.fixedDeltaTime);

            _player.AnimationController.SetWalk();
        }
        else
        {
            _player.AnimationController.SetIdle();
        }
    }

    private void AppltAimInput()
    {
        if (_moveDirection == Vector3.zero) return;

        _player.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_moveDirection), Time.fixedDeltaTime * _player.SO.RotationSpeed);
    }

    private void ApplyGravity()
    {
        if (!_player.CharacterController.isGrounded)
        {
            _player.CharacterController.Move(Vector3.down * gravity * Time.fixedDeltaTime);
        }
    }
}
