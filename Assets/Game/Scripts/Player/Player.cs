using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterAnimationController))]
[RequireComponent(typeof(CarrySystem))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSO _playerSO;

    public CharacterController CharacterController { get; private set; }
    public CharacterAnimationController AnimationController { get; private set; }
    public CarrySystem CarrySystem { get; private set; }
    public PlayerSO SO => _playerSO;

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        AnimationController = GetComponent<CharacterAnimationController>();
        CarrySystem = GetComponent<CarrySystem>();
    }

    private void OnEnable()
    {
        CarrySystem.OnStackChanged += CarrySystem_OnChange;
    }

    private void OnDisable()
    {
        CarrySystem.OnStackChanged -= CarrySystem_OnChange;
    }

    private void CarrySystem_OnChange(CarrySystem system)
    {
        AnimationController.SetCarrying(system.IsCarrying);
    }
}
