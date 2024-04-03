using UnityEngine;

public class Windmill : RenewableEnergyBuilding
{
    [Header("Wind setting")]
    [SerializeField] private float _windThreashold;

    protected override void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (WeatherSystem.Instance.WindSpeedKmh < _windThreashold)
        {
            if (Status != BuildingStatus.NotProducing)
            {
                Status = BuildingStatus.NotProducing;
            }
        }
        else if(Status == BuildingStatus.NotProducing)
        {
            Status = BuildingStatus.Producing;
        }

        base.OnDateTimeChanged(dateTime);
    }
}