using UnityEngine;

public class StackArea : UnloadArea
{
    [Header("Stack to")]
    [SerializeField] private ResourceStack _energyStack;

    protected override void UnloadTick(int stackPerTick)
    {
        _energyStack.AddToStuck(stackPerTick);
    }
}
