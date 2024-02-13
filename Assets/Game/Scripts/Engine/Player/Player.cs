using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSO _playerSO;

    public CharacterController CharacterController { get; private set; }
    public PlayerSO SO => _playerSO;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    
    void Update()
    {

    }
}
