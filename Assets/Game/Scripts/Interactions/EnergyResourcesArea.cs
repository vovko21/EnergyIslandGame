using UnityEngine;

public class EnergyResourcesArea : InteractableArea
{
    [SerializeField] private EnergyResource _energyResource;

    protected override void ContactWithPlayer(Player player)
    {
        if (_energyResource.resourcesValue == 0) return;

        int overflow = 0;
        switch (_energyResource.type)
        {
            case EnergyResourceType.None:
                break;
            case EnergyResourceType.Coal:
                overflow = player.CarrySystem.AddToStack(EnergyResourceType.Coal, _energyResource.resourcesValue);
                break;
            default:
                break;
        }

        _energyResource.resourcesValue = overflow;
    }
}
