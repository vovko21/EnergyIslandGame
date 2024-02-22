public class Solarpanel : ProductionBuilding
{
    protected override void OnHourPassed()
    {
        if (_nextHourTime.IsNight())
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
