using UnityEngine;

public class GatherArea : InteractableArea
{
    [SerializeField] private ProductionBuilding _productionBuilding;

    private void Start()
    {
        _productionBuilding.OnStatusChanged += OnStatusChanged;

        this.gameObject.SetActive(false);
    }

    protected override void ContactWithPlayer(Player player)
    {
        if (_productionBuilding.Produced < _productionBuilding.MinGatherAmount) return;

        var overflow = player.Hands.AddToStack(EnergyResourceType.Battery, _productionBuilding.Produced);

        if(overflow == -1)
        {
            return;
        }

        _productionBuilding.Gather(overflow);
    }

    protected override void ContactWithWorker(Worker worker)
    {
        if (_productionBuilding.Produced < _productionBuilding.MinGatherAmount) return;

        if(worker is CarrierWorker)
        {
            var overflow = ((CarrierWorker)worker).CarrySystem.AddToStack(EnergyResourceType.Battery, _productionBuilding.Produced);

            if (overflow == -1)
            {
                return;
            }

            _productionBuilding.Gather(overflow);
        }
    }

    private void OnStatusChanged(BuildingStatus status)
    {
        if (status == BuildingStatus.Maintenance)
        {
            this.gameObject.SetActive(false);
            return;
        }

        if (_productionBuilding.Produced >= _productionBuilding.MinGatherAmount)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
