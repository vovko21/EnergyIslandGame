using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterAnimationController))]
[RequireComponent(typeof(Hands))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSO _playerSO;

    private PlayerStats _currentStats;

    public PlayerStats Stats => _currentStats;
    public CharacterController CharacterController { get; private set; }
    public CharacterAnimationController AnimationController { get; private set; }
    public Hands Hands { get; private set; }

    private void Awake()
    {
        _currentStats = new PlayerStats(_playerSO);

        CharacterController = GetComponent<CharacterController>();
        AnimationController = GetComponent<CharacterAnimationController>();
        Hands = GetComponent<Hands>();
    }

    private void OnEnable()
    {
        Hands.OnStackChanged += CarrySystem_OnChange;
    }

    private void OnDisable()
    {
        Hands.OnStackChanged -= CarrySystem_OnChange;
    }

    private void CarrySystem_OnChange(CarrySystem system)
    {
        AnimationController.SetCarrying(system.IsCarrying);
    }
}
