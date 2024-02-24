public class RenewableEnergyBuilding : ProductionBuilding
{
    protected override void Produce()
    {
        base.Produce();

        Status = BuildingStatus.Maintenance;
    }

    public void Maintenanced()
    {
        if (Status != BuildingStatus.Maintenance)
        {
            return;
        }

        _nextHourTime = GameTimeManager.Instance.CurrentDateTime;
        _nextHourTime.AdvanceMinutes(60);
        Status = BuildingStatus.Producing;
    }
}
