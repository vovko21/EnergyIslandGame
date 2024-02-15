using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class CarrySystem : MonoBehaviour
{
    [SerializeField] private ResourceStack _energyStack;
    [SerializeField] private int _maxCarryCount;

    private Player _player;

    public bool IsCarrying { get; private set; }
    public bool IsMax => _energyStack.IsMax;
    public int StuckValue => _energyStack.StuckValue;

    public event Action<CarrySystem> OnChange;

    private void Start()
    {
        _player = GetComponent<Player>();

        _energyStack.Initialize(_maxCarryCount);
    }

    public int AddEnergyStack(int energyValue)
    {
        if (energyValue <= 0)
        {
            return -1;
        }

        var overflow = _energyStack.AddToStuck(energyValue);

        OnChange?.Invoke(this);

        if (!IsCarrying)
        {
            IsCarrying = true;

            _player.AnimationController.SetCarrying(true);
        }

        return overflow;
    }


    public int UpdateEnergyStack(int energyValue)
    {
        if (energyValue <= 0)
        {
            return -1;
        }

        _energyStack.UpdateStack(energyValue);

        OnChange?.Invoke(this);

        if (!IsCarrying)
        {
            IsCarrying = true;

            _player.AnimationController.SetCarrying(true);
        }

        return 0;
    }

    public void ClearAll()
    {
        _energyStack.ClearStack();

        OnChange?.Invoke(this);

        IsCarrying = false;

        _player.AnimationController.SetCarrying(false);
    }

    //public bool TryTakeEnergy(int energyValue)
    //{
    //    if (IsCarrying)
    //    {
    //        return false;
    //    }

    //    var success = _energyStack.TryStack(energyValue);

    //    if (!success)
    //    {
    //        return false;
    //    }

    //    _player.AnimationController.SetCarrying(true);

    //    IsCarrying = true;

    //    return true;
    //}

    //public void UpdateEnergyStack(int energyValue)
    //{
    //    if (!IsCarrying)
    //    {
    //        return;
    //    }

    //    _energyStack.TryStack(energyValue);

    //    if(StuckValue <= 0)
    //    {
    //        _player.AnimationController.SetCarrying(false);

    //        IsCarrying = false;
    //    }
    //}
}
