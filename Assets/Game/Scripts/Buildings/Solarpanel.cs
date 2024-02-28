public class Solarpanel : RenewableEnergyBuilding
{
    protected override void OnHourPassed()
    {
        if (_produced >= CurrentStats.MaxSupply)
        {
            Status = BuildingStatus.MaxedOut;
            return;
        }

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
