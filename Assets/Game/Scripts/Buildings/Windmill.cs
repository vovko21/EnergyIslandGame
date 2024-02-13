using UnityEngine;

public class Windmill : ProductionBuilding
{
    [SerializeField] private float _productionPerGameHour;

    private void OnEnable()
    {
        TimeController.Instance.OnHourPassed += OnHourPassed;
    }

    private void OnDisable()
    {
        TimeController.Instance.OnHourPassed -= OnHourPassed;
    }

    private void OnHourPassed()
    {
        _produced += _productionPerGameHour;
    }
}
