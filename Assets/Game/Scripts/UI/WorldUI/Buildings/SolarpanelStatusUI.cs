using UnityEngine;

public class SolarpanelStatusUI : RenewableBuildingStatusUI
{
    [Header("Night status")]
    [SerializeField] private GameObject _nightStatus;

    protected override void OnBuildingStatusChanged(BuildingStatus status)
    {
        base.OnBuildingStatusChanged(status);

        if (_building.Status == BuildingStatus.NotProducing)
        {
            _nightStatus.SetActive(true);
        }
        else
        {
            _nightStatus.SetActive(false);
        }
    }
}
