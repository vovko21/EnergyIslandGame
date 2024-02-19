public class Solarpanel : ProductionBuilding
{
    protected override void OnHourPassed()
    {
        if (_produced >= _maxSupply)
        {
            _status = BuildingStatus.MaxedOut;
            return;
        }

        if (_lastHourTime.IsNight())
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
