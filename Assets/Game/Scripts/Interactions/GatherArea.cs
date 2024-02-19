using UnityEngine;

public class GatherArea : InteractableArea
{
    [SerializeField] private ProductionBuilding _productionBuilding;

    protected override void ContactWithPlayer(Player player)
    {
        if (_productionBuilding.Produced < _productionBuilding.MinGatherAmount) return;

        var overflow = player.CarrySystem.AddEnergyStack(_productionBuilding.Produced);

        _productionBuilding.Gather(overflow);
    }

    protected override void ContactWithWorker(Worker worker)
    {
        if (_productionBuilding.Produced < _productionBuilding.MinGatherAmount) return;

        var overflow = worker.CarrySystem.AddEnergyStack(_productionBuilding.Produced);

        _productionBuilding.Gather(overflow);
    }
}
