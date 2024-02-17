using UnityEngine;

public class Windmill : ProductionBuilding, IInteractable
{
    [SerializeField] private int _productionPerGameHour;
    [SerializeField] private int _maxSupply;
    [SerializeField] private int _minGatherAmount;

    private InGameDateTime _lastProducedTime;

    public int MaxSupply => _maxSupply;

    private void OnEnable()
    {
        _lastProducedTime = TimeManager.Instance.CurrentDateTime;
        TimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        TimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if(_lastProducedTime.Hour != dateTime.Hour)
        {
            OnHourPassed();
            _lastProducedTime = dateTime;
        }
    }

    private void OnHourPassed()
    {
        if (_produced >= _maxSupply) return;

        _produced += _productionPerGameHour;
    }

    public void Interact(Player player)
    {
        if (_produced < _minGatherAmount) return;

        var overflow = player.CarrySystem.AddEnergyStack(_produced);

        if (overflow >= 0)
        {
            if(overflow == 0)
            {
                _produced = 0;
            }
            else
            {
                _produced = overflow;
            }
        }
    }
}
