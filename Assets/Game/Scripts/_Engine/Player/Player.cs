using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimationController))]
[RequireComponent(typeof(CarrySystem))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSO _playerSO;

    public CharacterController CharacterController { get; private set; }
    public PlayerAnimationController AnimationController { get; private set; }
    public CarrySystem CarrySystem { get; private set; }
    public PlayerSO SO => _playerSO;

    void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        AnimationController = GetComponent<PlayerAnimationController>();
        CarrySystem = GetComponent<CarrySystem>();
    }
}
