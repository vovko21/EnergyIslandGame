using UnityEngine;

public class WindmillStatusUI : RenewableBuildingStatusUI
{
    [Header("Wind status")]
    [SerializeField] private GameObject _windStatus;

    protected override void OnBuildingStatusChanged(BuildingStatus status)
    {
        base.OnBuildingStatusChanged(status);

        if (_building.Status == BuildingStatus.NotProducing)
        {
            _windStatus.SetActive(true);
        }
        else
        {
            _windStatus.SetActive(false);
        }
    }
}