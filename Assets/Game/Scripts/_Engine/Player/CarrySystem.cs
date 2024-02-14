using UnityEngine;

[RequireComponent(typeof(Player))]
public class CarrySystem : MonoBehaviour
{
    [SerializeField] private ResourceStack _energyStack;
    private Player _player;

    public bool IsCarrying { get; private set; }
    public int StuckValue => _energyStack.StuckValue;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    public bool TryTakeEnergy(int energyProduced)
    {
        if (IsCarrying)
        {
            return false;
        }

        _energyStack.Stack(energyProduced);

        _player.AnimationController.SetCarrying(true);

        IsCarrying = true;

        return true;
    }

    public void UpdateEnergy(int energyProduced)
    {
        if (!IsCarrying)
        {
            return;
        }

        _energyStack.Stack(energyProduced);

        if(StuckValue == 0)
        {
            _player.AnimationController.SetCarrying(false);

            IsCarrying = false;
        }
    }
}
