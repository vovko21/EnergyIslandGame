using UnityEngine;

public class Windmill : ProductionBuilding
{
    [Header("Wind setting")]
    [SerializeField] private float _windThreashold;

    protected override void OnHourPassed()
    {
        if (_produced >= CurrentStats.MaxSupply)
        {
            _status = BuildingStatus.MaxedOut;
            return;
        }

        if (WeatherSystem.Instance.WindSpeedKmh < _windThreashold)
        {
            _status = BuildingStatus.NotProducing;
        }
        else
        {
            _status = BuildingStatus.Producing;
        }

        if (_status == BuildingStatus.Producing)
        {
            Produce();
        }
    }
}
