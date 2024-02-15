using UnityEngine;

public class Windmill : ProductionBuilding, IInteractable
{
    [SerializeField] private int _productionPerGameHour;
    [SerializeField] private int _maxSupply;
    [SerializeField] private int _minGatherAmount;

    public int MaxSupply => _maxSupply;

    private void OnEnable()
    {
        InvokeRepeating(nameof(OnHourPassed), TimeController.Instance.RealDurationOfInGameHour, TimeController.Instance.RealDurationOfInGameHour);
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
