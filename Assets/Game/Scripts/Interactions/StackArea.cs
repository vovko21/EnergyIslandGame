using UnityEngine;

public class StackArea : UnloadArea
{
    [Header("Stack to")]
    [SerializeField] private EnergyBank _energyBank;

    protected override void UnloadTick(int stackPerTick)
    {
        _energyBank.AddEnergy(stackPerTick);
    }
}
