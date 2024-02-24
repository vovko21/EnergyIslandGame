using System.Collections;
using UnityEngine;

public class LoadInArea : UnloadArea
{
    [Header("Load into settings")]
    [SerializeField] private TraditionalEnergyBuilding _traditionalEnergyBuilding;

    protected override void UnloadTick(int stackPerTick)
    {
        _traditionalEnergyBuilding.AddResource(new EnergyResource(stackPerTick, EnergyResourceType.Coal));
    }
}
