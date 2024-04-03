public class Solarpanel : RenewableEnergyBuilding
{
    protected override void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (dateTime.IsNight())
        {
            if (Status != BuildingStatus.NotProducing)
            {
                Status = BuildingStatus.NotProducing;
            }
        }
        else if (Status == BuildingStatus.NotProducing)
        {
            Status = BuildingStatus.Producing;
        }

        base.OnDateTimeChanged(dateTime);
    }
}