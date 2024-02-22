using UnityEngine;

public class Windmill : ProductionBuilding
{
    [Header("Wind setting")]
    [SerializeField] private float _windThreashold;

    protected override void OnHourPassed()
    {
        if (WeatherSystem.Instance.WindSpeedKmh < _windThreashold)
        {
            Status = BuildingStatus.NotProducing;
        }
        else
        {
            Status = BuildingStatus.Producing;
        }

        if (Status == BuildingStatus.Producing)
        {
            Produce();
        }
    }
}
