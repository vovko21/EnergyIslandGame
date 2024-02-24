using UnityEngine;

public enum EnergyResourceType
{
    None = 0,
    Coal = 1,
    Battery = 2
}

[System.Serializable]
public struct EnergyResource
{
    public int resourcesValue;
    public EnergyResourceType type;

    public EnergyResource(int resourcesValue, EnergyResourceType type)
    {
        this.resourcesValue = resourcesValue;
        this.type = type;
    }
}

public class TraditionalEnergyBuilding : ProductionBuilding
{
    [Header("Resource price")]
    [SerializeField] private EnergyResource _energyResource;

    protected override void OnEnable()
    {
        base.OnEnable();

        if(_energyResource.resourcesValue - CurrentStats.Consumption < 0)
        {
            Status = BuildingStatus.NotProducing;
        }
    }

    protected override void Produce()
    {
        base.Produce();

        _energyResource.resourcesValue -= CurrentStats.Consumption;

        if (_energyResource.resourcesValue - CurrentStats.Consumption < 0)
        {
            Status = BuildingStatus.NotProducing;
        }
        else
        {
            _nextHourTime.AdvanceMinutes(60);
        }
    }

    public bool AddResource(EnergyResource energyResource)
    {
        if (_energyResource.type != energyResource.type)
        {
            return false;
        }

        _energyResource.resourcesValue += energyResource.resourcesValue;

        if (_energyResource.resourcesValue - CurrentStats.Consumption >= 0)
        {
            Status = BuildingStatus.Producing;
            _nextHourTime = GameTimeManager.Instance.CurrentDateTime;
            _nextHourTime.AdvanceMinutes(60);
        }

        return true;
    }
}
