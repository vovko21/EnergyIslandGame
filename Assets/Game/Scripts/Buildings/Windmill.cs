using UnityEngine;

public class Windmill : ProductionBuilding, IInteractable
{
    [SerializeField] private int _productionPerGameHour;
    [SerializeField] private int _maxSupply;
    [SerializeField] private int _minGatherAmount;

    private void Start()
    {
        InvokeRepeating("OnHourPassed", 0f, TimeController.Instance.RealDurationOfInGameHour);
    }

    private void OnHourPassed()
    {
        if (_produced >= _maxSupply) return;

        _produced += _productionPerGameHour;
    }

    public void Interact(Player player)
    {
        if (_produced <= _minGatherAmount) return;
        player.CarrySystem.TryTakeEnergy(_produced);
    }
}
