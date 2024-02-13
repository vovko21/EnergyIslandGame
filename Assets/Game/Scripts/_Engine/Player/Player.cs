using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimationController))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSO _playerSO;

    private Wallet _wallet;

    public CharacterController CharacterController { get; private set; }
    public PlayerAnimationController AnimationController { get; private set; }
    public PlayerSO SO => _playerSO;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        AnimationController = GetComponent<PlayerAnimationController>();

        _wallet = new Wallet();
    }
}
