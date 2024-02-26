using UnityEngine;

public class EnergyResourcesArea : InteractableArea
{
    [SerializeField] private EnergyResource _energyResource;

    protected override void ContactWithPlayer(Player player)
    {
        if (_energyResource.resourcesValue == 0) return;

        int overflow = player.Hands.AddToStack(_energyResource.type, _energyResource.resourcesValue);

        if(overflow == -1)
        {
            return;
        }

        _energyResource.resourcesValue = overflow;
    }

    public void AddValue(int value)
    {
        if(value <= 0) return;

        _energyResource.resourcesValue += value;
    }
}
