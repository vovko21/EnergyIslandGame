using System;
using UnityEngine;

public class CarrySystem : MonoBehaviour
{
    [SerializeField] private ResourceStack _energyStack;
    [SerializeField] private int _maxCarryCount;

    public bool IsCarrying { get; private set; }
    public bool IsMax => _energyStack.IsMax;
    public int StuckValue => _energyStack.StuckValue;

    public event Action<CarrySystem> OnChange;

    private void Start()
    {
        _energyStack.Initialize(_maxCarryCount);
    }

    public int AddEnergyStack(int energyValue)
    {
        if (energyValue <= 0)
        {
            return -1;
        }

        var overflow = _energyStack.AddToStuck(energyValue); 

        if (!IsCarrying)
        {
            IsCarrying = true;
        }

        OnChange?.Invoke(this);

        return overflow;
    }

    public int UpdateEnergyStack(int energyValue)
    {
        if (energyValue <= 0)
        {
            return -1;
        }

        _energyStack.UpdateStack(energyValue);  

        if (!IsCarrying)
        {
            IsCarrying = true;
        }

        OnChange?.Invoke(this);

        return 0;
    }

    public void ClearAll()
    {
        _energyStack.ClearStack();
   
        IsCarrying = false;

        OnChange?.Invoke(this);
    }
}
